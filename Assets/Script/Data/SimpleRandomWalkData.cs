using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "SimpleRandomWalkParams", menuName  = "PDG/SimpleRandomWalkData")]
public class SimpleRandomWalkData : ScriptableObject
{
    public int iterations = 10;
    public int pathLength = 10;
    public bool randomStartEachIteration = true;

}
