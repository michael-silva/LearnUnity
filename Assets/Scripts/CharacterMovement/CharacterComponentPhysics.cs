using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterComponentPhysics : CharacterComponent
{
    private Rigidbody rigidBody;
    private float checkGroundRay = 0.1f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        if (character.applyRootMotion) return;

        var movement = character.moveInCameraSpace ? CameraUtils.ConvertToCameraSpace(character.velocity) : character.velocity;
        rigidBody.velocity = new Vector3(movement.x, rigidBody.velocity.y, movement.z);
        character.isGrounded = Physics.Raycast(transform.position, Vector3.down, checkGroundRay);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkGroundRay);
    }
}
