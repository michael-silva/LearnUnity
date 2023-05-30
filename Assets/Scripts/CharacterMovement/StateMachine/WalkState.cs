using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WalkState : CharacterState<CharacterSM>
{
    [SerializeField] private float groundedGravity = -0.5f;
    public override void OnEnterState(CharacterSM character)
    {
    }

    public override void OnExitState(CharacterSM character)
    {
    }

    public override ICharacterState<CharacterSM> Execute(CharacterSM stateMachine)
    {
        if (stateMachine.movementInput == Vector2.zero)
            return stateMachine.IdleState;
        if (stateMachine.isRunningPressed)
            return stateMachine.RunState;
        if (stateMachine.isJumpingPressed)
            return stateMachine.JumpState;
        if (!stateMachine.character.isGrounded)
            return stateMachine.FallingState;

        stateMachine.character.velocity.y = groundedGravity;

        var character = stateMachine.character;
        var transform = stateMachine.transform;

        character.velocity.x = stateMachine.movementInput.x * character.GetWalkSpeed();
        character.velocity.z = stateMachine.movementInput.y * character.GetWalkSpeed(); ;

        character.OnMoving.Invoke();
        return this;
    }
}