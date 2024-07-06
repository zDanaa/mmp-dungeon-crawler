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

    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary
        = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

    private HashSet<Vector2Int> floorPoints, corridorPoints;

    private List <Color> roomColors = new List<Color>();
    [SerializeField]
    private bool showRoomGizmo = false, showCorridorsGizmo;

    protected override void startProceduralGeneration()
    {
        CorridorInitialDungeonGeneration();
    }

    private void CorridorInitialDungeonGeneration()
    {
        HashSet<Vector2Int> floorpoints = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPoints = new HashSet<Vector2Int>();


        List<List<Vector2Int>> corridors = CreateCorridors(floorpoints, potentialRoomPoints);

        HashSet<Vector2Int> roomPoints = CreateRooms(potentialRoomPoints);

        List<Vector2Int> deadEnds = SelectDeadEnds(floorpoints);

        CreateRoomsAtDeadEnds(deadEnds, roomPoints);

        floorpoints.UnionWith(roomPoints);
        for (int i = 0; i < corridors.Count; i++)
        {
            //corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
            corridors[i] = IncreaseCorridorBrush(corridors[i]);
            floorpoints.UnionWith(corridors[i]);
        }

        tilemapVisualizer.PaintFloorTiles(floorpoints);
        WallGenerator.GenerateWalls(floorpoints, tilemapVisualizer);
    }

    public List<Vector2Int> IncreaseCorridorBrush(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++) 
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }


    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var deadEnd in deadEnds)
        {

            if (!roomFloors.Contains(deadEnd))
            {
                var room = startRandomWalk(randomWalkData, deadEnd);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> SelectDeadEnds(HashSet<Vector2Int> floorpoints)
    {
        List<Vector2Int > deadEnds = new List<Vector2Int>();

        foreach (var point in floorpoints)
        {
            int numberOfNeighbours = 0;
            foreach (var direction in Direction2D.mainDirectionsList)
            {
                if (floorpoints.Contains(point + direction))
                    { numberOfNeighbours++; }
            }
            if (numberOfNeighbours == 1)
                { deadEnds.Add(point); }
            
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPoints)
    {
        HashSet<Vector2Int> roomPoints = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(roomPercentage * potentialRoomPoints.Count);

        List<Vector2Int> roomsToCreate = potentialRoomPoints.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        ClearRoomData();
        foreach (var roomPoint in roomsToCreate) 
        {
            var roomFloor = startRandomWalk(randomWalkData, roomPoint);

            SaveRoomData(roomPoint, roomFloor);
            roomPoints.UnionWith(roomFloor);
        }
        return roomPoints;
    }

    private void SaveRoomData(Vector2Int roomPoint, HashSet<Vector2Int> roomFloor)
    {
        roomsDictionary[roomPoint] = roomFloor;
        roomColors.Add(UnityEngine.Random.ColorHSV());
    }

    private void ClearRoomData()
    {
        roomsDictionary.Clear();
        roomColors.Clear();
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorpoints, HashSet<Vector2Int> potentialRoomPoints)
    {
        var currentPoint = startingPoint;
        potentialRoomPoints.Add(currentPoint);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();


        for (int i = 0; i < corridorCount; i++) 
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPoint, corridorLength);
            corridors.Add(corridor);
            currentPoint = corridor[corridor.Count - 1];
            potentialRoomPoints.Add(currentPoint);
            floorpoints.UnionWith(corridor);
        }
        corridorPoints = new HashSet<Vector2Int>(floorpoints);
        return corridors;
    }
}
