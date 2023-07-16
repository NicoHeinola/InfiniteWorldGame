using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimpleNoise
{
    public int seed = 0;
    public float frequenzy = 0.5f;
    public float scale = 1f;
    public float GetNoise(int x, int y)
    {
        return Mathf.PerlinNoise(x / frequenzy + seed, y / frequenzy + seed) * scale;
    }
}
