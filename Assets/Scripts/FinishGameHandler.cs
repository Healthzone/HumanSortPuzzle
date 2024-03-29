using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using YG;

public class FinishGameHandler : MonoBehaviour
{
    [SerializeField] private GameObject canvasChanger;

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
        if (currentFilledFlaskCount == flaskWithColorCount)
        {
            int animIndex = UnityEngine.Random.Range(0, 4);
            YandexGame.savesData.currentLevel++;
            YandexGame.SaveProgress();
            if (YandexGame.auth)
                YandexGame.NewLeaderboardScores("topPlayers1", YandexGame.savesData.currentLevel - 1);
            GlobalEvents.SendLevelEnd(animIndex);
            canvasChanger.SetActive(false);
        }
    }

    private void Initialize()
    {
        flaskWithColorCount = GetComponent<FlaskInitializer>().FilledFlask;
    }
}
