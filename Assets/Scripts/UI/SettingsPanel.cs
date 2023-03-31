using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject BottomPanelGameObject;
    [SerializeField] private GameObject TopPanelGameObject;
    [SerializeField] private GameObject SettingsPanelGameObject;

    public void OpenSettignsPanel()
    {
        BottomPanelGameObject.SetActive(false);
        TopPanelGameObject.SetActive(false);
        SettingsPanelGameObject.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        SettingsPanelGameObject.SetActive(false);
        BottomPanelGameObject.SetActive(true);
        TopPanelGameObject.SetActive(true);
    }
}
