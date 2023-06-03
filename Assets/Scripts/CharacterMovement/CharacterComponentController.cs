using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterComponentController : CharacterComponent
{
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.SimpleMove(character.velocity);
        character.isGrounded = characterController.isGrounded;
    }

    private void LateUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        if (character.applyRootMotion) return;
        var movement = character.moveInCameraSpace ? CameraUtils.ConvertToCameraSpace(character.velocity) : character.velocity;
        characterController.Move(movement * Time.deltaTime);
        character.isGrounded = characterController.isGrounded;
    }
}
