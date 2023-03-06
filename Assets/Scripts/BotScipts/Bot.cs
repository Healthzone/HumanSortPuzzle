using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot
{
    private GameObject _spawnedBot;
    private Colors _botColor;

    public Bot()
    {

    }
    public Bot(GameObject spawnedBot, Colors botColor)
    {
        _spawnedBot = spawnedBot;
        _botColor = botColor;
    }

    public Colors BotColor { get => _botColor; set => _botColor = value; }
    public GameObject SpawnedBot { get => _spawnedBot; set => _spawnedBot = value; }
}
