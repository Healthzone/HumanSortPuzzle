using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using YG;

public class FlaskController : MonoBehaviour
{
    [SerializeField] private Transform[] flaskPositions = new Transform[4];
    [SerializeField] private int nextEmptyPositionIndex = -1;
    [SerializeField] private ParticleSystem flaskParticles;

    private Stack<Color> colors = new Stack<Color>();
    private Stack<GameObject> bots = new Stack<GameObject>();

    private GameObject flaskPlane;

    private bool isFilledByOneColor = false;

    private bool isLevelEnd;

    #region Properties
    public GameObject FlaskPlane { get => flaskPlane; }
    public Stack<Color> Colors { get => colors; }
    public Stack<GameObject> Bots { get => bots; }
    public Transform[] FlaskPositions { get => flaskPositions; }
    public int NextEmptyPositionIndex { get => nextEmptyPositionIndex; }
    public bool IsFilledByOneColor { get => isFilledByOneColor; set => isFilledByOneColor = value; }
    #endregion

    private void Awake()
    {
        GlobalEvents.OnBotsInitialized.AddListener(InitializeComponent);
        GlobalEvents.OnLevelEnd.AddListener(TriggerLevelEnd);
        GlobalEvents.OnLevelEnd.AddListener(PlayFlaskParticle);

    }

    private void TriggerLevelEnd(int arg0)
    {
        isLevelEnd = true;
    }

    private void OnDisable()
    {
        GlobalEvents.OnLevelEnd.RemoveListener(PlayFlaskParticle);
        GlobalEvents.OnBotsInitialized.RemoveListener(InitializeComponent);
        GlobalEvents.OnLevelEnd.RemoveListener(TriggerLevelEnd);
    }

    public void InitializeComponent(Bot[] bots = null, bool restart = false)
    {
        InitializeStackColor();
        InitializeFlaskPositions();
        InitializeBotPositions();
        isFilledByOneColor = false;
        GetComponent<MeshRenderer>().material.color = new Color(0.7830188f, 0.7830188f, 0.7830188f);
        if (YandexGame.savesData.currentLevel < 5 && YandexGame.SDKEnabled && colors.Count != 0)
            CheckFilledFlask();
        if (!restart)
            GlobalEvents.SendFlaskControllerInitialized();
    }

    private void InitializeBotPositions()
    {
        if (bots.Count != 0)
            bots.Clear();

        for (int i = 0; i < gameObject.transform.childCount - 1; i++)
        {
            if (gameObject.transform.GetChild(i).childCount == 0)
                continue;
            GameObject child = gameObject.transform.GetChild(i).GetChild(0).gameObject;
            //child.GetComponent<NavMeshAgent>().updatePosition = false;
            bots.Push(child);
        }
    }

    private void InitializeFlaskPositions()
    {
        //var positions = transform.GetComponentsInChildren<Transform>().Where(x => x.gameObject.CompareTag("Position")).ToArray();

        if (gameObject.transform.GetChild(0).childCount == 0)
            nextEmptyPositionIndex = 0;
        else
            nextEmptyPositionIndex = 4;

        //for (int i = 1; i < positions.Length; i++)
        //{
        //    flaskPositions[i - 1] = positions[i];
        //}

    }

    private void InitializeStackColor()
    {
        if (colors.Count != 0)
            colors.Clear();

        var bots = transform.GetComponentsInChildren<NavMeshAgent>();
        for (int i = 0; i < bots.Length; i++)
        {
            colors.Push(bots[i].GetComponentInChildren<SkinnedMeshRenderer>().material.color);
        }
        flaskPlane = transform.Find("Plane").gameObject;
    }

    private bool CheckStackColorFill()
    {
        Color firstColor;
        IEnumerator<Color> enumerator = colors.GetEnumerator();
        enumerator.MoveNext();
        firstColor = enumerator.Current;

        int sameColors = 1;
        while (enumerator.MoveNext())
        {
            if (firstColor != enumerator.Current)
                return false;
            sameColors++;
        }
        return sameColors == 4 ? true : false;
    }

    /// <summary>
    /// ���� ����� ���������� ���� �� ��������� ������� ������
    /// </summary>
    /// <param name="bot">������������ ���</param>
    /// <returns>���������� true, ���� ���� ��������� ����� ��� ���������� ����</returns>
    public bool ProcessBotPosition(GameObject bot)
    {
        colors.Push(bot.GetComponentInChildren<SkinnedMeshRenderer>().material.color);
        bots.Push(bot);

        Transform position = flaskPositions[nextEmptyPositionIndex];
        ShiftNextPositionIndex(1);

        bot.transform.SetParent(position.transform);

        bot.GetComponent<NavMeshAgent>().SetDestination(position.position);
        //if (bot.GetComponent<NavMeshAgent>().velocity.magnitude == 0)
        bot.GetComponent<Animator>().SetBool("IsRunning", true);
        //GlobalEvents.SendBotMoveStarts(1);
        CheckFilledFlask();

        return Bots.Count == 4 ? false : true;
    }

    private bool CheckFilledFlask()
    {
        isFilledByOneColor = CheckStackColorFill();
        if (isFilledByOneColor)
        {
            GetComponent<MeshRenderer>().material.color = Colors.Peek();
            GlobalEvents.SendFlaskFilledByOneColor();
            PlayFlaskParticle();
            return true;
        }
        return false;
    }

    private void PlayFlaskParticle(int arg0 = 0)
    {
        if (isLevelEnd)
        {
            var main = flaskParticles.main;
            main.loop = true;
        }

        flaskParticles.gameObject.SetActive(true);
        flaskParticles.Play();
    }

    /// <summary>
    /// ���� ����� �������� ��������� �� ������� ��������� 
    /// ������� ������.
    /// </summary>
    /// <param name="mode">0 - ������ �����. 1 - ���������� �����</param>
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
