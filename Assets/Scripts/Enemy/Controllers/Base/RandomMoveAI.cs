using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class RandomMoveAI : BaseAI
{
    [SerializeField] private Cooldown cooldown = new Cooldown();
    public float moveLength = 1;
    public float moveSlow = 0.25f;
    public float maxSpeed = 10;

    private Vector3 toPos = new Vector3();
    private Vector3 velocity = new Vector3();

    public void SetToPos(Vector3 toPos)
    {
        this.toPos = toPos;
    }

    public void ResetCooldown()
    {
        cooldown.StartCooldown();
    }

    public void ResetPosition()
    {
        this.toPos = this.transform.position;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void NewToPos()
    {
        float angle = Random.Range(0, 360);
        float newX = Mathf.Cos(angle) * this.moveLength;
        float newY = Mathf.Sin(angle) * this.moveLength;

        this.toPos.x = this.transform.position.x + newX;
        this.toPos.y = this.transform.position.y + newY;
        this.toPos.z = this.transform.position.z;
        this.velocity = new Vector3(0, 0, 0);
    }
    private void HandleMovement()
    {
        if (cooldown.isDone)
        {
            this.NewToPos();
            cooldown.StartCooldown();
        }
        else
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, this.toPos, ref velocity, moveSlow, maxSpeed);
        }
    }
}
