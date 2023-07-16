using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Chunk
{
    public int size;
    public int x;
    public int y;
    public LayerTileList tileList;
    public Chunk(int size, int x, int y)
    {
        this.x = x;
        this.y = y;
        this.size = size;
        tileList = new LayerTileList();
    }
}
[System.Serializable]
public class ChunkLoader : MonoBehaviour
{
    public ChunkOptions chunkOptions;

    public Transform renderPoint;

    private Dictionary<Vector2Int, Chunk> generatedChunks = new Dictionary<Vector2Int, Chunk>(); // Contains all generated chunks so they aren't generated again
    private Dictionary<Vector2Int, bool> renderedChunks = new Dictionary<Vector2Int, bool>();
    private HashSet<Vector2Int> loadedChunks = new HashSet<Vector2Int>();
    private BaseBiome activeBiome;

    private Vector2Int oldPos = new Vector2Int(100000, 100000);
    [SerializeField] private bool rendering = false;

    // Tilemaps
    public TilemapManager tilemapManager;

    public void SetActiveBiome(BaseBiome biome)
    {
        biome.OnInitialize();
        this.activeBiome = biome;
    }

    private void SetGeneratedChunk(Vector2Int chunkCoord, Chunk chunk)
    {
        generatedChunks[chunkCoord] = chunk;
    }
    private Chunk FindChunk(Vector2Int chunkCoord)
    {
        Chunk chunk;
        this.generatedChunks.TryGetValue(chunkCoord, out chunk);
        return chunk;
    }
    private bool IsChunkRendered(Vector2Int chunkCoord)
    {
        return this.renderedChunks.ContainsKey(chunkCoord);
        /*bool rendered;
        this.renderedChunks.TryGetValue(chunkCoord, out rendered);
        return rendered;*/
    }
    public int ToChunkPosition(float t)
    {
        return Mathf.FloorToInt((t / chunkOptions.ChunkSize));
    }
    private bool IsChunkLoaded(Vector2Int chunkCoord)
    {
        return this.loadedChunks.Contains(chunkCoord);
    }
    private bool IsChunkInGenerationDistance(int cX, int cY, int minX, int minY, int maxX, int maxY)
    {
        return (cX < maxX && cX > minX - 1 && cY < maxY && cY > minY - 1);
    }
    /*private bool IsChunkInGenerationDistance(Vector2Int currentPos, Vector2Int chunkPos)
    {
        return Vector2Int.Distance(chunkPos, currentPos) <= renderDistance;
    }
    */

    private void FixedUpdate()
    {
        if (!this.rendering)
        {
            this.RenderTiles();
        }
    }

    public void RenderTiles()
    {
        int currentChunkX = ToChunkPosition(renderPoint.position.x);
        int currentChunkY = ToChunkPosition(renderPoint.position.y);
        Vector2Int currentPos = new Vector2Int(currentChunkX, currentChunkY);

        // If this chunk is loaded, don't reload it
        if (currentPos != this.oldPos)
        {
            this.rendering = true;
            this.oldPos = currentPos;

            this.Unload(currentPos);
            this.StartCoroutine(this.Load(currentChunkX, currentChunkY));
        }
    }


    private void Unload(Vector2Int currentPos)
    {
        // Min & max values
        int minX = currentPos.x - chunkOptions.RenderDistance;
        int minY = currentPos.y - chunkOptions.RenderDistance;
        int maxX = currentPos.x + chunkOptions.RenderDistance + 1;
        int maxY = currentPos.y + chunkOptions.RenderDistance + 1;

        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (Vector2Int chunkPos in this.loadedChunks)
        {
            if (!this.IsChunkInGenerationDistance(chunkPos.x, chunkPos.y, minX, minY, maxX, maxY))
            {
                toRemove.Add(chunkPos);
            }
        }

        var watch = System.Diagnostics.Stopwatch.StartNew();

        TileBase[] emptyTiles = new TileBase[chunkOptions.ChunkSize * chunkOptions.ChunkSize];
        foreach (Vector2Int chunkPos in toRemove)
        {
            this.loadedChunks.Remove(chunkPos);
            foreach (TileLayerName layerName in this.activeBiome.GetEnabledTileLayers())
            {
                this.tilemapManager.GetTilemap(layerName).SetTilesBlock(new BoundsInt(chunkPos.x * chunkOptions.ChunkSize, chunkPos.y * chunkOptions.ChunkSize, 0, 16, 16, 1), emptyTiles);
            }
        }

        watch.Stop();
        Debug.Log($"Removed chunks: {watch.ElapsedMilliseconds} ms");
    }
    private IEnumerator Load(int currentChunkX, int currentChunkY)
    {
        // Min & max values
        int minX = currentChunkX - chunkOptions.RenderDistance;
        int minY = currentChunkY - chunkOptions.RenderDistance;
        int maxX = currentChunkX + chunkOptions.RenderDistance + 1;
        int maxY = currentChunkY + chunkOptions.RenderDistance + 1;

        // Random loop data
        Vector2Int coord = new Vector2Int();
        Chunk chunk;
        List<Chunk> chunks = new List<Chunk>();

        int loadedChunkCount = 0;
        for (int cX = minX; cX < maxX; cX++)
        {
            coord.x = cX;
            for (int cY = minY; cY < maxY; cY++)
            {
                coord.y = cY;

                if (this.IsChunkLoaded(coord))
                {
                    continue;
                }

                if (loadedChunkCount > chunkOptions.RenderChunksPerFrame)
                {
                    // Place the tiles
                    /*foreach (Chunk c in chunks)
                    {
                        foreach (TileLayerName layerName in this.activeBiome.GetEnabledTileLayers())
                        {
                            Dictionary<Vector3Int, TileBase> tiles = c.tileList.GetTilesInLayer(layerName).GetAllTiles();
                            this.tilemapManager.GetTilemap(layerName).SetTiles(tiles.Keys.ToArray(), tiles.Values.ToArray());
                        }
                    }*/

                    loadedChunkCount = 0;
                    yield return new WaitForSeconds(0.1f);
                }
                loadedChunkCount++;


                chunk = this.FindChunk(coord); // Try to find already generated chunk
                // If not found
                if (chunk == null)
                {
                    // Generate tiles and create a chunk
                    chunk = new Chunk(this.chunkOptions.ChunkSize, cX, cY);
                    chunk.tileList = this.activeBiome.GenerateTiles(cX * chunkOptions.ChunkSize, cY * chunkOptions.ChunkSize, (cX + 1) * chunkOptions.ChunkSize, (cY + 1) * chunkOptions.ChunkSize);

                    // Save chunk data
                    this.SetGeneratedChunk(coord, chunk);
                }

                // Add chunk tiles
                foreach (TileLayerName layerName in this.activeBiome.GetEnabledTileLayers())
                {
                    Dictionary<Vector3Int, TileBase> tiles = chunk.tileList.GetTilesInLayer(layerName).GetAllTiles();
                    this.tilemapManager.GetTilemap(layerName).SetTiles(tiles.Keys.ToArray(), tiles.Values.ToArray());
                }

                //chunks.Add(chunk);
                this.loadedChunks.Add(coord);
            }
        }

        // Set tiles that were generated
        /*foreach (Chunk c in chunks)
        {
            foreach (TileLayerName layerName in this.activeBiome.GetEnabledTileLayers())
            {
                Dictionary<Vector3Int, TileBase> tiles = c.tileList.GetTilesInLayer(layerName).GetAllTiles();
                this.tilemapManager.GetTilemap(layerName).SetTiles(tiles.Keys.ToArray(), tiles.Values.ToArray());
            }
        }*/

        this.rendering = false;
    }
}
