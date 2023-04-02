using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ReviewPanel : MonoBehaviour
{
    [SerializeField] private GameObject reviewPanel;

    private bool isEditorMode;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += TryEnableReviewPanel;
        YandexGame.ReviewSentEvent += CloseReviewPanel;
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= TryEnableReviewPanel;
        YandexGame.ReviewSentEvent -= CloseReviewPanel;
    }

    private void TryEnableReviewPanel()
    {
        if ((YandexGame.savesData.currentLevel == 10 || YandexGame.savesData.currentLevel > 10)
            && (YandexGame.EnvironmentData.reviewCanShow || isEditorMode)
            && !YandexGame.savesData.isSuggestedReview)
            GlobalEvents.OnLevelEnd.AddListener(ActivateReviewPanel);
    }

    private void ActivateReviewPanel(int arg0)
    {
        reviewPanel.SetActive(true);
    }

    private void Start()
    {
#if UNITY_EDITOR
        isEditorMode = true;
#endif
        if (YandexGame.SDKEnabled)
        {
            TryEnableReviewPanel();
        }
    }

    public void CloseReviewPanel(bool sent)
    {
        reviewPanel.SetActive(false);
        YandexGame.savesData.isSuggestedReview = true;
        YandexGame.SaveProgress();
    }
    public void Review()
    {
        YandexGame.ReviewShow(YandexGame.EnvironmentData.reviewCanShow);
    }

}
