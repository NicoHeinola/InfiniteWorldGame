using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawner : MonoBehaviour
{
    public Transform centerPoint;
    public int despawnRadius = 30;
    public Cooldown despawnTimer = new Cooldown();
    private void FixedUpdate()
    {
        if (!despawnTimer.isDone) return;
        despawnTimer.StartCooldown();

        List<GameObject> toRemove = new List<GameObject>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            if (CheckDespawn(child))
            {
                toRemove.Add(child);
            }
        }

        foreach (GameObject remove in toRemove)
        {
            Destroy(remove);
        }
    }
    public bool CheckDespawn(GameObject enemy)
    {
        if (Vector3.Distance(this.centerPoint.position, enemy.transform.position) > this.despawnRadius)
        {
            return true;
        }
        return false;
    }
}
