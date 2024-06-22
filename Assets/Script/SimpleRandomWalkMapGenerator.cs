using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkMapGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startingPoint = Vector2Int.zero;
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int pathLength = 10;
    [SerializeField]
    public bool randomStartEachIteration = true;

    public void startProceduralGeneration()
    {
        HashSet<Vector2Int> floorPoints = startRandomWalk();
        foreach (var point in floorPoints)
        {
            Debug.Log(point);
        }
    }

    protected HashSet<Vector2Int> startRandomWalk()
    {
        var currentPoint = startingPoint;
        HashSet<Vector2Int> floorpoints = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++) 
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPoint, pathLength);
            floorpoints.UnionWith(path); // Union to Add without Duplicates
            if (randomStartEachIteration)
            {
                currentPoint = floorpoints.ElementAt(Random.Range(0, floorpoints.Count));
            }
        }
        return floorpoints;
    }
}
