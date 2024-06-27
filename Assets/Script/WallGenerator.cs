using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void GenerateWalls(HashSet<Vector2Int> floorpoints, TilemapVisualizer tilemapVisualizer)
    {
        var simpleWallPoints = FindWallsInDirections(floorpoints, Direction2D.mainDirectionsList);
        foreach (var point in simpleWallPoints)
        {
            tilemapVisualizer.paintSingleSimpleWall(point);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorpoints, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPoints = new HashSet<Vector2Int>();
        foreach (var point in floorpoints)
        {
            foreach (var direction in directionList)
            {
                var neighbourpoint = point + direction;
                if (!floorpoints.Contains(neighbourpoint))
                {
                    wallPoints.Add(neighbourpoint);
                }
            }
        }
        return wallPoints;
    }
}
