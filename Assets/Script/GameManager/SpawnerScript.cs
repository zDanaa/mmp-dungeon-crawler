using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{   
    [SerializeField] private GameObject[] spawnableObjects;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool canSpawn = true;
    public PlayerController player;
    [SerializeField] private float spawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(spawnRate);
            int random = Random.Range(0, spawnableObjects.Length);
            GameObject spawningEnemies = spawnableObjects[random];
            Vector3 spawnPosition = player.transform.position + Random.insideUnitSphere * spawnRadius;
            Instantiate(spawningEnemies, spawnPosition, Quaternion.identity);
        }
    }
    public void StopSpawning()
{
    canSpawn = false;
    Debug.Log("Spawning stopped by external call.");
}

   
}
