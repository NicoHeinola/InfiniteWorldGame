using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunctions
{
    public static string playerTag = "player";
    public static GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag(playerTag);
    }
}
