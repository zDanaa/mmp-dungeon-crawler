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
            //currentSpawnRate *= decreaseSpawnRateFactor;
            Debug.Log("Rate: " + currentSpawnRate);
            if (spawnPosition.HasValue)
            {
                Instantiate(enemyToSpawn, (Vector3)spawnPosition.Value, Quaternion.identity);
                //currentSpawnRate *= decreaseSpawnRateFactor;
                Debug.Log("Spawned enemy at: " + spawnPosition.Value + " | Rate: " + currentSpawnRate);
            }
        }
    }
          
    public void DecreaseSpawnRate()
    {
        currentSpawnRate -= decreaseSpawnRateFactor;
        Debug.Log("New spawn rate: " + currentSpawnRate);
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
                Debug.Log($"Erfolgreicher Spawn bei {potentialPosition} mit Distanz {distance}");
                return potentialPosition;
            }
        }
        Debug.Log("GetValidSpawnPosition: Keine gültige Spawn-Position gefunden nach 20 Versuchen.");
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
        }

        return randomPoints;
    }*/

    public void StopSpawning()
{
    canSpawn = false;
}

   
}
