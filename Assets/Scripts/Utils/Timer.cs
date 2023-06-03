using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Timer
{
    [SerializeField] private float currentTimer = 0;
    [SerializeField] private float duration;
    [SerializeField] private bool isRunning;

    public Timer(float duration)
    {
        this.duration = duration;
    }

    public void Tick()
    {
        currentTimer += Time.deltaTime;
    }

    public bool IsFinish()
    {
        return isRunning && currentTimer >= duration;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public void Reset()
    {
        currentTimer = 0;
    }

    public void Start()
    {
        isRunning = true;
    }

    public void Stop()
    {
        isRunning = false;
        currentTimer = 0;
    }
}


