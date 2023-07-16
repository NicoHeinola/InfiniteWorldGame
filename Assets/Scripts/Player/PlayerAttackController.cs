using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    PlayerStatsController pStats;
    [SerializeField] private Cooldown punchCooldownTimer = new Cooldown();
    [SerializeField] private GameObject punchEffect;
    [SerializeField] private Transform punchEffectPoint;

    private void Start()
    {
        this.pStats = gameObject.GetComponent<PlayerStatsController>();
    }
    private void DoPunch(float angle, Vector3 dir)
    {
        if (this.punchCooldownTimer.isDone)
        {
            this.punchCooldownTimer.cooldownTime = pStats.punchCooldown.GetValue();
            this.punchCooldownTimer.StartCooldown();

            /* ---------------------- */
            // Calculate x (b of triangle) and y (a of triangle) using the angle and reach (aka. c of triangle)

            //Debug.DrawRay(transform.position, dir);

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, pStats.GetPunchReach(), 1 << LayerMask.NameToLayer("Enemy"));
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.3f, dir, pStats.punchReach.GetValue(), 1 << LayerMask.NameToLayer("Enemy"));

            // Loop each enemy that was hit
            int pierced = 0;

            // Create punch effect
            Instantiate(punchEffect, punchEffectPoint.position + dir, Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle), this.transform);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    // Ìf hit enemy
                    if (hit.collider.gameObject.CompareTag("enemy"))
                    {
                        // 'Attack' enemy
                        EnemyStatsController eStats = hit.collider.gameObject.GetComponent<EnemyStatsController>();
                        eStats.TakeDamage(pStats.punchDamage.GetValue(), this.pStats);
                    }
                }

                pierced++;
                if (pierced >= pStats.punchPierce.GetValue())
                {
                    break;
                }
            }
        }
    }
    public void Punch(int dirX, int dirY)
    {
        if (dirX == 0 && dirY == 0) dirX = 1;
        float angle = Mathf.Atan2(dirY, dirX);
        this.DoPunch(angle, new Vector3(dirX, dirY, 0));
    }
    public void Punch(Vector2 pos)
    {
        /* Calculate reach vector */
        // Get angle of trianle
        float angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x);
        float x = transform.position.x + Mathf.Cos(angle) * pStats.punchReach.GetValue();
        float y = transform.position.y + Mathf.Sin(angle) * pStats.punchReach.GetValue();
        pos = new Vector2(x, y);

        Vector3 dir = ((Vector2) transform.position - pos) * -1;
        this.DoPunch(angle, dir);
    }
}