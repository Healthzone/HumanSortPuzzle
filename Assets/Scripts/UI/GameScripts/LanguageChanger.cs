using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LanguageChanger : MonoBehaviour
{
    [SerializeField] private YandexGame yandexGame;
    [SerializeField] private GameObject ruLanguageButton;
    [SerializeField] private GameObject enLanguageButton;
    public void ChangeLanguage(string lang)
    {
        switch (lang)
        {
            case "ru":
                yandexGame._SwitchLanguage(lang);
                enLanguageButton.SetActive(false);
                ruLanguageButton.SetActive(true);
                Debug.Log(YandexGame.savesData.language);
                break;
            case "en":
                yandexGame._SwitchLanguage(lang);
                enLanguageButton.SetActive(true);
                ruLanguageButton.SetActive(false);
                Debug.Log(YandexGame.savesData.language);
                break;
            default:
                enLanguageButton.SetActive(false);
                ruLanguageButton.SetActive(true);
                yandexGame._SwitchLanguage("Ru");
                break;
        }
    }
}
