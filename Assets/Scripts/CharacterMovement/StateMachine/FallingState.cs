using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FallingState : CharacterState<CharacterSM>
{
    [SerializeField] private float gravity = -9.8f;
    private Vector3 movement = Vector3.zero;

    public override void OnEnterState(CharacterSM stateMachine)
    {
    }

    public override void OnExitState(CharacterSM stateMachine)
    {
    }



    public override ICharacterState<CharacterSM> Execute(CharacterSM stateMachine)
    {
        if (stateMachine.character.isGrounded)
        {
            if (stateMachine.isJumpingPressed)
                return stateMachine.JumpState;
            if (stateMachine.movementInput == Vector2.zero)
                return stateMachine.IdleState;
            else
            {
                if (stateMachine.isRunningPressed)
                    return stateMachine.RunState;
                else
                    return stateMachine.WalkState;
            }
        }

        HandleGravity(stateMachine.character);

        return this;
    }

    private void HandleGravity(CharacterBase character)
    {
        float previousY = movement.y;
        movement.y = movement.y + (gravity * Time.deltaTime);
        character.velocity.y = (previousY + movement.y) * 0.5f;
    }
}
