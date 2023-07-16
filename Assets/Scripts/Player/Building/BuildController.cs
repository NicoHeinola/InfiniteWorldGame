using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;
    public void Build()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3Int tilePos = tilemap.WorldToCell(worldPos);
        tilemap.SetTile(tilePos, tile);
    }
}
