using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterComponent
{
    private CharacterController characterController;
    [SerializeField]
    private CharacterModel _character = new CharacterModel();
    public CharacterModel character { get { return _character; } }


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.SimpleMove(character.velocity);
        character.isGrounded = characterController.isGrounded;
        GameInput.Instance.OnStartRunning.AddListener(() => character.isRunning = true);
        GameInput.Instance.OnStopRunning.AddListener(() => character.isRunning = false);
    }

    private void LateUpdate()
    {
        if (!PlayerManager.Instance.IsPlayerActive(gameObject)) return;
        if (character.applyRootMotion) return;
        var movement = character.moveInCameraSpace ? CameraUtils.ConvertToCameraSpace(character.velocity) : character.velocity;
        characterController.Move(movement * Time.deltaTime);
        character.isGrounded = characterController.isGrounded;
    }
}
