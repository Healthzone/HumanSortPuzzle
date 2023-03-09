using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInitializer : MonoBehaviour
{
    [SerializeField] private GameObject botPrefab;

    private Colors[] botGeneratedColors;
    private Bot[] bots;

    public void Initialize(GameObject[] flasks)
    {
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
        var spawnPositions = flask.GetComponentsInChildren<Transform>();

        for (int i = 1; i <= spawnPositions.Length - 1; i++)
        {
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
            bot.SpawnedBot.GetComponent<Renderer>().material.color = SelectColorConstant(bot.BotColor);
        }
    }

    private Color SelectColorConstant(Colors color)
    {
        switch (color)
        {
            case Colors.Red:
                return ColorConstants.RedColor;
            case Colors.Green:
                return ColorConstants.GreenColor;
            case Colors.Blue:
                return ColorConstants.BlueColor;
            case Colors.Yellow:
                return ColorConstants.YellowColor;
            case Colors.Pink:
                return ColorConstants.PinkColor;
            case Colors.Orange:
                return ColorConstants.OrangeColor;
            case Colors.Cyan:
                return ColorConstants.CyanColor;
            default:
                return ColorConstants.NoColor;
        }
    }
}
