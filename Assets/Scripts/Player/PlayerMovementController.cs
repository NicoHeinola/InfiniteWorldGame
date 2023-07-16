using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private PlayerStatsController pStats;
    public int dirX = 0;
    public int dirY = 0;
    public bool moving = false;


    private Rigidbody2D rgBody;

    // Dash
    private Cooldown dashCooldown = new Cooldown();
    float currentDashTime;
    public bool dashing = false;

    public BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        this.pStats = gameObject.GetComponent<PlayerStatsController>();
    }

    private void RegisterMovement(int dirX, int dirY)
    {
        this.dirX = dirX;
        this.dirY = dirY;
        if (dirX == 0 && dirY == 0)
        {
            rgBody.velocity = Vector2.zero;
            this.moving = false;
        }
        else
        {
            this.moving = true;
        }
    }

    private bool ProperMove(float speed)
    {
        Vector2 newPos = new Vector2(transform.position.x + speed * this.dirX * Time.deltaTime, transform.position.y + speed * this.dirY * Time.deltaTime);
        return this.ProperMove(newPos);
    }
    private bool ProperMove(Vector2 newPos)
    {
        // Use normal update for non-collision movement
        Vector2 collisionTestPos = newPos + boxCollider.offset;
        bool collider = Physics2D.OverlapBox(collisionTestPos, boxCollider.size, 0, 1 << LayerMask.NameToLayer("Ground"));
        // If not collided, use normal movement
        if (!collider)
        {
            rgBody.velocity = Vector2.zero;
            transform.position = newPos;
            return false;
        }
        else
        {
            rgBody.velocity = new Vector2(this.dirX * this.pStats.moveSpeed.GetValue(), this.dirY * this.pStats.moveSpeed.GetValue());
            return true;
        }
    }
    public void Move(int dirX, int dirY)
    {
        // Can't register movement if dashing
        if (this.dashing) return;

        this.RegisterMovement(dirX, dirY);
        // If not moving return;
        if (!this.moving) return;

        this.ProperMove(pStats.moveSpeed.GetValue());
    }
    public void Dash(int dirX, int dirY)
    {
        // Can't register movemnt if dashing
        if (this.dashing) return;
        this.RegisterMovement(dirX, dirY);

        if (this.moving && this.dashCooldown.isDone)
        {
            this.dashing = true;

            StartCoroutine(DashRoutine(new Vector2(dirX, dirY)));
        }
    }
    IEnumerator DashRoutine(Vector2 direction)
    {
        currentDashTime = pStats.dashDuration.GetValue();

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime;

            bool collided = this.ProperMove(pStats.dashSpeed.GetValue());
            if (collided) break;
            yield return null;
        }
        rgBody.velocity = new Vector2(0f, 0f); // Stop dashing.

        dashCooldown.cooldownTime = pStats.dashCooldown.GetValue();
        dashCooldown.StartCooldown();
        dashing = false;
    }
}
