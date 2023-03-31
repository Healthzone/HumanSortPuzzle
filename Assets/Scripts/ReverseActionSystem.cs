using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Example;

public class ReverseActionSystem : MonoBehaviour
{
    private const int listSize = 5;
    private LinkedList<ReverseElement> _reverseElements = new LinkedList<ReverseElement>();

    [Header("Reverse action sprites")]
    [SerializeField] private Sprite reverse3Action;
    [SerializeField] private Sprite reverse2Action;
    [SerializeField] private Sprite reverse1Action;
    [SerializeField] private Sprite reverse0ActionAds;
    [SerializeField] private Sprite reverse0Action;
    [SerializeField] private Image imageReverseAction;

    private int reverseActionsCount = 2;
    private bool watchedAds;

    private void OnEnable() => YandexGame.RewardVideoEvent += ReverseRewarded;

    private void OnDisable() => YandexGame.RewardVideoEvent -= ReverseRewarded;

    public void SaveAction(ReverseElement reverseElement)
    {
        _reverseElements.AddFirst(reverseElement);
        CheckListSize();
    }

    private void CheckListSize()
    {
        if (_reverseElements.Count > listSize)
        {
            _reverseElements.RemoveLast();
        }
    }

    public void ReverseAction()
    {
        if (reverseActionsCount == 0 && !watchedAds)
        {
            YandexGame.RewVideoShow(1);
            return;
        }
        if (_reverseElements.Count != 0)
        {
            if (reverseActionsCount == 0 && watchedAds)
                return;

            if (reverseActionsCount != 0)
                reverseActionsCount--;

            ChangeReverseSprite();
            ReverseElement reverseElement = _reverseElements.First.Value;
            _reverseElements.RemoveFirst();

            FlaskController currentController = reverseElement.Bots[0].GetComponentInParent<FlaskController>();
            if (currentController.IsFilledByOneColor)
            {
                currentController.IsFilledByOneColor = false;
                currentController.GetComponent<MeshRenderer>().material.color = new Color(0.7830188f, 0.7830188f, 0.7830188f);
                GetComponent<FinishGameHandler>().CurrentFilledFlaskCount--;
            }

            for (int i = 0; i < reverseElement.Bots.Count; i++)
            {
                var bot = currentController.Bots.Pop();
                currentController.Colors.Pop();
                currentController.ShiftNextPositionIndex(0);
                reverseElement.PreviousFlask.ProcessBotPosition(bot);
            }
        }
    }
    private void ReverseRewarded(int id)
    {
        if (id == 1)
        {
            reverseActionsCount = 3;
            imageReverseAction.sprite = reverse3Action;
            imageReverseAction.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            watchedAds = true;
        }
    }

    private void ChangeReverseSprite()
    {
        if (reverseActionsCount == 2)
            imageReverseAction.sprite = reverse2Action;
        if (reverseActionsCount == 1)
            imageReverseAction.sprite = reverse1Action;
        if (reverseActionsCount == 0 && !watchedAds)
        {
            imageReverseAction.sprite = reverse0ActionAds;
            imageReverseAction.GetComponent<RectTransform>().sizeDelta = new Vector2(87, 80);
        }
        if (reverseActionsCount == 0 && watchedAds)
            imageReverseAction.sprite = reverse0Action;

    }
    public void ResetReverseActionCount()
    {
        reverseActionsCount = 2;
        imageReverseAction.sprite = reverse2Action;
        imageReverseAction.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
        _reverseElements.Clear();
    }
}
