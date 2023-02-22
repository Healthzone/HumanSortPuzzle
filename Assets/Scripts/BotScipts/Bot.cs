using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private GameObject _spawnedBot;
    private Colors _botColor;

    public Bot(GameObject spawnedBot, Colors botColor)
    {
        _spawnedBot = spawnedBot;
        _botColor = botColor;
    }

    public Colors BotColor { get => _botColor; }
    public GameObject SpawnedBot { get => _spawnedBot; }
}
