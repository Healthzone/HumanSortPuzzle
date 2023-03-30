using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPanel : MonoBehaviour
{
    [SerializeField] private GameObject LevelEndPanelGameObject;
    [SerializeField] private float endPanelDelay = 1f;
    private void OnEnable()
    {
        GlobalEvents.OnLevelEnd.AddListener(ShowLevelEndPanel);
    }

    private void ShowLevelEndPanel(int arg0)
    {

        StartCoroutine(WaitDelayToShowEndPanel());
    }

    private IEnumerator WaitDelayToShowEndPanel()
    {
        yield return new WaitForSeconds(endPanelDelay);
        LevelEndPanelGameObject.SetActive(true);

    }
}
