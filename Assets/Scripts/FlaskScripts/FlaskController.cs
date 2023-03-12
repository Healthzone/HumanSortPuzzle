using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlaskController : MonoBehaviour
{
    private Stack<Color> colors = new Stack<Color>();
    private Queue<Transform> flaskPositions = new Queue<Transform>();
    private Stack<GameObject> bots = new Stack<GameObject>();

    private GameObject flaskPlane;

    public GameObject FlaskPlane { get => flaskPlane; }
    public Stack<Color> Colors { get => colors; }
    public Stack<GameObject> Bots { get => bots; }
    public Queue<Transform> FlaskPositions { get => flaskPositions; set => flaskPositions = value; }

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
        InitializeBotPositions();
        GlobalEvents.SendFlaskControllerInitialized();
    }

    private void InitializeBotPositions()
    {
        for (int i = 0; i < gameObject.transform.childCount - 1; i++)
        {
            if (gameObject.transform.GetChild(i).childCount == 0)
                continue;
            GameObject child = gameObject.transform.GetChild(i).GetChild(0).gameObject;
            bots.Push(child);
        }
    }

    private void InitializeFlaskPositions()
    {
        var positions = transform.GetComponentsInChildren<Transform>();
        if (gameObject.transform.GetChild(0).childCount != 0)
            return;
        for (int i = 1; i <= positions.Length - 1; i++)
        {
            flaskPositions.Enqueue(positions[i]);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bot")
        {

        }
    }


}
