using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Biomes/Grassland")]
public class Grassland : BaseBiome
{
    public SimpleNoise grassNoise;
    public SimpleNoise mountainNoise;
    public SimpleNoise lakeNoise;
    public SimpleNoise treeNoise;
    public SimpleNoise bushNoise;
    public SimpleNoise rockNoise;
    public SimpleNoise grassPropNoise;

    private MyTile grass;
    private MyTile grassPatch;
    private MyTile waterTile;
    private MyTile treeTile;
    private MyTile bushTile;
    private MyTile rockTile;
    private MyTile grassPropTile;
    private MyTile mountainTile;

    public override void OnInitialize()
    {
        base.OnInitialize();

        this.grass = this.tileAtlas.GetTile("grass-1");
        this.grassPatch = this.tileAtlas.GetTile("grass-2");
        this.waterTile = this.tileAtlas.GetTile("water");
        this.treeTile = this.tileAtlas.GetTile("tree-1");
        this.bushTile = this.tileAtlas.GetTile("bush-1");
        this.rockTile = this.tileAtlas.GetTile("rock-1");
        this.mountainTile = this.tileAtlas.GetTile("mountain-1");
    }

    protected override void InitializeTileLayers(LayerTileList layerTileList)
    {
        this.EnableTileLayer(TileLayerName.BACKGROUND);
        //this.EnableTileLayer(TileLayerName.WATER_1);
        this.EnableTileLayer(TileLayerName.PROPS_1);
        this.EnableTileLayer(TileLayerName.GROUND_1);

        base.InitializeTileLayers(layerTileList);
    }
    protected override void OnGenerateTile(Vector3Int pos, LayerTileList layerTileList)
    {
        this.GenerateBackgroundTiles(pos, layerTileList);
        this.GenerateLakes(pos, layerTileList);
        this.GenerateProps(pos, layerTileList);
        this.GenerateMountainTiles(pos, layerTileList);
    }
    private void GenerateMountainTiles(Vector3Int pos, LayerTileList layerTileList)
    {
        float value = this.mountainNoise.GetNoise(pos.x, pos.y); // Noise

        if (value <= 1)
        {
            // Cannot add mountains on top of water
            if (layerTileList.GetTilesInLayer(TileLayerName.BACKGROUND).GetTileId(pos) == this.waterTile.id) return;
            layerTileList.GetTilesInLayer(TileLayerName.GROUND_1).SetTile(pos, this.mountainTile);
        }
    }
    private void GenerateBackgroundTiles(Vector3Int pos, LayerTileList layerTileList)
    {
        float value = this.grassNoise.GetNoise(pos.x, pos.y); // Noise

        // Grass
        MyTile tile;
        if (value <= 1)
        {
            tile = grassPatch;
        }
        else
        {
            tile = grass;
        }
        layerTileList.GetTilesInLayer(TileLayerName.BACKGROUND).SetTile(pos, tile);

    }
    private void GenerateLakes(Vector3Int pos, LayerTileList layerTileList)
    {
        float value = this.lakeNoise.GetNoise(pos.x, pos.y); // Noise

        // Grass
        if (value <= 1)
        {
            layerTileList.GetTilesInLayer(TileLayerName.BACKGROUND).SetTile(pos, this.waterTile);
        }
    }

    private void GenerateProps(Vector3Int pos, LayerTileList layerTileList)
    {
        MyTile tile;

        // See if should put tile
        if (this.treeNoise.GetNoise(pos.x, pos.y) <= 1)
        {
            tile = this.treeTile;
        }
        else if (this.bushNoise.GetNoise(pos.x, pos.y) <= 1)
        {
            tile = this.bushTile;
        }
        else if (this.rockNoise.GetNoise(pos.x, pos.y) <= 1)
        {
            tile = this.rockTile;
        }
        else if (this.grassPropNoise.GetNoise(pos.x, pos.y) <= 1 && layerTileList.GetTilesInLayer(TileLayerName.BACKGROUND).GetTileId(pos) == this.grassPatch.id)
        {
            tile = this.grassPropTile;
        }
        else
        {
            return;
        }

        if (!layerTileList.tilesPerLayer[TileLayerName.BACKGROUND].tileDatas[pos].IsWater())
        {
            layerTileList.GetTilesInLayer(TileLayerName.PROPS_1).SetTile(pos, tile);
        }
    }

}
