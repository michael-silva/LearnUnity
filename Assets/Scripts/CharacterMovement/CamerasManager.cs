using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CamerasManager : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras;
    [SerializeField] private int cameraActiveIndex = 0;

    private void Start()
    {
        DeactivateAllCamera();
        ActivateCamera(cameraActiveIndex);
        PlayerManager.Instance.OnChangeActivePlayer.AddListener(UpdateCameraTarget);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DeactivateCamera(cameraActiveIndex);
            cameraActiveIndex = (cameraActiveIndex + 1) % virtualCameras.Count;
            ActivateCamera(cameraActiveIndex);
        }
    }

    private void UpdateCameraTarget()
    {
        var player = PlayerManager.Instance.GetActivePlayer();
        virtualCameras[cameraActiveIndex].Follow = player.transform;
        if (cameraActiveIndex != 0)
        {
            var character = player.GetComponent<CharacterComponent>().character;
            character.moveInCameraSpace = true;
        }
    }

    private void ActivateCamera(int index)
    {
        virtualCameras[index].gameObject.SetActive(true);
        cameraActiveIndex = index;
        UpdateCameraTarget();
    }

    private void DeactivateCamera(int index)
    {
        virtualCameras[index].gameObject.SetActive(false);
    }

    private void DeactivateAllCamera()
    {
        for (int i = 0; i < virtualCameras.Count; i++)
        {
            DeactivateCamera(i);
        }
    }
}
