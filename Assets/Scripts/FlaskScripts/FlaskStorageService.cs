using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class FlaskStorageService : MonoBehaviour
{
    private Bot[] _bots;

    private void OnEnable()
    {
        GlobalEvents.OnBotsInitialized.AddListener(SaveInitializedFlasks);
    }
    private void OnDisable()
    {
        GlobalEvents.OnBotsInitialized.RemoveListener(SaveInitializedFlasks);
    }

    private void SaveInitializedFlasks(Bot[] bots)
    {
        _bots = bots;
    }

    public void RestartFlask()
    {
        foreach (var bot in _bots)
        {
            bool isNeedToMoveBot = !bot.SpawnedBot.GetComponentsInParent<Transform>()[1].Equals(bot.ParentPosition);
            bot.SpawnedBot.transform.SetParent(bot.ParentPosition);

            var agent = bot.SpawnedBot.GetComponent<NavMeshAgent>();
            var animator = bot.SpawnedBot.GetComponent<Animator>();
            agent.SetDestination(bot.ParentPosition.position);
            if (!animator.GetBool("IsRunning") && isNeedToMoveBot)
                animator.SetBool("IsRunning", true);
        }

        var flasks = GameObject.FindGameObjectsWithTag("Flask");
        foreach (var flask in flasks)
        {
            flask.GetComponent<FlaskController>().InitializeComponent();
        }

        GetComponent<FinishGameHandler>().CurrentFilledFlaskCount = 0;
    }
}
