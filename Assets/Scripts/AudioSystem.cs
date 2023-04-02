using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private int currentSoundIndex = 0;

    private AudioSource audioSource;

    private void OnEnable()
    {
        GlobalEvents.OnFlaskSelected.AddListener(PlaySelectSound);
    }
    private void OnDisable()
    {
        GlobalEvents.OnFlaskSelected.RemoveListener(PlaySelectSound);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySelectSound()
    {
        audioSource.PlayOneShot(clips[currentSoundIndex]);
        currentSoundIndex++;
        if (currentSoundIndex == 22)
            currentSoundIndex = 0;
    }
}
