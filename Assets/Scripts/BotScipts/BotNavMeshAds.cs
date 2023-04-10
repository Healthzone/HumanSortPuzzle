using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using YG;

public class BotNavMeshAds : MonoBehaviour
{
    [SerializeField] private Transform endPosition;

    public Transform EndPosition { get => endPosition; set => endPosition = value; }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += UpdateBotDestionation;
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= UpdateBotDestionation;
    }
    private void UpdateBotDestionation(int id)
    {
        if (id == 2)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            Debug.Log("Восстановление пути");
            agent.isStopped = false;
            agent.ResetPath();
            agent.SetDestination(endPosition.position);
        }
    }

}
