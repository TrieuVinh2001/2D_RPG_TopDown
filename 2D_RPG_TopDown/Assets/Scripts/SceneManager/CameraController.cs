using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public void SetPlayerCameraFollow()
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        //Gán vị tí player cho camera(tránh phải kéo thả player vào camera follow)
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}
