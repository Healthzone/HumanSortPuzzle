using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LanguageButtonGOHandler : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += ChangeLanguageGameObject;

    private void OnDisable() => YandexGame.GetDataEvent -= ChangeLanguageGameObject;

    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            ChangeLanguageGameObject();
        }

    }

    private void ChangeLanguageGameObject()
    {
        if (YandexGame.savesData.language == GetComponent<Image>().sprite.name)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
