using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Cooldown enemySpawnCooldown = new Cooldown();
    public GameObject enemyToSpawn;
    public Transform centerPoint;

    public float maxRadius = 25;
    public float minRadius = 5;

    public bool drawGizmos = true;

    private void Update()
    {
        if (enemySpawnCooldown.isDone)
        {
            enemySpawnCooldown.StartCooldown();

            Vector3 spawnPoint = GetRandomSpawnPosition();

            //GameObject spawned = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
            GameObject spawned = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity, this.transform);
        }
    }
    private Vector3 GetRandomSpawnPosition()
    {
        float radius = Random.Range(minRadius, maxRadius);
        float angle = Random.Range(0, 360);

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        return new Vector3(x + centerPoint.position.x, y + centerPoint.position.y, 0);
    }
    private void OnDrawGizmos()
    {
        if (!drawGizmos)
        {
            return;
        }
        Gizmos.color = new Color(122, 0, 0, 0.1f);
        Gizmos.DrawSphere(this.transform.position, this.maxRadius);

        Gizmos.color = new Color(0, 122, 0, 0.1f);
        Gizmos.DrawSphere(this.transform.position, this.minRadius);
    }
}
