using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public Sprite soundOn, soundOff;
    private bool isOn = true;
    public Button button;
    public void ButtonClicked()
    {
        if (isOn)
        {
            button.image.sprite = soundOff;
            isOn = false;
        }
        else
        {
            button.image.sprite = soundOn;
            isOn = true;
        }
    }

}
