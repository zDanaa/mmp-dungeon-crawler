using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    AudioSource audio;
    float musicVolume = 1f;
    // Start is called before the first frame update
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        audio = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        audio.volume = musicVolume;

    }

    public void SetVolume(float volume)
    {
        musicVolume = volume;
    }
}
