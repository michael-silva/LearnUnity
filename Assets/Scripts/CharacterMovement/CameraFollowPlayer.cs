using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraFollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        PlayerManager.Instance.OnChangeActivePlayer.AddListener(UpdateCameraTarget);
    }

    private void UpdateCameraTarget()
    {
        var player = PlayerManager.Instance.GetActivePlayer();
        virtualCamera.Follow = player.transform;
    }
}
