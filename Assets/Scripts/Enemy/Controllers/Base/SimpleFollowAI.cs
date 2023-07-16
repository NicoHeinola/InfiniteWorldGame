using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimpleFollowAI : BaseAI
{
    private Transform toFollow;
    public string toFollowTag;
    protected override void Start()
    {
        base.Start();
        this.toFollow = GameObject.FindGameObjectWithTag(this.toFollowTag).transform;
    }
    private void Update()
    {
        this.HandleMovement();
    }
    public void HandleMovement()
    {
        Vector3 dir = (toFollow.position - this.transform.position).normalized;
        this.transform.position = new Vector3(this.transform.position.x + this.eStats.GetMoveSpeed() * dir.x * Time.deltaTime, this.transform.position.y + this.eStats.GetMoveSpeed() * dir.y * Time.deltaTime, this.transform.position.z);
    }
}
