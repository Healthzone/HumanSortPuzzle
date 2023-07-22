using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject BottomPanelGameObject;
    [SerializeField] private GameObject TopPanelGameObject;
    [SerializeField] private GameObject SettingsPanelGameObject;
    [SerializeField] private PlatformRaycast platformRaycast;

    public void OpenSettignsPanel()
    {
        platformRaycast.IsEnabledReycastTargetting = false;
        BottomPanelGameObject.SetActive(false);
        TopPanelGameObject.SetActive(false);
        SettingsPanelGameObject.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        platformRaycast.IsEnabledReycastTargetting = true;
        SettingsPanelGameObject.SetActive(false);
        BottomPanelGameObject.SetActive(true);
        TopPanelGameObject.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
