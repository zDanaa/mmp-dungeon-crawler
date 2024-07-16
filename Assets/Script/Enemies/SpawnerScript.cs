using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{   
    [SerializeField] private GameObject[] spawnableObjects;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool canSpawn = true;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float decreaseSpawnRateFactor;
    private float currentSpawnRate;
    private PlayerController player;
    private ItemPlacementHelper itemPlacementHelper;
    private List<Vector2> validSpawnPositions;

    void Start()
    {
        if (player == null)
        {
           player = FindObjectOfType<PlayerController>();
        }
        currentSpawnRate = spawnRate;
        InitializeValidSpawnPositions(); 
    }
    
     public void Initialize(ItemPlacementHelper itemPlacementHelper, PlayerController player)
    {
        this.itemPlacementHelper = itemPlacementHelper;
        this.player = player;
        currentSpawnRate = spawnRate;
        InitializeValidSpawnPositions(); 
    }

    private void InitializeValidSpawnPositions()
    {
        if (itemPlacementHelper == null)
        {
            return;
        }
        validSpawnPositions = itemPlacementHelper.GetPotentialSpawnPositions();
    }

    public void StartSpawning()
    {
        if (canSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(currentSpawnRate);
            int randomIndex = Random.Range(0, spawnableObjects.Length);
            GameObject enemyToSpawn = spawnableObjects[randomIndex];
            Vector2? spawnPosition = GetValidSpawnPosition();
            //currentSpawnrate *= decreaseSpawnRateFactor;
            if (spawnPosition.HasValue)
            {
                Instantiate(enemyToSpawn, (Vector3)spawnPosition.Value, Quaternion.identity);
            }
        }
    }
          
    public void DecreaseSpawnRate()
    {
        currentSpawnRate -= decreaseSpawnRateFactor;
    }

    public void StopSpawning()
    {
        canSpawn = false;
        currentSpawnRate = 0;
    }

   private Vector2? GetValidSpawnPosition()
    {
        if (validSpawnPositions == null || validSpawnPositions.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < 40; i++)
        {
            int randomIndex = Random.Range(0, validSpawnPositions.Count);
            Vector2 potentialPosition = validSpawnPositions[randomIndex];
            float distance = Vector2.Distance(player.transform.position, potentialPosition);

            if (distance >= minSpawnDistance && distance <= maxSpawnDistance)
            {
                return potentialPosition;
            }
        }
        return null;
    }

    /* private Vector2? GetRandomValidSpawnPositionNearPlayer()
    {
        Vector2 playerPosition = player.transform.position;
    
        List<Vector2> potentialPositions = GenerateRandomPointsAroundPlayer(playerPosition, minSpawnDistance, maxSpawnDistance, 200);

        foreach (var potentialPosition in potentialPositions)
        {
            Vector2Int gridPosition = Vector2Int.RoundToInt(potentialPosition);
            if (itemPlacementHelper.IsPositionValid(gridPosition))
            {
                return potentialPosition;
            }
        
        }
        return null;
    }    */

   /* public void StopSpawning()
{
    canSpawn = false;
} */

   
}
