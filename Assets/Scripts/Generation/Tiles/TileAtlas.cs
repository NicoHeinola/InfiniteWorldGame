using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[System.Serializable]
public struct MyTile
{
    public TileBase tile;
    public TileData tileData;
    public int id;
    public string name;

    public void SetTileBase(TileBase tile) { this.tile = tile; }
}

[CreateAssetMenu(menuName = "Tiles/TileAtlas")]

public class TileAtlas : ScriptableObject
{
    [SerializeField] private List<MyTile> tiles;
    private Dictionary<int, MyTile> allTilesAsIds;
    private Dictionary<string, MyTile> allTilesAsNames;
    public void Initialize()
    {
        this.allTilesAsIds = new Dictionary<int, MyTile>();
        this.allTilesAsNames = new Dictionary<string, MyTile>();

        foreach (MyTile tile in tiles)
        {
            this.allTilesAsIds[tile.id] = tile;
            this.allTilesAsNames[tile.name] = tile;
        }
    }
    public MyTile GetTile(int id)
    {
        return this.allTilesAsIds[id];
    }
    public MyTile GetTile(string name)
    {
        return this.allTilesAsNames[name];
    }
}
