using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBotColorGenerator
{
    private int _botsCount;

    private Colors[] _botColors;

    public RandomBotColorGenerator(int flaskCount)
    {
        _botsCount = flaskCount * 4;
        _botColors = new Colors[_botsCount];
    }

    public Colors[] GenerateRandomColor()
    {

        for (int i = 0; i < _botsCount; i++)
        {
            _botColors[i] = SelectCurrentColor(i);
            //Debug.Log($"Bot {i} has {botColors[i]} color");
        }
        ShuffleArray();
        return _botColors;
    }

    private Colors SelectCurrentColor(int indexElement)
    {
        int colorIndex = indexElement / 4;
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
            case 7:
                return Colors.Purple;
            case 8:
                return Colors.White;
            case 9:
                return Colors.Grey;
            case 10:
                return Colors.PaleBlue;
            case 11:
                return Colors.Lime;
            case 12:
                return Colors.DarkModerateOrange;
            default:
                throw new Exception("Ошибка определения цвета");
        }
    }

    private void ShuffleArray()
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < _botColors.Length - 2; i++)
        {
            int newIndex = i + rnd.Next(_botColors.Length - i);
            SwapElements(i, newIndex);
        }
    }

    private void SwapElements(int oldIndex, int newIndex)
    {
        var temp = _botColors[oldIndex];
        _botColors[oldIndex] = _botColors[newIndex];
        _botColors[newIndex] = temp;
    }

}
