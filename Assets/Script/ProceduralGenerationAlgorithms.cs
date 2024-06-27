using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPoint, int pathLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPoint);
        var currentPoint = startPoint;

        for (int i = 0; i < pathLength; i++)
        {
            var newPoint = currentPoint + Direction2D.GetRandomMainDirection();
            path.Add(newPoint);
            currentPoint = newPoint;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPoint, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var directon = Direction2D.GetRandomMainDirection();
        var currentPoint = startPoint;
        corridor.Add(currentPoint);

        for (int i = 0; i < corridorLength; i++) 
        {
            currentPoint += directon;
            corridor.Add(currentPoint);
        }
        return corridor;
    }

    public static List<BoundsInt> binarySpacePartioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y<minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    { 
                        roomsList.Add(room);
                    }                }
                else
                {

                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, minHeight, roomsQueue, room);
                    }

                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        throw new System.NotImplementedException();
    }

    private static void SplitHorizontally(int minWidth, int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        throw new System.NotImplementedException();
    }
}

public static class Direction2D
{
    public static List<Vector2Int> mainDirectionsList = new List<Vector2Int>
    {
        // (0,1) for left (1,0) for up etc.
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

    public static Vector2Int GetRandomMainDirection()
    {

    return mainDirectionsList[Random.Range(0, mainDirectionsList.Count)]; 
    }
}