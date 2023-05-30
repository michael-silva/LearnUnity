using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JumpState : CharacterState<CharacterSM>
{
    [SerializeField] private float maxJumpHeight = 1f;
    [SerializeField] private float maxJumpTime = 0.5f;

    private List<JumpSettings> jumpSettings = new List<JumpSettings>();
    private int currentJumpIndex = 0;

    private Vector3 movement = Vector3.zero;

    public JumpState()
    {
        jumpSettings.Add(new JumpSettings(maxJumpTime, maxJumpHeight));
        jumpSettings.Add(new JumpSettings(maxJumpTime * 1.25f, maxJumpHeight));
        jumpSettings.Add(new JumpSettings(maxJumpTime * 1.3f, maxJumpHeight));
        jumpSettings.Add(new JumpSettings(maxJumpTime * 1f, maxJumpHeight));
    }

    public override void OnEnterState(CharacterSM stateMachine)
    {
        var character = stateMachine.character;
        character.OnJumpStart.Invoke(currentJumpIndex);
        var currentJump = jumpSettings[currentJumpIndex];
        currentJumpIndex = (currentJumpIndex + 1) % jumpSettings.Count;
        movement = character.velocity;
        movement.y = currentJump.initialJumpForce;
        character.velocity.y = currentJump.initialJumpForce;
    }

    public override void OnExitState(CharacterSM stateMachine)
    {
        var character = stateMachine.character;
        character.OnJumpEnd.Invoke(currentJumpIndex);
    }

    public override ICharacterState<CharacterSM> Execute(CharacterSM stateMachine)
    {
        HandleGravity(stateMachine.character);

        var character = stateMachine.character;

        if (character.isGrounded)
        {
            if (stateMachine.movementInput == Vector2.zero)
                return stateMachine.IdleState;
            if (stateMachine.isRunningPressed)
                return stateMachine.RunState;

            return stateMachine.WalkState;
        }


        return this;
    }

    private void HandleGravity(CharacterBase character)
    {
        bool isFalling = movement.y <= 0;
        var currentJump = jumpSettings[currentJumpIndex];
        if (isFalling)
        {
            float previousY = movement.y;
            movement.y = movement.y + (currentJump.gravity * currentJump.fallAcceleration * Time.deltaTime);
            character.velocity.y = Mathf.Max((previousY + movement.y) * 0.5f, -20f);

        }
        else
        {
            // Velocity Verlet Integration
            // Formula from calculate jump frame rate independent
            float previousY = movement.y;
            movement.y = movement.y + (currentJump.gravity * Time.deltaTime);
            character.velocity.y = (previousY + movement.y) * 0.5f;
        }
    }
}
