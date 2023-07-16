using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkOptions : MonoBehaviour
{
    [SerializeField] private int chunkSize = 16;
    [SerializeField] private int renderDistance = 2;
    [SerializeField] private int renderChunksPerFrame = 5;

    public int ChunkSize { get { return chunkSize; } }
    public int RenderDistance { get { return renderDistance; } }
    public int RenderChunksPerFrame { get { return renderChunksPerFrame; } }

    public void SetChunkSize(int size)
    {
        chunkSize = size;
    }

    public void SetRenderDistance(float renderDistance)
    {
        this.renderDistance = (int)renderDistance;
    }


    public void SetRenderDistance(int renderDistance)
    {
        this.renderDistance = renderDistance;
    }

    public void SetRenderedChunksPerFrame(float renderChunksPerFrame)
    {
        this.renderChunksPerFrame = (int)renderChunksPerFrame;
    }

    public void SetRenderedChunksPerFrame(int renderChunksPerFrame)
    {
        this.renderChunksPerFrame = renderChunksPerFrame;
    }
}
