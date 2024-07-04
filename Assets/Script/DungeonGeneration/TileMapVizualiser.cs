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
    private TileBase floorTile, wallTop, wallSideLeft, wallSideRight, wallBot, wallFull, 
        wallInnerCornerDownLeft, wallInnerCornerDownRight, 
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;

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

    internal void PaintSingleSimpleWall(Vector2Int point, String binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesBinaryData.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallTypesBinaryData.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypesBinaryData.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallTypesBinaryData.wallBottom.Contains(typeAsInt))
        {
            tile = wallBot;
        }
        else if (WallTypesBinaryData.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        if (tile != null)
        {
            paintSingleTile(wallTilemap, tile, point);
        }
    }
    
    internal void PaintSingleCornerWall(Vector2Int point, String binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypesBinaryData.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        
        else if (WallTypesBinaryData.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        
        else if (WallTypesBinaryData.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        
        else if (WallTypesBinaryData.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        
        else if (WallTypesBinaryData.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        
        else if (WallTypesBinaryData.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        
        else if (WallTypesBinaryData.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallTypesBinaryData.wallBottomEightDirections.Contains(typeAsInt))
        {
            tile = wallBot;
        }
        
        if (tile != null)
        {
            paintSingleTile(wallTilemap, tile, point);
        }
    }
}
