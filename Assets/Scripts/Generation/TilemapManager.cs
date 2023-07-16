using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{

    [Serializable]
    public class TilemapNamePair
    {
        public TileLayerName key;
        public Tilemap val;
    }

    public List<TilemapNamePair> tilemapList = new List<TilemapNamePair>();

    private Dictionary<TileLayerName, Tilemap> tilemaps;

    public Tilemap GetTilemap(TileLayerName name)
    {
        return this.tilemaps[name];
    }
    private void Start()
    {
        this.tilemaps = new Dictionary<TileLayerName, Tilemap>();
        // Initializes tilemap dictionary
        foreach (TilemapNamePair n in tilemapList)
        {
            //n.val.ClearAllTiles();
            this.tilemaps[n.key] = n.val;
        }
    }
}
