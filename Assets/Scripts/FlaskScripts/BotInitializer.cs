using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInitializer : MonoBehaviour
{
    [SerializeField] private GameObject botPrefab;

    private Colors[] botGeneratedColors;
    public void Initialize(GameObject[] flasks)
    {
        System.Random rnd = new System.Random();
        int emptyFlask1 = rnd.Next(0, flasks.Length);
        int emptyFlask2;
        do
        {
            emptyFlask2 = rnd.Next(0, flasks.Length);
        }
        while (emptyFlask1 == emptyFlask2);

        Debug.Log(emptyFlask1);
        Debug.Log(emptyFlask2);

        GenerateRandomColors(flasks.Length - 2);
        for (int i = 0; i < flasks.Length; i++)
        {
            if (i == emptyFlask1 || i == emptyFlask2)
                continue;
            IntstantiateBots(flasks[i]);
        }
    }

    private void IntstantiateBots(GameObject flask)
    {
        var spawnPositions = flask.GetComponentsInChildren<Transform>();

        for (int i = 1; i <= spawnPositions.Length - 1; i++)
        {
            Instantiate(botPrefab, spawnPositions[i].position, Quaternion.identity, spawnPositions[i]);
        }
    }

    private void GenerateRandomColors(int flaskCount)
    {
        botGeneratedColors = new Colors[flaskCount * 4];
        RandomBotColorGenerator colorGenerator = new RandomBotColorGenerator(flaskCount);
        colorGenerator.GenerateRandomColor();
    }
}
