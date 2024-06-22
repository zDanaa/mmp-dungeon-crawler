using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private TileBase floorTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPoints)
    {
        PaintFloorTiles(floorPoints, floorTilemap, floorTile);
    }

    private void PaintFloorTiles(IEnumerable<Vector2Int> points, Tilemap tilemap, TileBase tile)
    {
        foreach (var point in points) { 
            paintSingleTile(tilemap, tile, point);
        }
    }

    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int point)
    {
        var tilePoint = tilemap.WorldToCell((Vector3Int)point);
        tilemap.SetTile(tilePoint, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
    }
}
