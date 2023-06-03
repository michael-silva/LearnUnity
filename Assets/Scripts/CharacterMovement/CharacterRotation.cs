using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ICharacterComponent))]
public class CharacterRotation : MonoBehaviour
{
    private CharacterModel character;
    [SerializeField] private float turningDuration = 0.3f;
    private QuaternionLerp turningSlerp;

    private void Start()
    {
        character = GetComponent<ICharacterComponent>()?.character;
        GameInput.Instance.OnPressMove.AddListener(HandleRotation);
    }

    private void Update()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject) || turningSlerp == null) return;
        turningSlerp?.Update();
        transform.rotation = turningSlerp.value;
    }

    private void HandleRotation(Vector2 movement)
    {
        if (movement == Vector2.zero) return;
        var rotation = new Vector3(movement.x, 0, movement.y);
        if (character.moveInCameraSpace)
        {
            rotation = CameraUtils.ConvertToCameraSpace(rotation);
        }
        var targetRotation = Quaternion.LookRotation(rotation);
        if (targetRotation != turningSlerp?.finalValue)
        {
            turningSlerp = new QuaternionLerp(transform.rotation, targetRotation, turningDuration);
        }
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
