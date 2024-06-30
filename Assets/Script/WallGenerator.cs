using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void GenerateWalls(HashSet<Vector2Int> floorpoints, TilemapVisualizer tilemapVisualizer)
    {
        var simpleWallPoints = FindWallsInDirections(floorpoints, Direction2D.mainDirectionsList);
        var cornerWallPoints = FindWallsInDirections(floorpoints, Direction2D.diagonalDirectionsList);
        CreateBasicWalls(tilemapVisualizer, simpleWallPoints, floorpoints);
        GenerateCornerWalls(tilemapVisualizer, cornerWallPoints, floorpoints);
    }

    private static void GenerateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPoints, HashSet<Vector2Int> floorpoints)
    {
        foreach (var point in cornerWallPoints) 
        {
            string binaryNeighbours = "";
            foreach (var direction in Direction2D.allDirectionsList)
            {
                var neighbourPoint = point + direction;
                if (floorpoints.Contains(neighbourPoint))
                {
                    binaryNeighbours += "1";
                }
                else
                {
                    binaryNeighbours += "0";
                }
                tilemapVisualizer.paintSingleCornerWall(point, binaryNeighbours);
            }
        }
    }

    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> simpleWallPoints, HashSet<Vector2Int> floorpoints)
    {
        foreach (var point in simpleWallPoints)
        {
            string binaryNeighbours = "";
            foreach (var direction in Direction2D.mainDirectionsList)
            {
                var neighbourPoint = point + direction;
                if (floorpoints.Contains(neighbourPoint)){
                    binaryNeighbours += "1";
                }
                else
                {
                    binaryNeighbours += "0";
                }
            }
            tilemapVisualizer.paintSingleSimpleWall(point, binaryNeighbours);
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
