using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{   
    [SerializeField] private GameObject[] spawnableObjects;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool canSpawn = true;
    public PlayerController player;
    [SerializeField] private float minSpawnDistance = 2f;
    [SerializeField] private float maxSpawnDistance = 5f;
     private float currentSpawnRate;
     [SerializeField] private float decreaseSpawnRateFactor;
    private ItemPlacementHelper itemPlacementHelper;
    private List<Vector2> validSpawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentSpawnRate = spawnRate;
        InitializeValidSpawnPositions(); 
        StartCoroutine(DelayedStartSpawning(10f));
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
        if (itemPlacementHelper == null || player == null) return;

        validSpawnPositions = itemPlacementHelper.GetPotentialSpawnPositions();
    } 

     private IEnumerator DelayedStartSpawning(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartSpawning();
    }


    public void StartSpawning()
    {
        if (canSpawn)
        {
            StartCoroutine(DelayedStartSpawning(10f));
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

            if (spawnPosition.HasValue)
            {
                Instantiate(enemyToSpawn, (Vector3)spawnPosition.Value, Quaternion.identity);
                currentSpawnRate *= decreaseSpawnRateFactor;
                //Debug.Log("Spawned enemy at: " + spawnPosition.Value + " | Rate: " + currentSpawnRate);
            }
        }
    }
          

   private Vector2? GetValidSpawnPosition()
    {
        if (validSpawnPositions == null || validSpawnPositions.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < 20; i++)
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
        if (player == null || itemPlacementHelper == null)
        {
            Debug.LogError("Player or ItemPlacementHelper is null.");
            return null;
        }

        Vector2 playerPosition = player.transform.position;
        Debug.Log($"Player position: {playerPosition}");

        List<Vector2> potentialPositions = GenerateRandomPointsAroundPlayer(playerPosition, minSpawnDistance, maxSpawnDistance, 200);

        foreach (var potentialPosition in potentialPositions)
        {
            Vector2Int gridPosition = Vector2Int.RoundToInt(potentialPosition);
            if (itemPlacementHelper.IsPositionValid(gridPosition))
            {
                Debug.Log($"Found valid spawn position: {potentialPosition}");
                return potentialPosition;
            }
            else
            {
                Debug.Log($"Position {potentialPosition} is not valid.");
            }
        }

        Debug.LogWarning("Failed to find a valid spawn position near the player.");
        return null;
    }    

      private List<Vector2> GenerateRandomPointsAroundPlayer(Vector2 playerPosition, float minDistance, float maxDistance, int count)
    {
        List<Vector2> randomPoints = new List<Vector2>();

        for (int i = 0; i < count; i++)
        {
            float distance = Random.Range(minDistance, maxDistance);
            float angle = Random.Range(0, Mathf.PI * 2);
            Vector2 point = playerPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            randomPoints.Add(point);
            Debug.Log($"Generated random point: {point}");
        }

        return randomPoints;
    }*/

    public void StopSpawning()
{
    canSpawn = false;
    Debug.Log("Spawning stopped by external call.");
}

   
}
