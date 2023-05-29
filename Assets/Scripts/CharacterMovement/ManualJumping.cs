using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct JumpSettings
{
    public float gravity { get; private set; }
    public float initialJumpForce { get; private set; }
    public float maxJumpHeight { get; private set; }
    public float maxJumpTime { get; private set; }
    public float fallAcceleration { get; private set; }

    public JumpSettings(float maxJumpTime, float maxJumpHeight, float fallAcceleration = 2.0f)
    {
        float timeToApex = maxJumpTime / 2;
        this.gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        this.initialJumpForce = (2 * maxJumpHeight) / timeToApex;
        this.maxJumpHeight = maxJumpHeight;
        this.maxJumpTime = maxJumpTime;
        this.fallAcceleration = fallAcceleration;
    }
}

public class ManualJumping : MonoBehaviour
{
    private CharacterBase character;
    [SerializeField] private bool applyRootMotion;
    [SerializeField] private float defaultGravity = -9.8f;
    [SerializeField] private float groundedGravity = -0.5f;
    [SerializeField] private float maxJumpHeight = 1f;
    [SerializeField] private float maxJumpTime = 0.5f;

    private List<JumpSettings> jumpSettings = new List<JumpSettings>();
    private int currentJumpIndex = 0;
    private bool isJumpingPressed = false;
    private bool isJumping = false;

    private Vector3 movement = Vector3.zero;

    private void Start()
    {
        character = GetComponent<CharacterBase>();
        SetupJumpSettings();
        GameInput.Instance.OnStartJumping.AddListener(() => isJumpingPressed = true);
    }

    private void Update()
    {
        if (!PlayerManager.Instance.IsPlayerActive(character.gameObject)) return;
        if (applyRootMotion) return;
        HandleGravity();
        HandleJump();
    }

    private void SetupJumpSettings()
    {
        jumpSettings.Add(new JumpSettings(maxJumpTime, maxJumpHeight));
        jumpSettings.Add(new JumpSettings(maxJumpTime * 1.25f, maxJumpHeight));
        jumpSettings.Add(new JumpSettings(maxJumpTime * 1.3f, maxJumpHeight));
        jumpSettings.Add(new JumpSettings(maxJumpTime * 1.5f, maxJumpHeight));
    }

    private void HandleGravity()
    {
        bool isFalling = movement.y <= 0 && !isJumpingPressed;
        var currentJump = jumpSettings[currentJumpIndex];
        if (character.isGrounded)
        {
            movement.y = groundedGravity;
            character.velocity.y = movement.y;
        }
        else if (isFalling)
        {
            float previousY = movement.y;
            movement.y = movement.y + (currentJump.gravity * currentJump.fallAcceleration * Time.deltaTime);
            character.velocity.y = Mathf.Max((previousY + movement.y) * 0.5f, -20f);

        }
        else
        {
            // Velocity Verlet Integration
            // Formula from calculate jump frame rate independent
            float previousY = movement.y;
            movement.y = movement.y + (currentJump.gravity * Time.deltaTime);
            character.velocity.y = (previousY + movement.y) * 0.5f;
        }
    }

    private void HandleJump()
    {
        if (isJumpingPressed && !isJumping && character.isGrounded)
        {
            character.OnJumpStart.Invoke(currentJumpIndex);
            isJumping = true;
            isJumpingPressed = false;
            var currentJump = jumpSettings[currentJumpIndex];
            currentJumpIndex = (currentJumpIndex + 1) % jumpSettings.Count;
            movement.y = currentJump.initialJumpForce;
            character.velocity.y = movement.y;
        }
        else if (!isJumpingPressed && isJumping && character.isGrounded)
        {
            isJumping = false;
            character.OnJumpEnd.Invoke(currentJumpIndex);
        }
    }

}
