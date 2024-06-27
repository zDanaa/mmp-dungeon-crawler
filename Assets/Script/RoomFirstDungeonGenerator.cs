using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range (0,10)]
    private int offset = 1;
    [SerializeField]
    private bool activateRandomWalk = false;

    protected override void startProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartioning(new BoundsInt((Vector3Int) startingPoint, new Vector3Int
            (dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomsList);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.GenerateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomList)
        {
            for (int column = offset; column < room.size.x - offset; column++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(column, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
