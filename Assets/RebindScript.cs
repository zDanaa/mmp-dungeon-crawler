using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RebindScript : MonoBehaviour
{
    public InputManager inputManager;
    Transform menuPanel;
    Event keyEvent;
    Text ButtonText;
    KeyCode newKey;
    bool waitForKey;

    void Start()
    {
        menuPanel = transform.Find("RebindMenu");
        waitForKey = false;
        for (int i = 0; i < menuPanel.childCount; i++)
        {
            if (menuPanel.GetChild(i).name == "MoveUpKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = inputManager.forward.ToString();
            }
            else if (menuPanel.GetChild(i).name == "MoveDownKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = inputManager.backward.ToString();
            }
            else if (menuPanel.GetChild(i).name == "MoveRightKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = inputManager.right.ToString();
            }
            else if (menuPanel.GetChild(i).name == "MoveLeftKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = inputManager.left.ToString();
            }
            else if (menuPanel.GetChild(i).name == "ShootUpKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = inputManager.fireForward.ToString();
            }
            else if (menuPanel.GetChild(i).name == "ShootDownKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = inputManager.fireBackward.ToString();
            }
            else if (menuPanel.GetChild(i).name == "ShootRightKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = inputManager.fireRight.ToString();
            }
            else if (menuPanel.GetChild(i).name == "ShootLeftKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = inputManager.fireLeft.ToString();
            }

        }
    }

    void OnGUI()
    {
        keyEvent = Event.current;
        if (keyEvent.isKey && waitForKey)
        {
            newKey = keyEvent.keyCode;
            waitForKey = false;
        }
    }
    public void StartAssignment(string keyName)
    {
        if (!waitForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void SendText(Text text)
    {
        ButtonText = text;
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitForKey = true;
        yield return WaitForKey();
        switch (keyName)
        {
            case "forward":
                inputManager.forward = newKey;
                ButtonText.text = inputManager.forward.ToString();
                PlayerPrefs.SetString("forwardKey", inputManager.forward.ToString());
                break;
            case "backward":
                inputManager.backward = newKey;
                ButtonText.text = inputManager.forward.ToString();
                PlayerPrefs.SetString("backwardKey", inputManager.backward.ToString());
                break;
            case "right":
                inputManager.right = newKey;
                ButtonText.text = inputManager.right.ToString();
                PlayerPrefs.SetString("rightKey", inputManager.right.ToString());
                break;
            case "left":
                inputManager.left = newKey;
                ButtonText.text = inputManager.left.ToString();
                PlayerPrefs.SetString("leftKey", inputManager.left.ToString());
                break;
            case "fireForward":
                inputManager.fireForward = newKey;
                ButtonText.text = inputManager.fireForward.ToString();
                PlayerPrefs.SetString("fireForwardKey", inputManager.fireForward.ToString());
                break;
            case "fireBackward":
                inputManager.fireBackward = newKey;
                ButtonText.text = inputManager.fireBackward.ToString();
                PlayerPrefs.SetString("fireBackwardKey", inputManager.fireBackward.ToString());
                break;
            case "fireRight":
                inputManager.fireRight = newKey;
                ButtonText.text = inputManager.fireRight.ToString();
                PlayerPrefs.SetString("fireRightKey", inputManager.fireRight.ToString());
                break;
            case "fireLeft":
                inputManager.fireLeft = newKey;
                ButtonText.text = inputManager.fireLeft.ToString();
                PlayerPrefs.SetString("fireLeftKey", inputManager.fireLeft.ToString());
                break;
        }

        yield return null;

    }
}
