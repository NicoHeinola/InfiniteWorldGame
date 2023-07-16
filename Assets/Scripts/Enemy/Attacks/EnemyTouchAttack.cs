using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchAttack : BaseAI
{
    [SerializeField] private Cooldown atkCooldown = new Cooldown();
    [SerializeField] private Vector2 attackBoxSize = new Vector2(1, 1);

    private void FixedUpdate()
    {
        if (!this.atkCooldown.isDone) return;

        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, this.attackBoxSize, 0, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Character"));
        if (hit.collider != null && hit.collider.gameObject.CompareTag(UtilityFunctions.playerTag))
        {
            hit.collider.gameObject.GetComponent<PlayerStatsController>().TakeDamage(this.eStats.GetDamage());

            // Reset cooldown
            atkCooldown.cooldownTime = this.eStats.GetAttackCooldown();
            this.atkCooldown.StartCooldown();
        }
    }
}
