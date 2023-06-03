using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterJumpPhysics : MonoBehaviour
{
    private CharacterModel character;
    private Rigidbody rigidBody;
    private bool isJumpingPressed = false;
    private bool isJumping = false;
    private bool isFalling = false;
    private float jumpingTime = 0;
    [SerializeField] private float jumpHeight = 1;
    [SerializeField] private float gravityScale = 1;
    [SerializeField] private float fallGravityScale = 2;

    void Start()
    {
        character = GetComponent<ICharacterComponent>()?.character;
        rigidBody = GetComponent<Rigidbody>();

        GameInput.Instance.OnStartJumping.AddListener(() => isJumpingPressed = true);
        GameInput.Instance.OnStopJumping.AddListener(() => isJumpingPressed = false);
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;

        HandleJump();

        //It has to be FixedUpdate, because it applies force to the rigidbody constantly.
        rigidBody.AddForce(GetGravity(), ForceMode.Acceleration);
    }

    private void HandleJump()
    {
        isFalling = rigidBody.velocity.y < 0;
        bool isFallingOrGrounded = isFalling || rigidBody.velocity.y == 0;
        if (isJumpingPressed && !isJumping && character.isGrounded)
        {
            character.OnJumpStart.Invoke(0);
            isJumping = true;
            float jumpForce = Mathf.Sqrt(jumpHeight * GetGravity().y * -2) * rigidBody.mass;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        else if (isJumping)
        {
            if (!isJumpingPressed || isFalling)
            {
                isJumping = false;
            }
        }
        else if (isFallingOrGrounded && character.isGrounded)
        {
            character.OnJumpEnd.Invoke(0);
        }
    }

    private Vector3 GetGravity()
    {
        float scale = !isJumping && !character.isGrounded ? fallGravityScale : gravityScale;
        return Physics.gravity * scale;
    }
}
