using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cooldown
{
    public float cooldownTime = 0.5f;
    private float endTime = 0;

    public bool isDone => Time.time > endTime;

    public void StartCooldown()
    {
        this.endTime = Time.time + this.cooldownTime;
    }
}
