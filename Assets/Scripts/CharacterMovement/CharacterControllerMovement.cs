using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerMovement : MonoBehaviour
{
    private PlayerBase player;
    private CharacterController characterController;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private Vector3 slopeDirection = new Vector3(0, -0.5f, 0);
    private Vector3 movement = Vector3.zero;
    private bool isRunning = false;
    private bool isMoving = false;
    private Timer inAirTimer = new Timer(0.5f);

    private void Start()
    {
        player = GetComponent<PlayerBase>();
        characterController = GetComponent<CharacterController>();
        GameInput.Instance.OnPressMove.AddListener(HandleMovement);
        GameInput.Instance.OnStartRunning.AddListener(() => isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => isRunning = false);
    }

    private void Update()
    {
        if (!characterController.isGrounded) return;
        float slopeDistance = 1;
        if (Physics.Raycast(transform.position, transform.forward + slopeDirection, out var hit, slopeDistance)) return;


        Debug.Log("Sloping");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward + slopeDirection);
    }

    private float GetNormalizedVelocity()
    {
        float x = Mathf.Abs(movement.x) / runSpeed;
        float z = Mathf.Abs(movement.z) / runSpeed;
        return x > z ? x : z;
    }

    private void HandleMovement(Vector2 inputMovement)
    {
        if (!PlayerManager.Instance.IsPlayerActive(player.gameObject)) return;
        if (characterController.isGrounded)
        {
            if (inAirTimer.IsRunning())
            {
                inAirTimer.Stop();
            }
            // just to maintain isGrounded as true
            movement.y = -0.5f;
            movement.x = inputMovement.x * GetCurrentSpeed();
            movement.z = inputMovement.y * GetCurrentSpeed();
        }
        else
        {
            if (!inAirTimer.IsRunning())
            {
                inAirTimer.Start();
            }
            inAirTimer.Tick();

            if (inAirTimer.IsFinish())
            {
                movement.y -= gravity;
                movement.x = 0;
                movement.z = 0;
            }
        }

        characterController.SimpleMove(movement);
        player.OnMoving.Invoke(GetNormalizedVelocity());
    }

    private float GetCurrentSpeed()
    {
        return isRunning ? runSpeed : walkSpeed;
    }
}
