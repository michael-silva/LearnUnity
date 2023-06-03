using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ICharacterComponent))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterModel character;

    private void Start()
    {
        character = GetComponent<ICharacterComponent>()?.character;
        GameInput.Instance.OnPressMove.AddListener(HandleMovementInput);
        GameInput.Instance.OnStartRunning.AddListener(() => character.isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => character.isRunning = false);
    }

    private void HandleMovementInput(Vector2 inputMovement)
    {
        character.velocity.x = inputMovement.x * character.GetCurrentSpeed();
        character.velocity.z = inputMovement.y * character.GetCurrentSpeed();
    }
}
