using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using YG;
using YG.Example;

public class FlaskInitializer : MonoBehaviour
{
    private int flaskCount;
    [SerializeField] private int flaskRowCount;

    [Header("Prefabs")]
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject flaskPrefab;

    [Header("Flask offset")]
    [SerializeField] private float offsetX = 5.75f;
    [SerializeField] private float offsetY = 0.25f;
    [SerializeField] private float offsetZ = -15f;

    [SerializeField] private GameObject[] spawnedFlasks;

    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject addNewFlaskBtn;

    private List<Vector3> calculatedPositions = new List<Vector3>();

    private GameObject spawnedGround;

    private int filledFlask;

    public int FilledFlask { get => filledFlask; }
    public int FlaskCount { get => flaskCount; set => flaskCount = value; }
    public int FlaskRowCount { get => flaskRowCount; set => flaskRowCount = value; }
    private void OnEnable() => YandexGame.RewardVideoEvent += AddNewFlaskRewarded;

    private void OnDisable() => YandexGame.RewardVideoEvent -= AddNewFlaskRewarded;

    public void InitializeFlasks(bool isNeedToAddNewFlask = false)
    {
        filledFlask = flaskCount - 2;
        if (isNeedToAddNewFlask)
        {
            flaskCount++;
        }


        spawnedFlasks = new GameObject[flaskCount];

        if (!isNeedToAddNewFlask)
            spawnedGround = Instantiate(groundPrefab, Vector3.zero, Quaternion.identity);

        if (isNeedToAddNewFlask)
            calculatedPositions.Clear();

        Vector3 spawnPosition = Vector3.zero;

        for (int i = 0; i < flaskCount; i++)
        {
            int row = i / flaskRowCount;
            int column = i % flaskRowCount;

            //Если строка заполнена полностью колбами
            if (row < flaskCount / flaskRowCount)
            {
                //Вычисляем позицию колбы со смещением
                spawnPosition = new Vector3(offsetX * column, offsetY, row * offsetZ);
            }

            //Если последняя строка заполнена не полностью колбами
            if (flaskCount % flaskRowCount > 0 && row == flaskCount / flaskRowCount)
            {
                //Вычисляем новоем смещение по формуле
                float newOffsetX = offsetX * (flaskRowCount - 1) / ((flaskCount % flaskRowCount) + 1);

                spawnPosition = new Vector3(newOffsetX + newOffsetX * column, offsetY, row * offsetZ);

                //Если строка заполнена колбами на 1 меньше чем максимально вмещает, то вычисляем особое новое смещение для равномерного распределения
                if (flaskCount % flaskRowCount == flaskRowCount - 1)
                {
                    newOffsetX = offsetX * (flaskRowCount - 2) / ((flaskCount % flaskRowCount) - 1);
                    spawnPosition = new Vector3(newOffsetX / 2 + newOffsetX * column, offsetY, row * offsetZ);
                }

            }
            calculatedPositions.Add(spawnPosition);
        }
        InstantiateFlask(isNeedToAddNewFlask);
        GlobalEvents.SendFlaskInitialized();
        StartInitializingCamera(spawnedFlasks);
        BuildNavMeshPath(spawnedGround);
        EnableNavMeshAgentsOnBots();
        if (!isNeedToAddNewFlask)
            StartInitializingBots(spawnedFlasks);
    }

    private void EnableNavMeshAgentsOnBots()
    {
        var navAgents = spawnedGround.GetComponentsInChildren<NavMeshAgent>();
        foreach (var item in navAgents)
        {
            item.enabled = true;
        }
    }

    private void InstantiateFlask(bool isNeedToAddNewFlask)
    {
        if (isNeedToAddNewFlask)
        {
            var flaskControllers = spawnedGround.GetComponentsInChildren<FlaskController>();
            for (int i = 0; i < flaskControllers.Length; i++)
            {
                spawnedFlasks[i] = flaskControllers[i].gameObject;
                var navAgents = spawnedFlasks[i].GetComponentsInChildren<NavMeshAgent>();
                foreach (var item in navAgents)
                {
                    item.enabled = false;
                }
                spawnedFlasks[i].transform.position = calculatedPositions[i];

            }
            spawnedFlasks[flaskCount - 1] = Instantiate(flaskPrefab, calculatedPositions[calculatedPositions.Count - 1], flaskPrefab.transform.rotation, spawnedGround.transform);
            spawnedFlasks[flaskCount - 1].GetComponent<FlaskController>().InitializeComponent();
            return;
        }
        for (int i = 0; i < calculatedPositions.Count; i++)
        {
            spawnedFlasks[i] = Instantiate(flaskPrefab, calculatedPositions[i], flaskPrefab.transform.rotation, spawnedGround.transform);
        }
    }

    public void AddNewFlask()
    {

        YandexGame.RewVideoShow(2);
    }

    private void AddNewFlaskRewarded(int obj)
    {
        loadingPanel.SetActive(true);
        loadingPanel.GetComponent<CanvasGroup>().alpha = 1f;
        InitializeFlasks(true);
        addNewFlaskBtn.GetComponent<UnityEngine.UI.Button>().interactable = false;

    }
    private void StartInitializingBots(GameObject[] flasks)
    {
        BotInitializer initializer = GetComponent<BotInitializer>();
        initializer.Initialize(flasks);
    }
    private void StartInitializingCamera(GameObject[] flasks)
    {
        CameraInitializer cameraInitializer = GetComponent<CameraInitializer>();
        cameraInitializer.InitializeCameraPositionAndRotation(flasks);
    }

    private void BuildNavMeshPath(GameObject surface)
    {
        var navMeshSurf = surface.GetComponent<NavMeshSurface>();

        navMeshSurf.RemoveData();
        navMeshSurf.BuildNavMesh();
    }
}
