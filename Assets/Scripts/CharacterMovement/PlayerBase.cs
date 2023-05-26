using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerBase : MonoBehaviour, ICharacter
{
    protected UnityEvent<Vector2> onMoving = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoving => onMoving;

}
