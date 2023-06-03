using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponentManual : CharacterComponent
{
    private void Start()
    {
    }

    private void LateUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        if (character.applyRootMotion) return;

        float yMovement = Mathf.Abs(character.velocity.y) * Time.deltaTime;
        if (character.velocity.y < 0 && Physics.Raycast(transform.position, Vector3.down, out var hit, yMovement + 0.1f))
        {
            character.isGrounded = true;
            transform.position = hit.point;
            // transform.position = new Vector3(0, hit.point.y, 0);
            character.velocity.y = 0;
        }
        else
        {
            character.isGrounded = character.velocity.y == 0;
        }

        // transform.Translate(Vector3.forward * normalVelocity * currentSpeed * Time.deltaTime);
        transform.position += character.velocity * Time.deltaTime;
    }
}