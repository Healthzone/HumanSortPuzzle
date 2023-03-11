using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotInitializer : MonoBehaviour
{
    [SerializeField] private GameObject botPrefab;

    private Colors[] botGeneratedColors;
    private Bot[] bots;

    private Material[] colors;

    public void Initialize(GameObject[] flasks)
    {
        colors = GetComponent<ColorConstants>().BotsMaterial;
        bots = new Bot[(flasks.Length - 2) * 4];

        for (int i = 0; i < bots.Length; i++)
        {
            bots[i] = new Bot();
        }

        //Случайно определяем две пустые колбы
        #region Опредеяем пустые колбы
        System.Random rnd = new System.Random();
        int emptyFlask1 = rnd.Next(0, flasks.Length);
        int emptyFlask2;
        do
        {
            emptyFlask2 = rnd.Next(0, flasks.Length);
        }
        while (emptyFlask1 == emptyFlask2);
        #endregion

        GenerateRandomColors(flasks.Length - 2);
        int flaskCount = 0;
        for (int i = 0; i < flasks.Length; i++)
        {
            if (i == emptyFlask1 || i == emptyFlask2)
                continue;
            IntstantiateBots(flasks[i], flaskCount);
            flaskCount++;
        }
        SetBotColor();
        GlobalEvents.SendBotsInitialized();
    }

    private void IntstantiateBots(GameObject flask, int flaskCount)
    {
        //Получаем позиции колбы для расстановки
        var spawnPositions = flask.GetComponentsInChildren<Transform>();

        for (int i = 1; i <= spawnPositions.Length - 1; i++)
        {
            //Расставляем ботов по колбам
            bots[i - 1 + (flaskCount * 4)].SpawnedBot = Instantiate(botPrefab, spawnPositions[i].position, Quaternion.identity, spawnPositions[i]);
        }
    }

    private void GenerateRandomColors(int flaskCount)
    {
        RandomBotColorGenerator colorGenerator = new RandomBotColorGenerator(flaskCount);
        botGeneratedColors = colorGenerator.GenerateRandomColor();

        for (int i = 0; i < botGeneratedColors.Length; i++)
        {
            bots[i].BotColor = botGeneratedColors[i];
        }
    }

    private void SetBotColor()
    {
        foreach (var bot in bots)
        {
            //MaterialPropertyBlock propertyBlock = 
            bot.SpawnedBot.GetComponent<Renderer>().sharedMaterial = SelectColorConstant(bot.BotColor);
        }
    }

    private Material SelectColorConstant(Colors color)
    {
        switch (color)
        {
            case Colors.Red:
                return colors[0];
            case Colors.Green:
                return colors[1];
            case Colors.Blue:
                return colors[2];
            case Colors.Yellow:
                return colors[3];
            case Colors.Pink:
                return colors[4];
            case Colors.Orange:
                return colors[5];
            case Colors.Cyan:
                return colors[6];
            default:
                throw new Exception("Не удалось определить материал бота");
        }
    }
}
