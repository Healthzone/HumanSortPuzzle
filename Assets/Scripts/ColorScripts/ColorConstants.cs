using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorConstants : MonoBehaviour
{
    [SerializeField] private Material[] botsMaterial;

    [SerializeField] private Material[] instantitatedMaterials;

    public Material[] BotsMaterial { get => instantitatedMaterials; }

    private void Awake()
    {
        instantitatedMaterials = new Material[botsMaterial.Length];
        for (int i = 0; i < instantitatedMaterials.Length; i++)
        {
            instantitatedMaterials[i] = Instantiate(botsMaterial[i]);
        }
    }

}
