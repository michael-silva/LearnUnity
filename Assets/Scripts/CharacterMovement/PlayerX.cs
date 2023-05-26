using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerX : PlayerBase
{
    [SerializeField] private float maxWalkVelocity = 0.5f;
    [SerializeField] private float maxRunningVelocity = 1f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float deceleration = 2f;
    private Vector2 velocity = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {

    }
    Vector2 GetNormalizedVelocity()
    {
        return velocity;
    }

    void Update()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        var axisVert = Input.GetAxis("Vertical");
        var axisHor = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.Space);
        float currentMaxVelocity = isRunning ? maxRunningVelocity : maxWalkVelocity;
        bool isMovingForward = axisVert > 0.1;
        bool isMovingBackward = axisVert < -0.1;
        bool isMovingRight = axisHor > 0.1;
        bool isMovingLeft = axisHor < -0.1;

        if (isMovingForward)
        {
            if (velocity.y < currentMaxVelocity)
                velocity.y += axisVert * acceleration * Time.deltaTime;
        }
        else if (isMovingBackward)
        {
            if (velocity.y > -currentMaxVelocity)
                velocity.y += axisVert * acceleration * Time.deltaTime;
        }
        else if (!InDeadZone(velocity.y))
        {
            float direction = velocity.y > 0 ? -1 : 1;
            velocity.y += direction * deceleration * Time.deltaTime;
        }

        if (isMovingRight)
        {
            if (velocity.x < currentMaxVelocity)
                velocity.x += axisHor * acceleration * Time.deltaTime;
        }
        else if (isMovingLeft)
        {
            if (velocity.x > -currentMaxVelocity)
                velocity.x += axisHor * acceleration * Time.deltaTime;
        }
        else if (!InDeadZone(velocity.x))
        {
            float direction = velocity.x > 0 ? -1 : 1;
            velocity.x += direction * deceleration * Time.deltaTime;
        }

        onMoving.Invoke(GetNormalizedVelocity());
    }

    bool InDeadZone(float value)
    {
        float deadZone = 0.1f;
        return value < deadZone && value > -deadZone;
    }
}
