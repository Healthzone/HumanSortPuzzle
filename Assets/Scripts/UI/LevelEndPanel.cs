using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPanel : MonoBehaviour
{
    [SerializeField] private GameObject LevelEndPanelGameObject;
    private void OnEnable()
    {
        GlobalEvents.OnLevelEnd.AddListener(ShowLevelEndPanel);
    }

    private void ShowLevelEndPanel(int arg0)
    {
        LevelEndPanelGameObject.SetActive(true);
    }
}
