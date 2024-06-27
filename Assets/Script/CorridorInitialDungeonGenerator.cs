using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorInitialDungeonGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 7;

    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercentage = 0.9f;

    protected override void startProceduralGeneration()
    {
        CorridorInitialDungeonGeneration();
    }

    private void CorridorInitialDungeonGeneration()
    {
        HashSet<Vector2Int> floorpoints = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPoints = new HashSet<Vector2Int>();


        CreateCorridors(floorpoints, potentialRoomPoints);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPoints);

        floorpoints.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorpoints);
        WallGenerator.GenerateWalls(floorpoints, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPoints)
    {
        HashSet<Vector2Int> roomPoints = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(roomPercentage * potentialRoomPoints.Count);

        List<Vector2Int> roomsToCreate = potentialRoomPoints.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPoint in roomsToCreate) 
        {
            var roomFloor = startRandomWalk(randomWalkData, roomPoint);
            roomPoints.UnionWith(roomFloor);
        }
        return roomPoints;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorpoints, HashSet<Vector2Int> potentialRoomPoints)
    {
        var currentPoint = startingPoint;
        potentialRoomPoints.Add(currentPoint);


        for (int i = 0; i < corridorCount; i++) 
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPoint, corridorLength);
            currentPoint = corridor[corridor.Count - 1];
            potentialRoomPoints.Add(currentPoint);
            floorpoints.UnionWith(corridor);
        }
    }
}
