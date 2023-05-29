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

    public Vector3 velocity = Vector3.zero;
    public bool isRunning = false;
    public bool isGrounded = false;


    [SerializeField] protected float walkSpeed = 2f;
    [SerializeField] protected float runSpeed = 7f;

    public float GetRunSpeed()
    {
        return runSpeed;
    }

    public float GetCurrentSpeed()
    {
        return isRunning ? runSpeed : walkSpeed;
    }

}
