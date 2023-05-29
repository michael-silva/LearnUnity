using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerMovement : MonoBehaviour
{
    private CharacterBase character;
    private CharacterController characterController;
    [SerializeField] private bool applyRootMotion;
    [SerializeField] private Vector3 slopeDirection = new Vector3(0, -0.5f, 0);
    private bool isMovingPressed = false;

    private void Start()
    {
        character = GetComponent<CharacterBase>();
        characterController = GetComponent<CharacterController>();
        characterController.SimpleMove(character.velocity);
        character.isGrounded = characterController.isGrounded;
        GameInput.Instance.OnPressMove.AddListener(HandleMovementInput);
        GameInput.Instance.OnStartRunning.AddListener(() => character.isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => character.isRunning = false);
    }

    private void LateUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(character.gameObject)) return;
        if (applyRootMotion) return;
        characterController.Move(character.velocity * Time.deltaTime);
        character.isGrounded = characterController.isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward + slopeDirection);
    }

    private void HandleMovementInput(Vector2 inputMovement)
    {
        isMovingPressed = inputMovement != Vector2.zero;

        character.velocity.x = inputMovement.x * character.GetCurrentSpeed();
        character.velocity.z = inputMovement.y * character.GetCurrentSpeed();

    }
}
