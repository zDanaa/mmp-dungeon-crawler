using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    //Boss music
    public GameObject bossMonster;
    private bool bossMusicOn = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("GameMusic");
    }

    //Boss Music
    private void Update()
    {
        //Debug.Log("Update called. BossMusicOn: " + bossMusicOn + ", IsBossActive: " + IsBossActive());
        if (!bossMusicOn && IsBossActive())
        {
            //Debug.Log("Boss is active. Switching to Boss Music.");
            PlayMusic("BossAMusic");
            bossMusicOn = true;
        }
        else if (bossMusicOn && !IsBossActive())
        {
            //Debug.Log("Boss is not active. Switching to Game Music.");
            PlayMusic("GameMusic");
            bossMusicOn = false;
        }
    }


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.musicName == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.loop = true;  // Setze den Clip auf Loop
            musicSource.Play();
            //Debug.Log("Playing Music: " + name);
        }
    }

    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.musicName == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.loop = true;
            musicSource.Pause();
        }
    }


    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.musicName == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }


    private bool IsBossActive()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("BossA");
        if (boss != null)
        {
            //Debug.Log("Boss Monster Found: " + boss.name);
            return true;
        }
        else
        {
            Debug.Log("Boss Monster Not Found");
            return false;
        }
    }

}
