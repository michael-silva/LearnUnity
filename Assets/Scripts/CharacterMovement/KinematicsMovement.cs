using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* BROKEN: Don't use this movement controller, moving a kinematics object is a bad way to do things
*/
[RequireComponent(typeof(Rigidbody))]
public class KinematicsMovement : MonoBehaviour
{
    private CharacterBase character;
    private Rigidbody rigidBody;
    [SerializeField] private bool applyRootMotion;
    private bool isMoving = false;

    private void Start()
    {
        character = GetComponent<CharacterBase>();
        rigidBody = GetComponent<Rigidbody>();
        GameInput.Instance.OnPressMove.AddListener(HandleMovement);
        GameInput.Instance.OnStartRunning.AddListener(() => character.isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => character.isRunning = false);

        Debug.LogWarning("The Kinematics movement don't work, it was the worst movement controller");
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(character.gameObject)) return;
        if (!isMoving) return;

        float distanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        character.isGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + 0.1f);

        // if (isGrounded)
        // {
        if (!applyRootMotion)
        {
            rigidBody.MovePosition(transform.position + character.velocity * Time.deltaTime);
        }
        // }
    }

    private void HandleMovement(Vector2 inputMovement)
    {
        isMoving = inputMovement != Vector2.zero;
        character.velocity = new Vector3(inputMovement.x, 0, inputMovement.y) * character.GetCurrentSpeed();
    }
}
