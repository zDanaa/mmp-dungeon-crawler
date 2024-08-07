using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkMapGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected SimpleRandomWalkData randomWalkData;

    protected override void startProceduralGeneration()
    {
        HashSet<Vector2Int> floorPoints = startRandomWalk(randomWalkData, startingPoint);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPoints);
        WallGenerator.GenerateWalls(floorPoints,tilemapVisualizer);
    }

    protected HashSet<Vector2Int> startRandomWalk(SimpleRandomWalkData parameters, Vector2Int point)
    {
        var currentPoint = point;
        HashSet<Vector2Int> floorpoints = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkData.iterations; i++) 
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPoint, randomWalkData.pathLength);
            floorpoints.UnionWith(path); // Union to Add without Duplicates
            if (randomWalkData.randomStartEachIteration)
            {
                currentPoint = floorpoints.ElementAt(Random.Range(0, floorpoints.Count));
            }
        }
        return floorpoints;
    }

}
