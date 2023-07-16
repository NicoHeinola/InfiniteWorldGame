using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{

    [Header("Leveling")]
    [SerializeField] private int level = 1;
    [SerializeField] private int xp = 0;
    [SerializeField] private int nextLevelXp = 100;
    [SerializeField] private int statPoints = 0;

    [Header("Movement")]
    public FloatPlayerStat moveSpeed = new FloatPlayerStat();
    public FloatPlayerStat dashSpeed = new FloatPlayerStat();
    public FloatPlayerStat dashDuration = new FloatPlayerStat();
    public FloatPlayerStat dashCooldown = new FloatPlayerStat();

    [Header("Basic Stats")]
    public IntPlayerStat maxHp = new IntPlayerStat();
    public int hp;
    public FloatPlayerStat damage = new FloatPlayerStat();

    [Header("Punch Stats")]
    public FloatPlayerStat punchDamage = new FloatPlayerStat();
    public FloatPlayerStat punchReach = new FloatPlayerStat();
    public FloatPlayerStat punchCooldown = new FloatPlayerStat();
    public IntPlayerStat punchPierce = new IntPlayerStat();

    // Skills
    PlayerSkillController pSkills;

    private PlayerMovementController moveController;

    private Animator animator;

    // Getters
    public int Level { get { return level; } private set { level = value; } }

    private void Start()
    {
        this.pSkills = GetComponent<PlayerSkillController>();

        this.moveController = GetComponent<PlayerMovementController>();
        this.animator = GetComponent<Animator>();
        hp = maxHp.GetValue();
    }
    public void AddXp(int xp)
    {
        this.xp += xp;
        // Level up until xp is no longer bigger than required
        while (this.xp >= nextLevelXp)
        {
            this.xp -= nextLevelXp;
            LevelUp(); // Level up
        }
    }

    private void Update()
    {
    }
    public void LevelUp()
    {
        this.level++;
        this.nextLevelXp = Mathf.RoundToInt(nextLevelXp * 1.1f) + 50;
        this.statPoints += 5;
    }
    public void TakeDamage(float damage)
    {
        if (this.moveController.dashing) return;
        this.animator.SetTrigger("damaged");

        this.hp -= (int)damage;
    }

    private bool canSpendPoints(int points)
    {
        return points <= this.statPoints;
    }
    private void spendPoints(int points)
    {
        this.statPoints -= points;
    }
    public void upgradeMovement(int points)
    {
        if (!this.canSpendPoints(points)) return;
        this.spendPoints(points);

        this.moveSpeed.AddAddition(points / 2f);
    }
    public void upgradeMaxHp(int points)
    {
        if (!this.canSpendPoints(points)) return;
        this.spendPoints(points);
        this.maxHp.AddAddition(points * 3);
    }
    public void upgradeDamage(int points)
    {
        if (!this.canSpendPoints(points)) return;
        this.spendPoints(points);
        this.maxHp.AddAddition(points);
    }
    public int GetXp() { return this.xp; }
    public int GetNextXp() { return this.nextLevelXp; }

}
