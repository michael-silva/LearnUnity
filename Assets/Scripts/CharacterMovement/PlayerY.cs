using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerY : MonoBehaviour, ICharacterComponent
{
    [SerializeField]
    private CharacterModel _character = new CharacterModel();
    public CharacterModel character { get { return _character; } }

    [SerializeField] private float maxWalkVelocity = 0.5f;
    [SerializeField] private float maxRunningVelocity = 1f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float turnSpeed = 90f;

    // Start is called before the first frame update
    void Start()
    {

    }
    Vector2 GetNormalizedVelocity()
    {
        return character.velocity;
    }

    void UpdateAlternative()
    {
        var axisVert = Input.GetAxis("Vertical");
        var axisHor = Input.GetAxis("Horizontal");

        bool isRunning = Input.GetKey(KeyCode.Space);
        float currentMaxVelocity = isRunning ? maxRunningVelocity : maxWalkVelocity;
        bool isMovingForward = axisVert > 0.1;

        if (isMovingForward)
        {
            if (character.velocity.y < currentMaxVelocity)
                character.velocity.y += axisVert * acceleration * Time.deltaTime;
            character.OnMoving.Invoke();
        }
        else if (character.velocity.y > 0)
        {
            character.velocity.y -= deceleration * Time.deltaTime;
            if (character.velocity.y < 0)
                character.velocity.y = 0;
            character.OnMoving.Invoke();
        }

        if (axisHor != 0)
        {
            transform.Rotate(Vector3.up * axisHor * turnSpeed * Time.deltaTime, Space.Self);
        }
    }

    void Update()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        var axisVert = Input.GetAxis("Vertical");
        var axisHor = Input.GetAxis("Horizontal");

        bool isRunning = Input.GetKey(KeyCode.Space);
        float currentMaxVelocity = isRunning ? maxRunningVelocity : maxWalkVelocity;
        bool isMoving = axisVert != 0;

        if (isMoving)
        {
            if (character.velocity.y < currentMaxVelocity)
                character.velocity.y += Mathf.Abs(axisVert) * acceleration * Time.deltaTime;
            character.OnMoving.Invoke();
        }
        else if (character.velocity.y > 0)
        {
            character.velocity.y -= deceleration * Time.deltaTime;
            if (character.velocity.y < 0)
                character.velocity.y = 0;
            character.OnMoving.Invoke();
        }

        HandleRotation(axisVert, axisHor);
    }

    private void HandleRotation(float axisVert, float axisHor)
    {
        var newPosition = new Vector3(axisHor, 0, axisVert);
        var direction = transform.position - newPosition;
        transform.LookAt(direction);

    }
}
