using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if(transitionName == SceneManagement.Instance.SceceTransitionName)//Nếu trùng với tên cổng của scene bên kia
        {
            PlayerController.Instance.transform.position = this.transform.position;//Lấy vị trí của AreaEntrance để gán cho player
            CameraController.Instance.SetPlayerCameraFollow();

            UIFade.Instance.FadeToClear();
        }
    }
}
