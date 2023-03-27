using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameHandler : MonoBehaviour
{
    private int flaskWithColorCount;
    private int currentFilledFlaskCount = 0;

    public int CurrentFilledFlaskCount { get => currentFilledFlaskCount; set => currentFilledFlaskCount = value; }

    private void OnEnable()
    {
        GlobalEvents.OnFlasksInitialized.AddListener(Initialize);
        GlobalEvents.OnFlaskFilledByOneColor.AddListener(HandleFilledFlask);
    }
    private void OnDisable()
    {
        GlobalEvents.OnFlasksInitialized.RemoveListener(Initialize);
        GlobalEvents.OnFlaskFilledByOneColor.RemoveListener(HandleFilledFlask);
    }

    private void HandleFilledFlask()
    {
        currentFilledFlaskCount++;
        Debug.Log(currentFilledFlaskCount);
        if (currentFilledFlaskCount == flaskWithColorCount)
            Debug.Log("������� �������");
    }

    private void Initialize()
    {
        flaskWithColorCount = GetComponent<FlaskInitializer>().FilledFlask;
        //currentFilledFlaskCount = 0;
    }
}