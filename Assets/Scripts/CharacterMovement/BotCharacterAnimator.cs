using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCharacterAnimator : MonoBehaviour
{
    [SerializeField] private PlayerBase player;
    [SerializeField] private bool applyRootMotion;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player.OnMoving.AddListener(UpdateSpeed);
    }

    private void UpdateSpeed(Vector2 velocity)
    {
        animator.SetFloat("VelocityX", velocity.x);
        animator.SetFloat("VelocityZ", velocity.y);
    }

    void OnAnimatorMove()
    {
        if (!applyRootMotion) return;
        player.transform.position += animator.deltaPosition;
    }
}
