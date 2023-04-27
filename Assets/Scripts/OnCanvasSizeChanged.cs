using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCanvasSizeChanged : MonoBehaviour
{
    [SerializeField] private CameraInitializer cameraInitializer;
    [SerializeField] private bool isGameStarted;

    private void OnEnable()
    {
        isGameStarted = true;
    }

    private void OnRectTransformDimensionsChange()
    {
        if (isGameStarted && cameraInitializer.Camera != null)
        {
            cameraInitializer.ReInitializedCameraPosition();
            Debug.Log("Reinitializing camera position");
        }
    }

}
