using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private CharacterBase character;
    [SerializeField] private bool applyRootMotion;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        character.OnJumpStart.AddListener(StartJumping);
        character.OnJumpEnd.AddListener(EndJumping);
        character.OnFallStart.AddListener(StartFalling);
        character.OnFallEnd.AddListener(EndFalling);
    }

    private void Update()
    {
        if (!PlayerManager.Instance.IsPlayerActive(character.gameObject)) return;
        float velocity = GetNormalizedVelocity();
        animator.SetFloat("Velocity", velocity);
    }


    private void EndFalling()
    {
        animator.SetBool("IsFalling", false);
    }


    private void StartFalling()
    {
        animator.SetBool("IsFalling", true);
    }


    private void EndJumping(int variation)
    {
        animator.SetBool("IsJumping", false);
        animator.SetInteger("JumpNumber", variation);
    }


    private void StartJumping(int variation)
    {
        animator.SetBool("IsJumping", true);
        animator.SetInteger("JumpNumber", variation);
    }

    private float GetNormalizedVelocity()
    {
        var velocity = character.velocity;
        float x = Mathf.Abs(velocity.x) / character.GetRunSpeed();
        float z = Mathf.Abs(velocity.z) / character.GetRunSpeed();
        return x > z ? x : z;
    }

    void OnAnimatorMove()
    {
        if (!applyRootMotion) return;
        character.transform.position += animator.deltaPosition;
    }
}
