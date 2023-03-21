using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameHandler : MonoBehaviour
{
    private int flaskWithColorCount;
    private int currentFilledFlaskCount = 0;

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
            Debug.Log("Уровень пройден");
    }

    private void Initialize()
    {
        flaskWithColorCount = GetComponent<FlaskInitializer>().FilledFlask;
        //currentFilledFlaskCount = 0;
    }
}
