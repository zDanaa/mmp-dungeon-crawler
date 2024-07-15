using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    AudioSource new_audio;
    public Slider slider;
    // Start is called before the first frame update
    public void Awake()
    {

        new_audio = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        new_audio.volume = slider.value;
    }

    public void ToggleMusic()
    {
        new_audio.mute = !new_audio.mute;
    }
}