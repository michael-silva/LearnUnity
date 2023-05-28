using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualMovement : MonoBehaviour
{
    private PlayerBase player;
    [SerializeField] private float colliderHeight = 2f;
    [SerializeField] private float colliderRadius = 7f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float slopeLimit = 0.2f;
    [SerializeField] private float stairStepHeight = 0.5f;
    private bool isRunning = false;
    private bool isMoving = false;
    private bool isGrounded = false;
    private Timer inAirTimer = new Timer(0.5f);

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 7f;
    private Vector3 movement = Vector3.zero;
    [SerializeField] private MovingUpStairs movingUpStairs = new MovingUpStairs();

    private void Start()
    {
        player = GetComponent<PlayerBase>();
        GameInput.Instance.OnPressMove.AddListener(HandleMovement);
        GameInput.Instance.OnStartRunning.AddListener(() => isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => isRunning = false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(0, 0.15f, 0), transform.position + Vector3.down * 0.15f);
        var topPosition = transform.position + (Vector3.up * colliderHeight);
        Gizmos.DrawWireSphere(transform.position + (Vector3.up * colliderRadius), colliderRadius);
        Gizmos.DrawWireSphere(topPosition - (Vector3.up * colliderRadius), colliderRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward + Vector3.up * slopeLimit);
        Gizmos.DrawRay(transform.position, transform.forward + Vector3.down * slopeLimit);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + Vector3.up * stairStepHeight, transform.forward);
        movingUpStairs.DrawGizmos(transform);
    }

    private bool CheckCanMove(Vector3 direction)
    {
        float distance = GetCurrentSpeed() * 2 * Time.deltaTime;
        return !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * colliderHeight), colliderRadius, direction, out var hit, distance);
    }

    private float GetNormalizedVelocity()
    {
        float x = Mathf.Abs(movement.x) / runSpeed;
        float z = Mathf.Abs(movement.z) / runSpeed;
        return x > z ? x : z;
    }

    void HandleMovement(Vector2 inputMovement)
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        isGrounded = Physics.Raycast(transform.position + new Vector3(0, 0.15f, 0), Vector3.down, 0.3f);
        Vector3 direction = new Vector3(inputMovement.x, 0, inputMovement.y);
        float currentSpeed = GetCurrentSpeed();
        if (isGrounded && CheckCanMove(direction))
        {
            movement.x = inputMovement.x * currentSpeed;
            movement.z = inputMovement.y * currentSpeed;
        }
        else if (isGrounded)
        {
            movement.x = 0;
            movement.z = 0;
            if (!InDeadZone(inputMovement.x) && CheckCanMove(new Vector3(inputMovement.x, 0, 0)))
            {
                movement.x = inputMovement.x * currentSpeed;
            }
            if (!InDeadZone(inputMovement.y) && CheckCanMove(new Vector3(0, 0, inputMovement.y)))
            {
                movement.z = inputMovement.y * currentSpeed;
            }
        }
        else
        {
            // Apply gravity
            // movement += new Vector3(0, -gravity, 0);
        }

        isMoving = movement != Vector3.zero;
        MoveStairs();

        var normalVelocity = GetNormalizedVelocity();
        // transform.Translate(Vector3.forward * normalVelocity * currentSpeed * Time.deltaTime);
        transform.position += movement * Time.deltaTime;
        player.OnMoving.Invoke(normalVelocity);
    }

    void HandleVerticalMovement()
    {

    }

    private float GetCurrentSpeed()
    {
        return isRunning ? runSpeed : walkSpeed;
    }

    bool InDeadZone(float value)
    {
        float deadZone = 0.5f;
        return value < deadZone && value > -deadZone;
    }

    private void MoveStairs()
    {
        if (!isMoving) return;
        movingUpStairs.MoveStairs(transform);
    }
}
