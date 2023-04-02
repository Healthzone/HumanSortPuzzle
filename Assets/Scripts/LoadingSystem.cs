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
    [SerializeField] private int difficultDelimeter = 5;
    [SerializeField] private bool isFlaskCountOverride;
    [SerializeField] private int flaskCountOverride;
    [SerializeField] private bool isLevelOvveride;
    [SerializeField] private int levelOverride;
    [SerializeField] private Animation loadingFadeAnimation;
    [SerializeField] private float preLoadingDelay = 0.5f;
    [SerializeField] private int maxFlaskCount = 15;
    private int calculatedFlaskCount;

    private FlaskInitializer flaskInitializer;

    private void OnEnable()
    {
        GlobalEvents.OnFlaskControllerInitialized.AddListener(DisableLoadingPanel);
        YandexGame.GetDataEvent += BeginLoading;
    }

    private void OnDisable() => YandexGame.GetDataEvent -= BeginLoading;

    private void BeginLoading()
    {
        StartCoroutine(LoadingDelay());
    }

    private void Awake()
    {
        loadingPanel.SetActive(true);
    }

    private IEnumerator LoadingDelay()
    {
        yield return new WaitForSeconds(preLoadingDelay);
        StartGame();
    }
    private void StartGame()
    {
        if (isLevelOvveride)
        {
            YandexGame.savesData.currentLevel = levelOverride;
            YandexGame.SaveProgress();
        }
        flaskInitializer = GetComponent<FlaskInitializer>();
        if (isFlaskCountOverride)
        {
            if (flaskCountOverride > maxFlaskCount)
            {
                flaskInitializer.FlaskCount = maxFlaskCount;
            }
            else
            {
                flaskInitializer.FlaskCount = flaskCountOverride;
            }
        }
        else
        {
            if (YandexGame.savesData.currentLevel >= 1 && YandexGame.savesData.currentLevel <= 4)
            {
                calculatedFlaskCount = 5;
                if (_camera.aspect < 1)
                    GetComponent<CameraInitializer>().Margin = 1f;
            }
            else
            {
                calculatedFlaskCount = 5 + YandexGame.savesData.currentLevel / difficultDelimeter;
            }

            if (calculatedFlaskCount > maxFlaskCount)
            {
                flaskInitializer.FlaskCount = maxFlaskCount;
            }
            else
            {
                flaskInitializer.FlaskCount = calculatedFlaskCount;
            }
        }

        if (flaskInitializer.FlaskCount >= 6)
            if (_camera.aspect < 1)
                GetComponent<CameraInitializer>().Margin = 0.9f;
        Debug.Log(_camera.aspect);
        if (flaskInitializer.FlaskCount >= 11)
            if (_camera.aspect < 1)
                GetComponent<CameraInitializer>().Margin = 0.75f;

        if (flaskInitializer.FlaskCount >= 13)
        {
            flaskInitializer.FlaskRowCount = 5;
            if (_camera.aspect > 1)
                GetComponent<CameraInitializer>().Margin = 1f;
        }
        flaskInitializer.InitializeFlasks();

    }

    private void DisableLoadingPanel()
    {
        loadingFadeAnimation.Play();
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            BeginLoading();
    }
}
