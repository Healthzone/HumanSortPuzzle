using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FlaskInitializer : MonoBehaviour
{
    [SerializeField] private int flaskCount;
    [SerializeField] private int flaskRowCount;

    [Header("Prefabs")]
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject flaskPrefab;

    [Header("Flask offset")]
    [SerializeField] private float offsetX = 5.75f;
    [SerializeField] private float offsetY = 0.25f;
    [SerializeField] private float offsetZ = -15f;

    [SerializeField] private GameObject[] spawnedFlasks;

    public int FlaskCount { get => flaskCount;}

    void Start()
    {
        InitializeFlasks();
    }

    private void InitializeFlasks()
    {
        spawnedFlasks = new GameObject[flaskCount];

        GameObject spawnedGround = Instantiate(groundPrefab, Vector3.zero, Quaternion.identity);

        
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
            spawnedFlasks[i] = Instantiate(flaskPrefab, spawnPosition, flaskPrefab.transform.rotation, spawnedGround.transform);
        }
        GlobalEvents.SendFlaskInitialized();
        StartInitializingCamera(spawnedFlasks);
        BuildNavMeshPath(spawnedGround);
        StartInitializingBots(spawnedFlasks);
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
