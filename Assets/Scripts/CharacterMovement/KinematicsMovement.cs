using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* BROKEN: Don't use this movement controller, moving a kinematics object is a bad way to do things
*/
[RequireComponent(typeof(Rigidbody))]
public class KinematicsMovement : MonoBehaviour
{
    private PlayerBase player;
    private Rigidbody rigidBody;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 7f;
    private Vector3 movement = Vector3.zero;
    private bool isRunning = false;
    private bool isMoving = false;
    private bool isGrounded = false;

    private void Start()
    {
        player = GetComponent<PlayerBase>();
        rigidBody = GetComponent<Rigidbody>();
        GameInput.Instance.OnPressMove.AddListener(HandleMovement);
        GameInput.Instance.OnStartRunning.AddListener(() => isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => isRunning = false);

        Debug.LogWarning("The Kinematics movement don't work, it was the worst movement controller");
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(player.gameObject)) return;
        if (!isMoving) return;

        // float distanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        // isGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + 0.1f);

        // if (isGrounded)
        // {
        float normalVelocity = GetNormalizedVelocity();
        rigidBody.MovePosition(transform.position + movement * Time.deltaTime);
        player.OnMoving.Invoke(normalVelocity);
        // }
    }

    private float GetNormalizedVelocity()
    {
        float x = Mathf.Abs(movement.x) / runSpeed;
        float z = Mathf.Abs(movement.z) / runSpeed;
        return x > z ? x : z;
    }

    private void HandleMovement(Vector2 inputMovement)
    {
        isMoving = inputMovement != Vector2.zero;
        movement = new Vector3(inputMovement.x, 0, inputMovement.y) * GetCurrentSpeed();
    }

    private float GetCurrentSpeed()
    {
        return isRunning ? runSpeed : walkSpeed;
    }
}
