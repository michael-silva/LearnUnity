using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementPhysics : MonoBehaviour
{
    private CharacterModel character;
    private Rigidbody rigidBody;
    private bool isMoving = false;
    [SerializeField] private MovingUpStairs movingUpStairs = new MovingUpStairs();

    private void Start()
    {
        character = GetComponent<ICharacterComponent>()?.character;
        rigidBody = GetComponent<Rigidbody>();
        GameInput.Instance.OnPressMove.AddListener(HandleMovement);
        GameInput.Instance.OnStartRunning.AddListener(() => character.isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => character.isRunning = false);
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        MoveStairs();
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
