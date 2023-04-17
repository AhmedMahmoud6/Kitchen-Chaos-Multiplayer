using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float fieldOfViewMin = 5f;
    [SerializeField] private float fieldOfViewMax = 50;


    private float targetFieldOfView = 20;

    private void Update()
    {
        HandleCameraZoom();
    }



    private void HandleCameraZoom()
    {
        float fieldOfViewIncreasAmount = 5f;
        if(Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= fieldOfViewIncreasAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += fieldOfViewIncreasAmount;
        }
        targetFieldOfView = Mathf.Clamp(targetFieldOfView,fieldOfViewMin,fieldOfViewMax);

        float zoomSpeed = 5f;
        cinemachineVirtualCamera.m_Lens.FieldOfView = 
            Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
        
    }
}
