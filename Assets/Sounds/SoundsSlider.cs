using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsSlider : MonoBehaviour
{
    private AudioSource audioSrc;
    private float musicVolume = 0.125f;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        DontDestroyOnLoad(audioSrc);
    }
    void Update()
    {
        audioSrc.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
