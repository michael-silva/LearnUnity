using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsInput : MonoBehaviour
{
    private PlayerBase player;
    [SerializeField] private float turnSpeed = 8f;

    void Start()
    {
        player = GetComponent<PlayerBase>();
        GameInput.Instance.OnPressMove.AddListener(HandleRotation);
    }

    void HandleRotation(Vector2 movement)
    {
        if (!PlayerManager.Instance.IsPlayerActive(player.gameObject)) return;
        if (movement == Vector2.zero) return;
        var targetRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
