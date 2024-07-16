using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData
{
    public Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary;
    public HashSet<Vector2Int> floorPoints;
    public HashSet<Vector2Int> corridorPoints;

    public HashSet<Vector2Int> GetRoomFloorWithoutCorridors(Vector2Int dictionaryKey)
    {
       HashSet<Vector2Int> roomFloorWithoutCorridors = new HashSet<Vector2Int>(roomsDictionary[dictionaryKey]);
        roomFloorWithoutCorridors.ExceptWith(corridorPoints);
        return roomFloorWithoutCorridors;
    }
}
