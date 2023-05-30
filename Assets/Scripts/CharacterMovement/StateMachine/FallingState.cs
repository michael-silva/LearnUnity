using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FallingState : CharacterState<CharacterSM>
{
    [SerializeField] private float deceleration = -0.8f;
    [SerializeField] private float gravity = -9.8f;
    private Vector3 movement = Vector3.zero;

    public override void OnEnterState(CharacterSM stateMachine)
    {
        movement = Vector3.zero;
        stateMachine.character.OnFallStart.Invoke();
    }

    public override void OnExitState(CharacterSM stateMachine)
    {
    }



    public override ICharacterState<CharacterSM> Execute(CharacterSM stateMachine)
    {
        if (stateMachine.character.isGrounded)
        {
            stateMachine.character.OnFallEnd.Invoke();
            if (stateMachine.movementInput != Vector2.zero)
                return stateMachine.WalkState;

            return stateMachine.IdleState;
        }


        stateMachine.character.velocity.x += deceleration * Time.deltaTime;
        stateMachine.character.velocity.z += deceleration * Time.deltaTime;

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
