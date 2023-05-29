using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpQuaternion
{
    private Quaternion initial;
    private Quaternion final;
    private float timer = 0;
    private readonly float duration;
    private Quaternion _value;

    public Quaternion value { get { return _value; } }

    public SlerpQuaternion(Quaternion initialValue, Quaternion finalValue, float duration = 1)
    {
        this._value = initial;
        this.initial = initialValue;
        this.final = finalValue;
        this.duration = duration;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        float time = timer / duration;
        _value = Quaternion.Lerp(initial, final, time);
    }

    public bool Finished()
    {
        return timer >= duration;
    }

    public void Revert()
    {
        var initialValue = initial;
        var finalValue = final;
        this.final = initialValue;
        this.initial = finalValue;
        Reset();
    }

    public void Reset()
    {
        timer = 0;
        _value = initial;
    }
}


public class SlerpVector
{
    private Vector3 initial;
    private Vector3 final;
    private float timer = 0;
    private readonly float duration;
    private Vector3 _value;

    public Vector3 value { get { return _value; } }

    public SlerpVector(Vector3 initialValue, Vector3 finalValue, float duration = 1)
    {
        this._value = initial;
        this.initial = initialValue;
        this.final = finalValue;
        this.duration = duration;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        float time = timer / duration;
        _value = Vector3.Slerp(initial, final, time);
    }

    public bool Finished()
    {
        return timer >= duration;
    }

    public void Revert()
    {
        var initialValue = initial;
        var finalValue = final;
        this.final = initialValue;
        this.initial = finalValue;
        Reset();
    }

    public void Reset()
    {
        timer = 0;
        _value = initial;
    }
}



public class RotateTowardsInput : MonoBehaviour
{
    private CharacterBase character;
    [SerializeField] private float turningDuration = 0.3f;
    private SlerpQuaternion turningSlerp;

    private void Start()
    {
        character = GetComponent<CharacterBase>();
        GameInput.Instance.OnPressMove.AddListener(HandleRotation);
    }

    private void Update()
    {
        if (!PlayerManager.Instance.IsPlayerActive(character.gameObject) || turningSlerp == null) return;
        turningSlerp?.Update();
        transform.rotation = turningSlerp.value;
    }

    private void HandleRotation(Vector2 movement)
    {
        if (movement == Vector2.zero) return;
        var targetRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y));
        turningSlerp = new SlerpQuaternion(transform.rotation, targetRotation, turningDuration);
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
