using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSfx();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }
    public void SfxVolume()
    {
        AudioManager.Instance.SfxVolume(sfxSlider.value);
    }
    public void SpriteChange()
    {

    }
}
