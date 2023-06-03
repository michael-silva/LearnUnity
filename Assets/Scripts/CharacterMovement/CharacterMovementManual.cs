using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementManual : MonoBehaviour
{
    private CharacterModel character;
    [SerializeField] private float colliderHeight = 2f;
    [SerializeField] private float colliderRadius = 7f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float slopeLimit = 0.2f;
    [SerializeField] private float stairStepHeight = 0.5f;
    private bool isMoving = false;
    private Timer inAirTimer = new Timer(0.5f);
    [SerializeField] private MovingUpStairs movingUpStairs = new MovingUpStairs();

    private void Start()
    {
        character = GetComponent<ICharacterComponent>()?.character;
        GameInput.Instance.OnPressMove.AddListener(HandleMovement);
        GameInput.Instance.OnStartRunning.AddListener(() => character.isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => character.isRunning = false);
    }

    private void Update()
    {
        float yMovement = Mathf.Abs(character.velocity.y) * Time.deltaTime;
        if (character.velocity.y < 0 && Physics.Raycast(transform.position, Vector3.down, out var hit, yMovement + 0.1f))
        {
            character.isGrounded = true;
            transform.position = hit.point;
            // transform.position = new Vector3(0, hit.point.y, 0);
            character.velocity.y = 0;
        }
        else
        {
            character.isGrounded = character.velocity.y == 0;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.1f);

        Gizmos.color = Color.white;
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
        float distance = character.GetCurrentSpeed() * 2 * Time.deltaTime;
        return !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * colliderHeight), colliderRadius, direction, out var hit, distance);
    }

    void HandleMovement(Vector2 inputMovement)
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        // character.isGrounded = Physics.Raycast(transform.position + new Vector3(0, 0.15f, 0), Vector3.down, 0.3f);
        Vector3 direction = new Vector3(inputMovement.x, 0, inputMovement.y);
        float currentSpeed = character.GetCurrentSpeed();
        if (character.isGrounded && CheckCanMove(direction))
        {
            character.velocity.x = inputMovement.x * currentSpeed;
            character.velocity.z = inputMovement.y * currentSpeed;
        }
        else if (character.isGrounded)
        {
            character.velocity.x = 0;
            character.velocity.z = 0;
            if (!InDeadZone(inputMovement.x) && CheckCanMove(new Vector3(inputMovement.x, 0, 0)))
            {
                character.velocity.x = inputMovement.x * currentSpeed;
            }
            if (!InDeadZone(inputMovement.y) && CheckCanMove(new Vector3(0, 0, inputMovement.y)))
            {
                character.velocity.z = inputMovement.y * currentSpeed;
            }
        }

        isMoving = inputMovement != Vector2.zero;
        MoveStairs();
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
