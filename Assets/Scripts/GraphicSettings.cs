using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GraphicSettings : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.GetDataEvent += ChangeAntiAliasingOnPhone;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent += ChangeAntiAliasingOnPhone;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            ChangeAntiAliasingOnPhone();
    }
    private void ChangeAntiAliasingOnPhone()
    {
        if (!YandexGame.EnvironmentData.isDesktop)
            QualitySettings.antiAliasing = 0;
        Debug.Log(QualitySettings.antiAliasing);
    }
}
