using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class IdleState : CharacterState<CharacterSM>
{
    [SerializeField] private float groundedGravity = -0.5f;
    public override void OnEnterState(CharacterSM stateMachine)
    {
    }

    public override void OnExitState(CharacterSM stateMachine)
    {
    }

    public override ICharacterState<CharacterSM> Execute(CharacterSM stateMachine)
    {
        if (stateMachine.movementInput != Vector2.zero)
            return stateMachine.WalkState;
        if (stateMachine.isJumpingPressed)
            return stateMachine.JumpState;
        if (stateMachine.isJumpingPressed)
            return stateMachine.JumpState;
        if (!stateMachine.character.isGrounded)
            return stateMachine.FallingState;

        return this;
    }
}
