using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseTester : MonoBehaviour
{
    public RawImage image;
    public SimpleNoise noise;
    public int w;
    public int h;
    public bool blackAndWhite = true;
    public float maxValue = 1f;
    public void OnValidate()
    {
        Texture2D texture = new Texture2D(w, h);
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                float value = this.noise.GetNoise(x, y);
                if (this.blackAndWhite)
                {
                    if (value <= this.maxValue)
                    {
                        value = 0;
                    }
                }
                texture.SetPixel(x, y, new Color(value, value, value));
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        this.image.texture = texture;
    }
}
