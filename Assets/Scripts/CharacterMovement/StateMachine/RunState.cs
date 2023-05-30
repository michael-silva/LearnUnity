using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class RunState : CharacterState<CharacterSM>
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
        if (!stateMachine.isRunningPressed)
            return stateMachine.WalkState;
        if (stateMachine.isJumpingPressed)
            return stateMachine.JumpState;
        if (!stateMachine.character.isGrounded)
            return stateMachine.FallingState;

        stateMachine.character.velocity.y = groundedGravity;

        var character = stateMachine.character;
        character.velocity.x = stateMachine.movementInput.x * character.GetRunSpeed();
        character.velocity.z = stateMachine.movementInput.y * character.GetRunSpeed(); ;

        character.OnMoving.Invoke();
        return this;
    }
}
