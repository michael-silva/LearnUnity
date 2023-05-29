using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInput : Singleton<GameInput>
{
    private const string VERTICAL_AXIS = "Vertical";
    private const string HORIZONTAL_AXIS = "Horizontal";
    public UnityEvent<Vector2> OnPressMove = new UnityEvent<Vector2>();
    public UnityEvent OnStartRunning = new UnityEvent();
    public UnityEvent OnStopRunning = new UnityEvent();
    public UnityEvent OnStartJumping = new UnityEvent();
    public UnityEvent OnStopJumping = new UnityEvent();

    public Vector2 GetMovementAxis()
    {
        var yAxis = Input.GetAxis(VERTICAL_AXIS);
        var xAxis = Input.GetAxis(HORIZONTAL_AXIS);
        return new Vector2(xAxis, yAxis);
    }

    private void Update()
    {
        var movement = GetMovementAxis();
        OnPressMove.Invoke(movement);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnStartJumping.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            OnStopJumping.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnStartRunning.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            OnStopRunning.Invoke();
        }
    }
}
