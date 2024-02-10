using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains Methods to Cycle between available cameras

public class CameraSwitcher : MonoBehaviour
{
    #region Fields and Properties
    public GameObject topDownCamera;
    public GameObject thirdPersonCamera;
    public float CameraMode { get; private set; }
    #endregion

    #region Initialization
    public void Start()
    {
        InitializeCameraMode();
    }
    #endregion

    #region Methods
    public void ChangeCameras()
    {
        if(topDownCamera.activeSelf)
        {
            topDownCamera.SetActive(false);
            thirdPersonCamera.SetActive(true);
            CameraMode = 1; // 1 is for Third Person Camera
        }
        else if(thirdPersonCamera.activeSelf)
        {
            topDownCamera.SetActive(true);
            thirdPersonCamera.SetActive(false);
            CameraMode = 2; //2 is for TopDown Camera
        }
    }

    public void InitializeCameraMode()
    {
        if (topDownCamera.activeSelf)
        {
            CameraMode = 2;
        }
        else if (thirdPersonCamera.activeSelf)
        {
            CameraMode = 1;
        }
    }
    #endregion
}
