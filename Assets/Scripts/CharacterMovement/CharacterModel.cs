using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public sealed class CharacterModel
{
    private UnityEvent onMoving = new UnityEvent();
    public UnityEvent OnMoving => onMoving;
    private UnityEvent<int> onJumpStart = new UnityEvent<int>();
    public UnityEvent<int> OnJumpStart => onJumpStart;
    private UnityEvent<int> onJumpEnd = new UnityEvent<int>();
    public UnityEvent<int> OnJumpEnd => onJumpEnd;
    private UnityEvent onFallStart = new UnityEvent();
    public UnityEvent OnFallStart => onFallStart;
    private UnityEvent onFallEnd = new UnityEvent();
    public UnityEvent OnFallEnd => onFallEnd;

    public Vector3 velocity = Vector3.zero;
    public bool isRunning = false;
    public bool isGrounded = false;


    [SerializeField] public float walkSpeed = 2f;
    [SerializeField] public float runSpeed = 7f;

    [SerializeField] public bool applyRootMotion;
    [SerializeField] public bool moveInCameraSpace = false;

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
