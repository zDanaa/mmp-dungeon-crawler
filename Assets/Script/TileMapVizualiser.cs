using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop;

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
        wallTilemap.ClearAllTiles();
    }

    internal void paintSingleSimpleWall(Vector2Int point, String binaryType)
    {
        paintSingleTile(wallTilemap, wallTop, point);
    }
    
    internal void PaintSingleCornerWall(Vector2Int point, String binaryType) { 
    
    }
}
