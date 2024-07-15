using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPlacementHelper
{
    private HashSet<Vector2Int> roomFloor;
    Dictionary<PlacementType, HashSet<Vector2Int>>
        tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();

    HashSet<Vector2Int> roomFloorWithoutCorridor;

    public ItemPlacementHelper(HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> roomFloorWithoutCorridor)
    {
        this.roomFloor = roomFloor;
        Graph graph = new Graph(roomFloor);
        this.roomFloorWithoutCorridor = roomFloorWithoutCorridor;
        tileByType = new Dictionary<PlacementType, HashSet<Vector2Int>>();

        foreach (var point in roomFloorWithoutCorridor)
        {
            int neighboursCountAllDir = graph.GetNeighboursAllDirections(point).Count;
            PlacementType type = neighboursCountAllDir < 8 ? PlacementType.NearWall : PlacementType.OpenSpace;

            if (!tileByType.ContainsKey(type))
            {
                tileByType[type] = new HashSet<Vector2Int>();
            }

            if (type == PlacementType.NearWall && graph.GetNeighboursBasicDirections(point).Count == 4)
            {
                continue;
            }
            tileByType[type].Add(point);
        }
    }

    public Vector2? GetItemPlacementPosition(PlacementType placementType, int maxIterations, Vector2Int size, bool addOffset)
    {
        int itemArea = size.x * size.y;
        if (tileByType[placementType].Count < itemArea)
            return null;

        int iteration = 0;
        while (iteration < maxIterations)
        {
            iteration++;
            int i = UnityEngine.Random.Range(0, tileByType[placementType].Count);
            Vector2Int point = tileByType[placementType].ElementAt(i);


            if (itemArea > 1)
            {
                var (result, placementPositions) = PlaceBigItem(point, size, addOffset);

                if(result == false)
                {
                    continue;
                }

                tileByType[placementType].ExceptWith(placementPositions);
                tileByType[PlacementType.NearWall].ExceptWith(placementPositions);
            }
            else
            {
                tileByType[placementType].Remove(point);
            }
            return point;
        }
        return null;

    }

    private (bool, List<Vector2Int>) PlaceBigItem(Vector2Int initialPoint, Vector2Int size, bool addOffset)
    {
        List<Vector2Int> points = new List<Vector2Int>() { initialPoint };
        int maxX = addOffset ? size.x + 1 : size.x;
        int maxY = addOffset ? size.y + 1 : size.y;
        int minX = addOffset ? -1 : 0;
        int minY = addOffset ? -1 : 0;

        for (int row = minX; row <= maxX; row++)
        {
            for (int col = minY; col <= maxY; col++)
            {
                if (col == 0 && row == 0)
                {
                    continue;
                }
                Vector2Int newPointsToCheck = new Vector2Int(initialPoint.x + row, initialPoint.y + col);
                if (!roomFloorWithoutCorridor.Contains(newPointsToCheck))
                {
                    return (false, points);
                }
                points.Add(newPointsToCheck);
            }
        }
        return (true, points);
    }
    public bool IsPositionValid(Vector2Int position)
    {
        bool isValid = roomFloor.Contains(position);
        return isValid;
    }

     public List<Vector2> GetPotentialSpawnPositions() 
    {
        List<Vector2> potentialPositions = roomFloorWithoutCorridor.Select(pos => (Vector2)pos).ToList();
        return potentialPositions;
    }
    
}
    public enum PlacementType
{
    OpenSpace,
    NearWall
}
