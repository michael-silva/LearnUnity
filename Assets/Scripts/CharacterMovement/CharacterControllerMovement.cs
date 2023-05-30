using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraUtils
{
    public static Vector3 ConvertToCameraSpace(Vector3 vector)
    {
        var cameraForward = Camera.main.transform.forward;
        var cameraRight = Camera.main.transform.right;

        // remove the Y values to ignore up/down direction
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        var cameraForwardProduct = vector.z * cameraForward;
        var cameraRightProduct = vector.x * cameraRight;

        var result = cameraForwardProduct + cameraRightProduct;
        result.y = vector.y;
        return result;

    }
}

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerMovement : MonoBehaviour
{
    private CharacterBase character;
    private CharacterController characterController;
    [SerializeField] private bool applyRootMotion;
    [SerializeField] private Vector3 slopeDirection = new Vector3(0, -0.5f, 0);
    [SerializeField] private bool moveInCameraSpace = false;

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
        var movement = moveInCameraSpace ? CameraUtils.ConvertToCameraSpace(character.velocity) : character.velocity;
        characterController.Move(movement * Time.deltaTime);
        character.isGrounded = characterController.isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward + slopeDirection);
    }

    private void HandleMovementInput(Vector2 inputMovement)
    {
        character.velocity.x = inputMovement.x * character.GetCurrentSpeed();
        character.velocity.z = inputMovement.y * character.GetCurrentSpeed();

    }
}
