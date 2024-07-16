using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RoomContentGenerator : MonoBehaviour
{
    [SerializeField]
    private RoomGenerator playerRoom, defaultRoom;

    List<GameObject> spawnedObjects = new List<GameObject>();

    [SerializeField]
    private GraphTest graphTest;


    public Transform itemParent;


    public UnityEvent RegenerateDungeon;

    public GameObject spawnerPrefab;
    public SpawnerScript spawner;
    private PlayerController player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var item in spawnedObjects)
            {
                Destroy(item);
            }
            RegenerateDungeon?.Invoke();
        }
    }
    public void GenerateRoomContent(DungeonData dungeonData)
    {
        foreach (GameObject item in spawnedObjects)
        {
            DestroyImmediate(item);
        }
        spawnedObjects.Clear();


        SelectPlayerSpawnPoint(dungeonData);
        SelectEnemySpawnPoints(dungeonData);
        
        GameObject spawnerInstance = Instantiate(spawnerPrefab, Vector3.zero, Quaternion.identity);
        spawner = spawnerInstance.GetComponent<SpawnerScript>();
        InitializeAndStartSpawning(dungeonData, spawnerInstance, spawner);

       
        
           
       /* HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>();
        HashSet<Vector2Int> roomFloorWithoutCorridors = new HashSet<Vector2Int>();
        foreach (var room in dungeonData.roomsDictionary.Values)
        {
            roomFloor.UnionWith(room);
        }

        foreach (var roomKey in dungeonData.roomsDictionary.Keys)
        {
            roomFloorWithoutCorridors.UnionWith(dungeonData.GetRoomFloorWithoutCorridors(roomKey));
        }

        ItemPlacementHelper itemPlacementHelper = new ItemPlacementHelper(roomFloor, roomFloorWithoutCorridors);

        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
        Debug.Log("Player found: " + player);
        Debug.Log("ItemPlacementHelper created: " + itemPlacementHelper);
        spawnerScript.Initialize(itemPlacementHelper, player);
        spawnerScript.StartSpawning();

        spawnedObjects.Add(spawnerInstance);*/


        
        foreach (GameObject item in spawnedObjects)
        {
            if(!item.scene.IsValid()) continue;
            if (item != null)
                item.transform.SetParent(itemParent, false);
        }
    }

    private void SelectPlayerSpawnPoint(DungeonData dungeonData)
    {
        int randomRoomIndex = UnityEngine.Random.Range(0, dungeonData.roomsDictionary.Count);
        Vector2Int playerSpawnPoint = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);

        graphTest.RunDijkstraAlgorithm(playerSpawnPoint, dungeonData.floorPoints);

        Vector2Int roomIndex = dungeonData.roomsDictionary.Keys.ElementAt(randomRoomIndex);

        List<GameObject> placedPrefabs = playerRoom.ProcessRoom(
            playerSpawnPoint,
            dungeonData.roomsDictionary.Values.ElementAt(randomRoomIndex),
            dungeonData.GetRoomFloorWithoutCorridors(roomIndex)
            );


        spawnedObjects.AddRange(placedPrefabs);

        dungeonData.roomsDictionary.Remove(playerSpawnPoint);
    }


    private void SelectEnemySpawnPoints(DungeonData dungeonData)
    {
        foreach (KeyValuePair<Vector2Int, HashSet<Vector2Int>> roomData in dungeonData.roomsDictionary)
        {
            spawnedObjects.AddRange(
                defaultRoom.ProcessRoom(
                    roomData.Key,
                    roomData.Value,
                    dungeonData.GetRoomFloorWithoutCorridors(roomData.Key)
                    )
            );

        }
    }

    private void InitializeAndStartSpawning(DungeonData dungeonData, GameObject spawnerInstance, SpawnerScript spawnerScript)
    {
    HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>();
    HashSet<Vector2Int> roomFloorWithoutCorridors = new HashSet<Vector2Int>();
    foreach (var room in dungeonData.roomsDictionary.Values)
    {
        roomFloor.UnionWith(room);
    }

    foreach (var roomKey in dungeonData.roomsDictionary.Keys)
    {
        roomFloorWithoutCorridors.UnionWith(dungeonData.GetRoomFloorWithoutCorridors(roomKey));
    }

    ItemPlacementHelper itemPlacementHelper = new ItemPlacementHelper(roomFloor, roomFloorWithoutCorridors);

    if (player == null)
    {
        player = FindObjectOfType<PlayerController>();
    }
    Debug.Log("Player found: " + player);
    Debug.Log("ItemPlacementHelper created: " + itemPlacementHelper);
    spawnerScript.Initialize(itemPlacementHelper, player);
    spawnerScript.StartSpawning();

    spawnedObjects.Add(spawnerInstance);

    }

    public HashSet<Vector2Int> GetRoomAtPosition(Vector2Int position, DungeonData dungeonData)
    {
        foreach (var room in dungeonData.roomsDictionary)
        {
            if (room.Value.Contains(position))
            {
                return room.Value;
            }
        }
        return null;
    }

}
