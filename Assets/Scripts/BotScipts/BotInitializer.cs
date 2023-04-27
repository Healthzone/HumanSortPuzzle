using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInitializer : MonoBehaviour
{
    [SerializeField] private GameObject botPrefab;

    private Colors[] botGeneratedColors;
    private Bot[] bots;

    private Material[] colors;

    private int emptyFlask1;
    private int emptyFlask2;
    private int? emptyFlask3;

    public void Initialize(GameObject[] flasks)
    {
        var colorsConstant = GetComponent<ColorConstants>();
        colors = colorsConstant.BotsMaterial;
        bots = new Bot[(flasks.Length - FlaskInitializer.emptyFlaskCount) * 4];

        for (int i = 0; i < bots.Length; i++)
        {
            bots[i] = new Bot();
        }

        //Случайно определяем индексы пустых колб
        if (FlaskInitializer.emptyFlaskCount == 2)
        {
            System.Random rnd = new System.Random();
            emptyFlask1 = rnd.Next(0, flasks.Length);
            do
            {
                emptyFlask2 = rnd.Next(0, flasks.Length);
            }
            while (emptyFlask1 == emptyFlask2);
        }
        else
        {
            System.Random rnd = new System.Random();
            emptyFlask1 = rnd.Next(0, flasks.Length);
            do
            {
                emptyFlask2 = rnd.Next(0, flasks.Length);
                emptyFlask3 = rnd.Next(0, flasks.Length);
            }
            while ((emptyFlask1 == emptyFlask2) || (emptyFlask1 == emptyFlask3) || (emptyFlask2 == emptyFlask3));
        }

        GenerateRandomColors(flasks.Length - FlaskInitializer.emptyFlaskCount);
        int flaskCount = 0;
        for (int i = 0; i < flasks.Length; i++)
        {
            if (i == emptyFlask1 || i == emptyFlask2 || i == emptyFlask3)
                continue;
            IntstantiateBots(flasks[i], flaskCount);
            flaskCount++;
        }
        SetBotColor();
        GlobalEvents.SendBotsInitialized(bots);
    }

    private void IntstantiateBots(GameObject flask, int flaskCount)
    {
        //Получаем позиции колбы для расстановки
        var spawnPositions = flask.GetComponentsInChildren<Transform>();

        for (int i = 1; i <= spawnPositions.Length - 1; i++)
        {
            //Расставляем ботов по колбам
            bots[i - 1 + (flaskCount * 4)].SpawnedBot = Instantiate(botPrefab, spawnPositions[i].position, Quaternion.Euler(0f, 180f, 0f), spawnPositions[i]);
            bots[i - 1 + (flaskCount * 4)].ParentPosition = spawnPositions[i];
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
            bot.SpawnedBot.GetComponentInChildren<SkinnedMeshRenderer>().material = null;
            bot.SpawnedBot.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = null;
            bot.SpawnedBot.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = SelectColorConstant(bot.BotColor);
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
            case Colors.Purple:
                return colors[7];
            case Colors.White:
                return colors[8];
            case Colors.Grey:
                return colors[9];
            case Colors.PaleBlue:
                return colors[10];
            case Colors.Lime:
                return colors[11];
            case Colors.DarkModerateOrange:
                return colors[12];
            default:
                throw new Exception("Не удалось определить материал бота");
        }
    }
}
