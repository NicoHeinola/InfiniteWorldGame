using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStatsController : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed;

    [SerializeField] private float baseDamage; // Base damage of the enemy
    [SerializeField] private float atkCooldown; // Cooldown of attacks
    [SerializeField] private int maxHp;
    [SerializeField] private int hp;

    [SerializeField] private int xp = 10;

    [SerializeField] private EnemyDeathController deathController;

    private Animator animator;

    private void Start()
    {
        this.hp = this.maxHp;
        this.animator = GetComponent<Animator>();
        this.deathController.Initialize(gameObject, this);
    }

    public void TakeDamage(float damage, PlayerStatsController whoHit)
    {
        animator.SetTrigger("damaged");
        this.hp -= (int)damage;
        deathController.CheckDeath(whoHit);
    }
    public int GetXp() { return xp; }
    public int GetHp() { return hp; }
    public void SetBaseDamage(float damage) { this.baseDamage = damage; }
    public float GetDamage() { return this.baseDamage; }
    public float GetAttackCooldown() { return atkCooldown; }
    public float GetMoveSpeed() { return baseMoveSpeed; }
}
