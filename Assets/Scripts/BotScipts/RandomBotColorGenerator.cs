using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBotColorGenerator
{
    private int _botsCount;

    private Colors[] botColors;

    public RandomBotColorGenerator(int flaskCount)
    {
        _botsCount = flaskCount * 4;
        botColors = new Colors[_botsCount];
    }

    public void GenerateRandomColor()
    {

        for (int i = 0; i < _botsCount; i++)
        {
            botColors[i] = SelectCurrentColor(i);
            Debug.Log($"Bot {i} has {botColors[i]} color");
        }
    }

    private Colors SelectCurrentColor(int indexElement)
    {
        int colorIndex = indexElement / (_botsCount / 4);
        switch (colorIndex)
        {
            case 0:
                return Colors.Red;
            case 1:
                return Colors.Green;
            case 2:
                return Colors.Blue;
            case 3:
                return Colors.Yellow;
            case 4:
                return Colors.Pink;
            case 5:
                return Colors.Orange;
            case 6:
                return Colors.Cyan;
            default:
                throw new Exception("Ошибка определения цвета");
        }
    }

    private Colors[] ShuffleArray()
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < botColors.Length - 2; i++)
        {
            int newIndex = i + rnd.Next(botColors.Length - i);
            SwapElements(i, newIndex);
        }
        return botColors;
    }

    private void SwapElements(int oldIndex, int newIndex)
    {
        var temp = botColors[oldIndex];
        botColors[oldIndex] = botColors[newIndex];
        botColors[newIndex] = temp;
    }

}
