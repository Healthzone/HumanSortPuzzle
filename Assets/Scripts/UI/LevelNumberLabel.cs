using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class LevelNumberLabel : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += SetLevelLabel;

    private void OnDisable() => YandexGame.GetDataEvent -= SetLevelLabel;

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
            SetLevelLabel();
    }

    private void SetLevelLabel()
    {
        GetComponent<TextMeshProUGUI>().text = YandexGame.savesData.currentLevel.ToString();
    }
}
