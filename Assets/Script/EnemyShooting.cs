using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;
    [SerializeField]
    private float fireRate = 2;

    private float timer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null){
        timer += Time.deltaTime;

        if (timer > fireRate){
            timer = 0;
            shoot();
        }

        }
    }

    public void shoot(){
        Instantiate(bullet,bulletPos.position, Quaternion.identity);
    }
}
