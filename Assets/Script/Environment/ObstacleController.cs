using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        Object[] sprites =  Resources.LoadAll("rocks", typeof(Sprite));
        int index = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = Instantiate(sprites[index]) as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
