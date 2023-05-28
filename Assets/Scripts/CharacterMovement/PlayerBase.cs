using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerBase : MonoBehaviour, ICharacter
{
    protected UnityEvent<float> onMoving = new UnityEvent<float>();
    public UnityEvent<float> OnMoving => onMoving;

}
