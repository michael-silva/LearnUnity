using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Lerper<T>
{
    public AnimationCurve curve;
    public T initialValue { get; private set; }
    public T finalValue { get; private set; }
    protected readonly float duration;
    private float timeElapsed;
    private T _value;
    public T value { get { return _value; } }

    public Lerper(T initialValue, T finalValue, float duration = 1)
    {
        this.timeElapsed = 0;
        this._value = this.initialValue;
        this.initialValue = initialValue;
        this.finalValue = finalValue;
        this.duration = duration;
    }

    protected abstract T GetValue(float timeElapsed);
    protected virtual float GetTime()
    {
        timeElapsed += Time.deltaTime;
        float time = timeElapsed / duration;
        if (curve != null)
        {
            time = curve.Evaluate(time);
        }
        return time;
    }

    public void Update()
    {
        if (IsFinished()) return;
        float time = GetTime();
        _value = GetValue(time);
    }

    public bool IsFinished()
    {
        return timeElapsed >= duration;
    }

    public void Revert()
    {
        var initialValue = this.initialValue;
        var finalValue = this.finalValue;
        this.finalValue = initialValue;
        this.initialValue = finalValue;
        Reset();
    }

    public void Reset()
    {
        timeElapsed = 0;
        _value = initialValue;
    }
}

public class QuaternionLerp : Lerper<Quaternion>
{
    public QuaternionLerp(Quaternion initialValue, Quaternion finalValue, float duration = 1)
        : base(initialValue, finalValue, duration) { }

    protected override Quaternion GetValue(float timeElapsed)
    {
        return Quaternion.Slerp(initialValue, finalValue, timeElapsed);
    }
}

public class NumberLerp : Lerper<float>
{
    public NumberLerp(float initialValue, float finalValue, float duration = 1)
        : base(initialValue, finalValue, duration) { }

    protected override float GetValue(float timeElapsed)
    {
        return Mathf.Lerp(initialValue, finalValue, timeElapsed);
    }
}

/**
* EXPERIMENTAL: Not tested yet
*/
public class ValueLerp : Lerper<float>
{
    public ValueLerp(float initialValue, float finalValue, float duration = 1)
        : base(initialValue, finalValue, duration) { }

    protected override float GetValue(float timeElapsed)
    {
        return Mathf.MoveTowards(base.value, base.finalValue, timeElapsed);
    }
    protected override float GetTime()
    {
        return (base.finalValue / base.duration) * Time.deltaTime;
    }
}


public class Vector3Lerp : Lerper<Vector3>
{
    public Vector3Lerp(Vector3 initialValue, Vector3 finalValue, float duration = 1)
        : base(initialValue, finalValue, duration) { }

    protected override Vector3 GetValue(float timeElapsed)
    {
        return Vector3.Lerp(initialValue, finalValue, timeElapsed);
    }
}

public class Vector2Lerp : Lerper<Vector2>
{
    public Vector2Lerp(Vector2 initialValue, Vector2 finalValue, float duration = 1)
        : base(initialValue, finalValue, duration) { }

    protected override Vector2 GetValue(float timeElapsed)
    {
        return Vector2.Lerp(initialValue, finalValue, timeElapsed);
    }
}



public class ColorLerp : Lerper<Color>
{
    public ColorLerp(Color initialValue, Color finalValue, float duration = 1)
        : base(initialValue, finalValue, duration) { }

    protected override Color GetValue(float timeElapsed)
    {
        return Color.Lerp(initialValue, finalValue, timeElapsed);
    }
}