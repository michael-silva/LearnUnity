using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsJumping : MonoBehaviour
{
    private CharacterBase character;
    private Rigidbody rigidBody;
    private bool isJumpingPressed = false;
    [ReadOnly] private bool isJumping = false;
    [SerializeField] private float jumpForce = 10f;

    void Start()
    {
        character = GetComponent<CharacterBase>();
        rigidBody = GetComponent<Rigidbody>();
        GameInput.Instance.OnStartJumping.AddListener(() => isJumpingPressed = true);
    }

    private void Update()
    {
        if (!PlayerManager.Instance.IsPlayerActive(character.gameObject)) return;
        HandleJump();
    }

    private void HandleJump()
    {

        if (isJumpingPressed && !isJumping && character.isGrounded)
        {
            character.OnJumpStart.Invoke(0);
            isJumping = true;
            isJumpingPressed = false;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (!isJumpingPressed && isJumping && character.isGrounded)
        {
            isJumping = false;
            character.OnJumpEnd.Invoke(0);
        }
        Debug.Log($"jumo: {isJumping} ground: {character.isGrounded}");
    }
}
