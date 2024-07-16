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
        if (player == null)
        {
            return;
        }
        currentSpawnRate = spawnRate;
        InitializeValidSpawnPositions(); 
        Debug.Log("Spawning start methdod");
    }
    
     public void Initialize(ItemPlacementHelper itemPlacementHelper, PlayerController player)
    {
        this.itemPlacementHelper = itemPlacementHelper;
        this.player = player;
        currentSpawnRate = spawnRate;
        Debug.Log("currentSpawnRate: " + currentSpawnRate);
        InitializeValidSpawnPositions(); 
    }

    private void InitializeValidSpawnPositions()
    {
        if (itemPlacementHelper == null || player == null) {
        Debug.Log("InitializeValidSpawnPositions: itemPlacementHelper oder player nicht vorhanden."); 
        return;
        }
        validSpawnPositions = itemPlacementHelper.GetPotentialSpawnPositions();
        Debug.Log("Valid Spawn Positions: " + validSpawnPositions.Count);
    } 

    public void StartSpawning()
    {
        if (canSpawn)
        {
            StartCoroutine(Spawn());
        }
        Debug.Log("Spawning started");
    }

    private IEnumerator Spawn()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(currentSpawnRate);
            Debug.Log("Spawning enemy");
            int randomIndex = Random.Range(0, spawnableObjects.Length);
            GameObject enemyToSpawn = spawnableObjects[randomIndex];

            Vector2? spawnPosition = GetValidSpawnPosition();

            if (spawnPosition.HasValue)
            {
                Instantiate(enemyToSpawn, (Vector3)spawnPosition.Value, Quaternion.identity);
                currentSpawnRate *= decreaseSpawnRateFactor;
                Debug.Log("Spawned enemy at: " + spawnPosition.Value + " | Rate: " + currentSpawnRate);
            }
        }
    }
          

   private Vector2? GetValidSpawnPosition()
    {
        if (validSpawnPositions == null || validSpawnPositions.Count == 0)
        {
            Debug.Log("GetValidSpawnPosition: Keine gültigen Spawn-Positionen verfügbar.");
            return null;
        }
        Debug.Log($"GetValidSpawnPosition: Anzahl gültiger Spawn-Positionen: {validSpawnPositions.Count}");
        Debug.Log($"GetValidSpawnPosition: MinSpawnDistance: {minSpawnDistance}, MaxSpawnDistance: {maxSpawnDistance}");

        for (int i = 0; i < 20; i++)
        {
            int randomIndex = Random.Range(0, validSpawnPositions.Count);
            Vector2 potentialPosition = validSpawnPositions[randomIndex];
            float distance = Vector2.Distance(player.transform.position, potentialPosition);

            Debug.Log($"Versuch {i}: PotentialPosition: {potentialPosition}, Distanz zum Spieler: {distance}");

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
