using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlaskController : MonoBehaviour
{
    [SerializeField] private Color[] colors;


    private void Awake()
    {
        GlobalEvents.OnBotsInitialized.AddListener(InitializeComponent);

        colors = new Color[4];
    }
    private void OnDisable()
    {
        GlobalEvents.OnBotsInitialized.RemoveListener(InitializeComponent);
    }

    private void InitializeComponent()
    {
        var bots = transform.GetComponentsInChildren<NavMeshAgent>();
        for (int i = 0; i < bots.Length; i++)
        {
            colors[i] = bots[i].GetComponent<MeshRenderer>().material.color;
        }
        GlobalEvents.SendFlaskControllerInitialized();
    }
}
