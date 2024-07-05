using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager IM;
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }

    public KeyCode fireForward { get; set; }
    public KeyCode fireBackward { get; set; }
    public KeyCode fireLeft { get; set; }
    public KeyCode fireRight { get; set; }

    void Awake()
    {
        //Singleton pattern
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if (IM != this)
        {
            Destroy(gameObject);
        }
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

        fireForward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireForwardKey", "UpArrow"));
        fireBackward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireBackwardKey", "DownArrow"));
        fireRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireRightKey", "RightArrow"));
        fireLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireLeftKey", "LeftArrow"));
    }
}
