using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlaskController : MonoBehaviour
{
    private Stack<Color> colors = new Stack<Color>();
    private Stack<Transform> flaskPositions = new Stack<Transform>();

    private GameObject flaskPlane;

    public GameObject FlaskPlane { get => flaskPlane; }
    public Stack<Color> Colors { get => colors; }

    private void Awake()
    {
        GlobalEvents.OnBotsInitialized.AddListener(InitializeComponent);

    }
    private void OnDisable()
    {
        GlobalEvents.OnBotsInitialized.RemoveListener(InitializeComponent);
    }

    private void InitializeComponent()
    {
        InitializeStackColor();
        InitializeFlaskPositions();
        GlobalEvents.SendFlaskControllerInitialized();
    }

    private void InitializeFlaskPositions()
    {
        var positions = transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i <= positions.Length-1; i++)
        {
            flaskPositions.Push(positions[i]);
        }
    }

    private void InitializeStackColor()
    {
        var bots = transform.GetComponentsInChildren<NavMeshAgent>();
        for (int i = 0; i < bots.Length; i++)
        {
            colors.Push(bots[i].GetComponent<MeshRenderer>().material.color);
        }
        flaskPlane = transform.Find("Plane").gameObject;
    }
}
