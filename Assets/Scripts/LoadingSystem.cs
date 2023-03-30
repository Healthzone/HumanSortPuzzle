using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LoadingSystem : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private float fadeDuration;
    [SerializeField] private int difficultDelimeter = 5;
    [SerializeField] private bool isFlaskCountOverride;
    [SerializeField] private int flaskCountOverride;
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
        flaskInitializer = GetComponent<FlaskInitializer>();
        if (isFlaskCountOverride)
        {
            flaskInitializer.FlaskCount = flaskCountOverride;
        }
        else
        {
            if (YandexGame.savesData.currentLevel >= 1 && YandexGame.savesData.currentLevel <= 4)
            {
                calculatedFlaskCount = 5;
            }
            else
            {
                calculatedFlaskCount = 5 + YandexGame.savesData.currentLevel / difficultDelimeter;
            }

            flaskInitializer.FlaskCount = calculatedFlaskCount;
        }
        if (flaskInitializer.FlaskCount >= 11)
            flaskInitializer.FlaskRowCount = 6;
        if (flaskInitializer.FlaskCount >= 13)
        {
            flaskInitializer.FlaskRowCount = 6;
            if (_camera.aspect > 1)
                GetComponent<CameraInitializer>().Margin = 1.2f;
            if (_camera.aspect < 1)
                GetComponent<CameraInitializer>().Margin = 0.85f;
        }
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
