using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LoadingSystem : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private float fadeDuration;
    [SerializeField] private int difficultDelimeter = 5;
    private int calculatedFlaskCount;

    private FlaskInitializer flaskInitializer;
    private void OnEnable()
    {
        GlobalEvents.OnFlaskControllerInitialized.AddListener(DisableLoadingPanel);
        YandexGame.GetDataEvent += StartGame;
    }

    private void OnDisable() => YandexGame.GetDataEvent -= StartGame;

    private void Awake()
    {
        loadingPanel.SetActive(true);
    }

    private void StartGame()
    {
        if (YandexGame.savesData.currentLevel >= 1 && YandexGame.savesData.currentLevel <= 4)
        {
            calculatedFlaskCount = 5;
        }
        else
        {
            calculatedFlaskCount = 5 + YandexGame.savesData.currentLevel / difficultDelimeter;
        }

        flaskInitializer = GetComponent<FlaskInitializer>();
        flaskInitializer.FlaskCount = calculatedFlaskCount;
        flaskInitializer.InitializeFlasks();

    }

    private void DisableLoadingPanel()
    {
        loadingPanel.GetComponent<CanvasGroup>().DOFade(0f, fadeDuration).OnComplete(() =>
        {
            loadingPanel.SetActive(false);
        });
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
            StartGame();
    }
}
