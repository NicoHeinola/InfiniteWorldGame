using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Contains tiles and their positions
/// </summary>
public class TilesInLayer
{
    public Dictionary<Vector3Int, TileBase> tiles;
    public Dictionary<Vector3Int, TileData> tileDatas;
    public Dictionary<Vector3Int, int> tileIds;

    public TilesInLayer()
    {
        this.tiles = new Dictionary<Vector3Int, TileBase>();
        this.tileDatas = new Dictionary<Vector3Int, TileData>();
        this.tileIds = new Dictionary<Vector3Int, int>();
    }

    public void SetTile(Vector3Int pos, MyTile tile)
    {
        this.tiles[pos] = tile.tile;
        this.tileDatas[pos] = tile.tileData;
        this.tileIds[pos] = tile.id;
    }

    public TileBase GetTile(Vector3Int pos)
    {
        return this.tiles[pos];
    }

    public int GetTileId(Vector3Int pos)
    {
        return this.tileIds[pos];
    }

    public TileData GetTileData(Vector3Int pos)
    {
        return this.tileDatas[pos];
    }

    public Dictionary<Vector3Int, TileBase> GetAllTiles()
    {
        return this.tiles;
    }
}

/// <summary>
/// A list of tiles per tilemap
/// </summary>
public class LayerTileList
{
    public Dictionary<TileLayerName, TilesInLayer> tilesPerLayer;

    public LayerTileList()
    {
        this.tilesPerLayer = new Dictionary<TileLayerName, TilesInLayer>();
    }
    public TilesInLayer GetTilesInLayer(TileLayerName layerName)
    {
        return this.tilesPerLayer[layerName];
    }

    public bool ContainsTile(TileLayerName layerName, Vector3Int pos)
    {
        return this.tilesPerLayer[layerName].tiles.ContainsKey(pos);
    }
}
/// <summary>
/// Tilemap names
/// </summary>
public enum TileLayerName
{
    BACKGROUND,
    GROUND_1,
    WATER_1,
    PROPS_1,
}
public abstract class BaseBiome : ScriptableObject
{
    // Info about generation parameters. Might be useful for noises
    protected int fromX;
    protected int fromY;
    protected int toX;
    protected int toY;

    [SerializeField] protected TileAtlas tileAtlas;

    private HashSet<TileLayerName> enabledLayers = new HashSet<TileLayerName>();

    public virtual void OnInitialize()
    {
        this.tileAtlas.Initialize();
    }

    /// <summary>
    /// Enables a tile layer
    /// </summary>
    /// <param name="layer">Name of the layer</param>
    protected void EnableTileLayer(TileLayerName layer)
    {
        this.enabledLayers.Add(layer);
    }
    /// <summary>
    /// Checks if tile layer is enabled
    /// </summary>
    /// <param name="layer">Name of the layer</param>
    /// <returns>Returns true if the layer is enabled</returns>
    public bool IsTileLayerEnabled(TileLayerName layer)
    {
        return this.enabledLayers.Contains(layer);
    }
    public HashSet<TileLayerName> GetEnabledTileLayers()
    {
        return this.enabledLayers;
    }
    /// <summary>
    /// Initializes tile layers
    /// </summary>
    /// <param name="layerTileList"></param>
    protected virtual void InitializeTileLayers(LayerTileList layerTileList)
    {
        layerTileList.tilesPerLayer[TileLayerName.BACKGROUND] = new TilesInLayer();
        layerTileList.tilesPerLayer[TileLayerName.GROUND_1] = new TilesInLayer();
        layerTileList.tilesPerLayer[TileLayerName.WATER_1] = new TilesInLayer();
        layerTileList.tilesPerLayer[TileLayerName.PROPS_1] = new TilesInLayer();
    }
    /// <summary>
    /// Generates an area of tiles for tilemaps
    /// </summary>
    /// <param name="fromX">Start x</param>
    /// <param name="fromY">Start y</param>
    /// <param name="toX">End x</param>
    /// <param name="toY">End y</param>
    /// <returns>A list of tiles per layer</returns>
    public virtual LayerTileList GenerateTiles(int fromX, int fromY, int toX, int toY)
    {
        int w = Mathf.Abs(toX - fromX);
        int h = Mathf.Abs(toY - fromY);
        int size = w * h;

        this.fromX = fromX;
        this.fromY = fromY;
        this.toX = toX;
        this.toY = toY;

        LayerTileList layerTileList = new LayerTileList();
        this.InitializeTileLayers(layerTileList);

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                int arrayPos = x * h + y;
                this.OnGenerateTile(new Vector3Int(fromX + x, fromY + y, 0), layerTileList);
            }
        }

        return layerTileList;
    }

    /// <summary>
    /// Called for each tile (position) when generating
    /// </summary>
    /// <param name="pos">Current position (z is ignored)</param>
    /// <param name="layerTileList">List of all tiles per layer. This should be modified when generating a tile</param>
    protected virtual void OnGenerateTile(Vector3Int pos, LayerTileList layerTileList)
    {
    }
}
