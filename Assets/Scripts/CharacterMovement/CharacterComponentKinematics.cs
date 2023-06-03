using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CharacterComponentKinematics : CharacterComponent
{
    private Rigidbody rigidBody;

    private void Start()
    {
        Debug.LogWarning("The Kinematics movement don't work, it was the worst movement controller");

        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        if (character.applyRootMotion) return;
        var movement = character.moveInCameraSpace ? CameraUtils.ConvertToCameraSpace(character.velocity) : character.velocity;
        rigidBody.MovePosition(transform.position + movement * Time.fixedDeltaTime);
        character.isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.04f);
    }
}
