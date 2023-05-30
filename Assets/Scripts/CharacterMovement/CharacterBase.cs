using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterBase : MonoBehaviour, ICharacter
{
    protected UnityEvent onMoving = new UnityEvent();
    public UnityEvent OnMoving => onMoving;
    protected UnityEvent<int> onJumpStart = new UnityEvent<int>();
    public UnityEvent<int> OnJumpStart => onJumpStart;
    protected UnityEvent<int> onJumpEnd = new UnityEvent<int>();
    public UnityEvent<int> OnJumpEnd => onJumpEnd;
    protected UnityEvent onFallStart = new UnityEvent();
    public UnityEvent OnFallStart => onFallStart;
    protected UnityEvent onFallEnd = new UnityEvent();
    public UnityEvent OnFallEnd => onFallEnd;

    public Vector3 velocity = Vector3.zero;
    public bool isRunning = false;
    public bool isGrounded = false;


    [SerializeField] protected float walkSpeed = 2f;
    [SerializeField] protected float runSpeed = 7f;

    public float GetRunSpeed()
    {
        return runSpeed;
    }
    public float GetWalkSpeed()
    {
        return walkSpeed;
    }

    public float GetCurrentSpeed()
    {
        return isRunning ? runSpeed : walkSpeed;
    }

}
