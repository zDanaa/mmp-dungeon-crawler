using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null){
        Camera.main.transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            Camera.main.transform.position.z 
        );
        }
        else
        {
            GameObject roomsContent = GameObject.Find("RoomsContent");
            if (roomsContent != null)
            {
                // Finde alle GameObjects mit dem Tag "Player"
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject potentialPlayer in players)
                {
                    if (potentialPlayer.transform.IsChildOf(roomsContent.transform))
                    {
                        player = potentialPlayer;
                        Debug.Log("Player gefunden: " + player.name);
                        break;
                    }
                }

                if (player == null)
                {
                    Debug.LogError("Player GameObject nicht gefunden!");
                }
            }
            else
            {
                Debug.LogError("RoomsContent GameObject nicht gefunden!");
            }
        }
    }
}
