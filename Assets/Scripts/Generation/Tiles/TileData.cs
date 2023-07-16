using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/TileData")]
public class TileData : ScriptableObject
{
    [SerializeField] private bool isWater = false;
    public bool IsWater()
    {
        return isWater;
    }
}
