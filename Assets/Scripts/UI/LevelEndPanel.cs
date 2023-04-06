using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LevelEndPanel : MonoBehaviour
{
    [SerializeField] private GameObject LevelEndPanelGameObject;
    [SerializeField] private float endPanelDelay = 1f;
    [SerializeField] private float adsDelayShow = 0.5f;
    [SerializeField] private GameObject topPanel;
    [SerializeField] private GameObject bottomPanel;
    private void OnEnable()
    {
        GlobalEvents.OnLevelEnd.AddListener(ShowLevelEndPanel);
    }

    private void ShowLevelEndPanel(int arg0)
    {
        topPanel.SetActive(false);
        bottomPanel.SetActive(false);
        StartCoroutine(WaitDelayToShowEndPanel());
    }

    private IEnumerator WaitDelayToShowEndPanel()
    {
        yield return new WaitForSeconds(endPanelDelay);
        LevelEndPanelGameObject.SetActive(true);
        StartCoroutine(AdsShowDelayed());

    }

    private IEnumerator AdsShowDelayed()
    {
        yield return new WaitForSeconds(adsDelayShow);
        YandexGame.FullscreenShow();
    }
}
