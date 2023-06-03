using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(ICharacterComponent))]
public class CharacterSM : MonoBehaviour, ICharacterComponent, ICharacterStateMachine<CharacterSM>
{
    public bool isJumpingPressed { get; private set; }
    public bool isRunningPressed { get; private set; }
    public Vector2 movementInput { get; private set; }
    [SerializeField]
    private CharacterModel _character = new CharacterModel();
    public CharacterModel character { get { return _character; } }
    private QuaternionLerp turningSlerp;
    [SerializeField] private float turningDuration = 0.3f;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private IdleState _idleState = new IdleState();
    public IdleState IdleState => _idleState;
    [SerializeField] private JumpState _jumpState = new JumpState();
    public JumpState JumpState => _jumpState;
    [SerializeField] private RunState _runState = new RunState();
    public RunState RunState => _runState;
    [SerializeField] private WalkState _walkState = new WalkState();
    public WalkState WalkState => _walkState;
    [SerializeField] private FallingState _fallState = new FallingState();
    public FallingState FallingState => _fallState;
    private ICharacterState<CharacterSM> currentState;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        ChangeState(this.IdleState);
        Move(character.velocity);

        GameInput.Instance.OnStartRunning.AddListener(() => isRunningPressed = true);
        GameInput.Instance.OnStopRunning.AddListener(() => isRunningPressed = false);
        GameInput.Instance.OnStartJumping.AddListener(() => isJumpingPressed = true);
        GameInput.Instance.OnStopJumping.AddListener(() => isJumpingPressed = false);
        GameInput.Instance.OnPressMove.AddListener((Vector2 input) => movementInput = input);
    }

    private void Update()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        var state = currentState.Execute(this);
        if (state != currentState)
        {
            ChangeState(state);
        }

        var movement = character.moveInCameraSpace
            ? CameraUtils.ConvertToCameraSpace(character.velocity)
            : character.velocity;

        Move(movement);
        HandleRotation();
    }

    public void Move(Vector3 movement)
    {
        characterController.Move(movement * Time.deltaTime);
        character.isGrounded = characterController.isGrounded;
    }

    private void HandleRotation()
    {
        if (movementInput == Vector2.zero) return;

        var rotation = new Vector3(movementInput.x, 0, movementInput.y);
        if (character.moveInCameraSpace)
        {
            rotation = CameraUtils.ConvertToCameraSpace(rotation);
        }
        var targetRotation = Quaternion.LookRotation(rotation);
        if (targetRotation != turningSlerp.finalValue)
        {
            turningSlerp = new QuaternionLerp(transform.rotation, targetRotation, turningDuration);
        }

        turningSlerp?.Update();
        transform.rotation = turningSlerp.value;
    }

    public void ChangeState(ICharacterState<CharacterSM> newState)
    {
        if (currentState != null)
        {
            currentState.OnExitState(this);
        }
        currentState = newState;
        Debug.Log(newState);
        currentState.OnEnterState(this);
    }
}
