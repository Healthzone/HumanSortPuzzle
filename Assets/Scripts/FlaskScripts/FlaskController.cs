using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FlaskController : MonoBehaviour
{
    private Stack<Color> colors = new Stack<Color>();
    [SerializeField] private Transform[] flaskPositions = new Transform[4];
    private Stack<GameObject> bots = new Stack<GameObject>();

    private GameObject flaskPlane;

    [SerializeField] private int nextEmptyPositionIndex = -1;

    public GameObject FlaskPlane { get => flaskPlane; }
    public Stack<Color> Colors { get => colors; }
    public Stack<GameObject> Bots { get => bots; }
    public Transform[] FlaskPositions { get => flaskPositions; set => flaskPositions = value; }
    public int NextEmptyPositionIndex { get => nextEmptyPositionIndex; }

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
        var positions = transform.GetComponentsInChildren<Transform>().Where(x => !x.gameObject.CompareTag("Bot")).ToArray();

        if (gameObject.transform.GetChild(0).childCount == 0)
            nextEmptyPositionIndex = 0;
        else 
            nextEmptyPositionIndex = 4;

        for (int i = 1; i < positions.Length; i++)
        {
            flaskPositions[i - 1] = positions[i];
        }

    }

    private void InitializeStackColor()
    {
        var bots = transform.GetComponentsInChildren<NavMeshAgent>();
        for (int i = 0; i < bots.Length; i++)
        {
            colors.Push(bots[i].GetComponent<Renderer>().material.color);
        }
        flaskPlane = transform.Find("Plane").gameObject;
    }

    /// <summary>
    /// Ётот метод перемещает бота на свободную позицию фласки
    /// </summary>
    /// <param name="bot">ѕеремещаемый бот</param>
    /// <returns>¬озвращает true, если есть свободное место дл€ следующего бота</returns>
    public bool ProcessBotPosition(GameObject bot)
    {
        colors.Push(bot.GetComponent<Renderer>().material.color);
        bots.Push(bot);

        Transform position = flaskPositions[nextEmptyPositionIndex];
        ShiftNextPositionIndex(1);

        bot.transform.SetParent(position.transform);

        bot.GetComponent<NavMeshAgent>().SetDestination(position.position);

        return Bots.Count == 4 ? false : true;
    }

    /// <summary>
    /// Ётот метод сдвигает указатель на текущую свободную 
    /// позицию фласки.
    /// </summary>
    /// <param name="mode">0 - зан€ть место. 1 - освободить место</param>
    public void ShiftNextPositionIndex(int mode)
    {
        if (mode == 0)
        {
            nextEmptyPositionIndex--;
            if (nextEmptyPositionIndex == -1)
                nextEmptyPositionIndex = 3;
        }
        else if (mode == 1)
        {
            nextEmptyPositionIndex++;
            if (nextEmptyPositionIndex == 4)
                nextEmptyPositionIndex = 0;
        }
    }
}
