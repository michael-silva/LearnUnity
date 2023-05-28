using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private PlayerBase player;
    [SerializeField] private bool applyRootMotion;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player.OnMoving.AddListener(UpdateSpeed);
    }

    private void UpdateSpeed(float velocity)
    {
        animator.SetFloat("Velocity", velocity);
    }

    void OnAnimatorMove()
    {
        if (!applyRootMotion) return;
        player.transform.position += animator.deltaPosition;
    }
}
