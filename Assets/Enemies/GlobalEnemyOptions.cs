using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Options/GlobalEnemyOption")]
public class GlobalEnemyOptions : ScriptableObject
{
    public string findCenterPositionWithTag = "player";
    public float maxUpdateDistance = 20f;
}
