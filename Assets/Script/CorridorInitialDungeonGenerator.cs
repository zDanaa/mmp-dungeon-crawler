using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorInitialDungeonGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 7;

    [SerializeField]
    [Range(0.1f, 1)]
    private float roompercentage = 0.9f;
    [SerializeField]
    public SimpleRandomWalkData roomGenerationData;

    protected override void startProceduralGeneration()
    {
        CorridorInitialDungeonGeneration();
    }

    private void CorridorInitialDungeonGeneration()
    {
        HashSet<Vector2Int> floorpoints = new HashSet<Vector2Int>();

        CreateCorridors(floorpoints);

        tilemapVisualizer.PaintFloorTiles(floorpoints);
        WallGenerator.GenerateWalls(floorpoints, tilemapVisualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorpoints)
    {
        var currentPoint = startingPoint;

        for (int i = 0; i < corridorCount; i++) 
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPoint, corridorLength);
            currentPoint = corridor[corridor.Count - 1];
            floorpoints.UnionWith(corridor);
        }
    }
}
