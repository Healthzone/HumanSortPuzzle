using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BotMoveCheck : MonoBehaviour
{
    [SerializeField] Button addNewFlaskButton;

    [SerializeField] private bool isNewFlaskAdded;
    [SerializeField] private int currentrlyMovingBotsCount = 0;

    private void OnEnable()
    {
        GlobalEvents.OnNewFlaskAdded.AddListener(NewFlaskAdded);
        GlobalEvents.OnBotMoveStarted.AddListener(BotMovingController);
    }


    private void OnDisable()
    {
        GlobalEvents.OnNewFlaskAdded.RemoveListener(NewFlaskAdded);
        GlobalEvents.OnBotMoveStarted.RemoveListener(BotMovingController);

    }
    private void BotMovingController(int offset)
    {
        if (!isNewFlaskAdded)
        {
            currentrlyMovingBotsCount += offset;
            //Debug.Log("Currently moving bots: " + currentrlyMovingBotsCount);

            if (currentrlyMovingBotsCount > 0)
            {
                addNewFlaskButton.interactable = false;
            }
            else if (currentrlyMovingBotsCount <= 0)
            {
                currentrlyMovingBotsCount = 0;
                addNewFlaskButton.interactable = true;
            }
        }
    }
    private void NewFlaskAdded()
    {
        isNewFlaskAdded = true;
    }
}
