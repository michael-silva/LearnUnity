using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    private PlayerBase player;
    private Rigidbody rigidBody;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 7f;
    private Vector3 movement = Vector3.zero;
    private bool isRunning = false;
    private bool isMoving = false;
    private bool isGrounded = false;
    [SerializeField] private Timer inAirTimer = new Timer(0.5f);
    [SerializeField] private MovingUpStairs movingUpStairs = new MovingUpStairs();

    private void Start()
    {
        player = GetComponent<PlayerBase>();
        rigidBody = GetComponent<Rigidbody>();
        GameInput.Instance.OnPressMove.AddListener(HandleMovement);
        GameInput.Instance.OnStartRunning.AddListener(() => isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => isRunning = false);
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(player.gameObject)) return;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        if (isGrounded)
        {
            if (inAirTimer.IsRunning())
            {
                inAirTimer.Stop();
            }
        }
        else
        {
            if (!inAirTimer.IsRunning())
            {
                inAirTimer.Start();
            }
            inAirTimer.Tick();
        }

        if (!inAirTimer.IsFinish())
        {
            // rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
            // player.OnMoving.Invoke(0);
            float normalVelocity = GetNormalizedVelocity();
            rigidBody.velocity = new Vector3(movement.x, rigidBody.velocity.y, movement.z);
            player.OnMoving.Invoke(normalVelocity);
            MoveStairs();
        }
        else
        {
            player.OnMoving.Invoke(0);
        }
    }

    private void OnDrawGizmos()
    {
        movingUpStairs.DrawGizmos(transform);
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

    private void MoveStairs()
    {
        if (!isMoving) return;
        movingUpStairs.MoveStairs(transform, rigidBody);
    }
}
