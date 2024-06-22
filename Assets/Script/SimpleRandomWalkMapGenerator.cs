using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRandomWalkMapGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startingPoint = Vector2Int.zero;

    private int iterations = 10;
    public int pathLength = 10;
    public bool randomStartPerIteration = true;
}
