using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GroundedState : CharacterState<CharacterSM>, ICharacterStateMachine<GroundedState>
{
    private ICharacterState<GroundedState> currentState;

    public override void OnEnterState(CharacterSM stateMachine)
    {
        var character = stateMachine.character;
        stateMachine.ChangeState(stateMachine.IdleState);
        stateMachine.Move(character.velocity);
    }

    public override void OnExitState(CharacterSM stateMachine)
    {
    }

    public override ICharacterState<CharacterSM> Execute(CharacterSM stateMachine)
    {
        if (stateMachine.isJumpingPressed)
            return stateMachine.JumpState;
        if (!stateMachine.character.isGrounded)
            return stateMachine.FallingState;

        var state = currentState.Execute(this);
        if (state != currentState)
        {
            ChangeState(state);
        }

        return this;
    }


    public void ChangeState(ICharacterState<GroundedState> newState)
    {
        if (currentState != null)
        {
            currentState.OnExitState(this);
        }
        currentState = newState;
        Debug.Log(newState);
        currentState.OnEnterState(this);
    }
}
