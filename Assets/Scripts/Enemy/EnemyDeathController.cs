using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDeathController
{
    private GameObject enemy; // Enemy
    private EnemyStatsController eStatsController;
    [SerializeField] private GameObject xpOrb;

    public void Initialize(GameObject enemy, EnemyStatsController eStatsController)
    {
        this.enemy = enemy;
        this.eStatsController = eStatsController;
    }
    public void CheckDeath(PlayerStatsController pStats)
    {
        if (eStatsController.GetHp() <= 0)
        {
            GameObject newXpOrb = GameObject.Instantiate(xpOrb, enemy.transform.position, Quaternion.identity);
            newXpOrb.GetComponent<XpMagnet>().xpAmount = this.eStatsController.GetXp(); // Set xp of this orb
            Object.Destroy(enemy);
        }
    }
}
