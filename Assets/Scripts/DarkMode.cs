using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMode : MonoBehaviour
{
    [SerializeField] private Color lightModeColor;
    [SerializeField] private Color darkModeColor;

    [SerializeField] private GameObject DarkModeToggleOnGameObject;
    [SerializeField] private GameObject DarkModeToggleOffGameObject;

    [SerializeField] private Material groundMaterial;

    private void Start()
    {
        if (groundMaterial.color == darkModeColor)
        {
            DarkModeToggleOnGameObject.SetActive(false);
            DarkModeToggleOffGameObject.SetActive(true);
        }
        else if (groundMaterial.color == lightModeColor)
        {
            DarkModeToggleOnGameObject.SetActive(true);
            DarkModeToggleOffGameObject.SetActive(false);
        }

    }
    public void EnableDarkMode()
    {
        groundMaterial.color = darkModeColor;
        DarkModeToggleOnGameObject.SetActive(false);
        DarkModeToggleOffGameObject.SetActive(true);
    }
    public void EnableLightMode()
    {
        groundMaterial.color = lightModeColor;
        DarkModeToggleOnGameObject.SetActive(true);
        DarkModeToggleOffGameObject.SetActive(false);
    }
}
