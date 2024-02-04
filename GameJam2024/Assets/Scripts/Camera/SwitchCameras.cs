using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCameras : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;
    public CinemachineVirtualCamera primaryCamera;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("CameraTrigger"))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == ("CameraTrigger"))
        {
            SwitchToCamera(primaryCamera);
            Debug.Log("Switching to primary camera");
        }
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in virtualCameras)
        {
            camera.enabled = camera == targetCamera;
        }
    }
}
