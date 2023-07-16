using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public bool validate = true;

    public ChunkLoader chunkLoader;
    public BaseBiome biome;

    private void Start()
    {
        chunkLoader.SetActiveBiome(this.biome);
    }
}
