using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundsUI : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject SoundToggleOnGameObject;
    [SerializeField] private GameObject SoundToggleOffGameObject;


    private void Start()
    {
        audioMixer.GetFloat("Volume", out float volumeValue);
        if (volumeValue == -80f)
        {
            SoundToggleOnGameObject.SetActive(false);
            SoundToggleOffGameObject.SetActive(true);
        }
        else
        {
            SoundToggleOnGameObject.SetActive(true);
            SoundToggleOffGameObject.SetActive(false);
        }
    }

    public void DisableSound()
    {
        audioMixer.SetFloat("Volume", -80f);
        SoundToggleOffGameObject.SetActive(true);
        SoundToggleOnGameObject.SetActive(false);
    }
    public void EnableSound()
    {
        audioMixer.SetFloat("Volume", 0f);
        SoundToggleOffGameObject.SetActive(false);
        SoundToggleOnGameObject.SetActive(true);
    }
}
