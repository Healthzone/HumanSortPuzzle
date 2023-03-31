using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class DeveloperURLOpener : MonoBehaviour
{
    [SerializeField] private YandexGame yandexGame;
    public void OpenDeveloperURL(string developerURL)
    {
        yandexGame._OnURL_Yandex_DefineDomain(developerURL);
    }
}
