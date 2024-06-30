using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{   
    [SerializeField] private GameObject[] spawnableObjects;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(spawnRate);
            int random = Random.Range(0, spawnableObjects.Length);
            GameObject spawningEnemies = spawnableObjects[random];
            Instantiate(spawningEnemies, transform.position, Quaternion.identity);
           
            Debug.Log("Spawned: " + spawningEnemies.name);

        }
    }
    public void StopSpawning()
{
    canSpawn = false;
    Debug.Log("Spawning stopped by external call.");
}

   
}
