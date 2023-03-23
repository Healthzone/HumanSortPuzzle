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
            bot.SpawnedBot.transform.SetParent(bot.ParentPosition);
            bot.SpawnedBot.GetComponent<NavMeshAgent>().SetDestination(bot.ParentPosition.position);
        }

        var flasks = GameObject.FindGameObjectsWithTag("Flask");
        foreach (var flask in flasks)
        {
            flask.GetComponent<FlaskController>().InitializeComponent();
        }

        GetComponent<FinishGameHandler>().CurrentFilledFlaskCount = 0;
    }
}
