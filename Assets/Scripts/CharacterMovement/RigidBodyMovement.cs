using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    private CharacterBase character;
    private Rigidbody rigidBody;
    [SerializeField] private bool applyRootMotion;
    private bool isMoving = false;
    [SerializeField] private Timer inAirTimer = new Timer(0.5f);
    [SerializeField] private MovingUpStairs movingUpStairs = new MovingUpStairs();

    private void Start()
    {
        character = GetComponent<CharacterBase>();
        rigidBody = GetComponent<Rigidbody>();
        GameInput.Instance.OnPressMove.AddListener(HandleMovement);
        GameInput.Instance.OnStartRunning.AddListener(() => character.isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => character.isRunning = false);
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(character.gameObject)) return;

        character.isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.04f);
        // if (character.isGrounded)
        // {
        //     if (inAirTimer.IsRunning())
        //     {
        //         inAirTimer.Stop();
        //     }
        // }
        // else
        // {
        //     if (!inAirTimer.IsRunning())
        //     {
        //         inAirTimer.Start();
        //     }
        //     inAirTimer.Tick();
        // }

        // if (!inAirTimer.IsFinish())
        // {
        // rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);

        if (!applyRootMotion)
        {
            rigidBody.velocity = new Vector3(character.velocity.x, rigidBody.velocity.y, character.velocity.z);
            MoveStairs();
        }
        // }
    }

    private void OnDrawGizmos()
    {
        movingUpStairs.DrawGizmos(transform);
    }

    private void HandleMovement(Vector2 inputMovement)
    {
        isMoving = inputMovement != Vector2.zero;
        character.velocity = new Vector3(inputMovement.x, 0, inputMovement.y) * character.GetCurrentSpeed();
    }

    private void MoveStairs()
    {
        if (!isMoving) return;
        movingUpStairs.MoveStairs(transform, rigidBody);
    }
}
