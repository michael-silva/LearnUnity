using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    private CharacterModel character;
    private bool isJumpingPressed = false;
    private bool isJumping = false;
    private bool isFalling = false;
    private float jumpingTime = 0;
    [SerializeField] private float jumpHeight = 1;
    [SerializeField] private float gravityScale = 1;
    [SerializeField] private float fallGravityScale = 2;


    private void Start()
    {
        character = GetComponent<ICharacterComponent>()?.character;
        GameInput.Instance.OnStartJumping.AddListener(() => isJumpingPressed = true);
        GameInput.Instance.OnStopJumping.AddListener(() => isJumpingPressed = false);
    }

    private void Update()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        HandleGravity();
        HandleJump();
    }

    private void HandleGravity()
    {
        character.velocity += GetGravity() * Time.deltaTime;
    }

    private void HandleJump()
    {
        isFalling = character.velocity.y < 0;
        bool isFallingOrGrounded = isFalling || character.velocity.y == 0;
        if (isJumpingPressed && !isJumping && character.isGrounded)
        {
            character.OnJumpStart.Invoke(0);
            isJumping = true;
            float jumpForce = Mathf.Sqrt(jumpHeight * GetGravity().y * -2);
            character.velocity.y = jumpForce;
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
