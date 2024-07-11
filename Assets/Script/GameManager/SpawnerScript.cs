using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{   
    [SerializeField] private GameObject[] spawnableObjects;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool canSpawn = true;
    public PlayerController player;
    //[SerializeField] private float spawnRadius;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
     private float currentSpawnRate;
     [SerializeField] private float decreaseSpawnRateFactor;
    public LayerMask obstacleLayer;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentSpawnRate = spawnRate;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(currentSpawnRate);
            int random = Random.Range(0, spawnableObjects.Length);
            GameObject spawningEnemies = spawnableObjects[random];
            float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 spawnPosition = player.transform.position + Random.insideUnitSphere.normalized * spawnDistance;

            
            Instantiate(spawningEnemies, spawnPosition, Quaternion.identity);
            currentSpawnRate = currentSpawnRate * decreaseSpawnRateFactor;
            Debug.Log("Rate: " + currentSpawnRate);
        
        }  
            
        
    }
    public void StopSpawning()
{
    canSpawn = false;
    Debug.Log("Spawning stopped by external call.");
}

   
}
