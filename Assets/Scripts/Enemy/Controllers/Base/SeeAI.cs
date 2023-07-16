using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeeAI : BaseAI
{
    public string targetTag = "player";
    public float seeRadius = 3f;
    public float followRadius = 12f;

    private Transform targetPos;
    private bool follow = false;
    private bool see = false;

    public bool See { get { return see; } private set { see = value; } }
    public bool Follow { get { return follow; } private set { follow = value; } }

    private List<Action<bool>> onSeeChange = new List<Action<bool>>(); // Called when visibility changed

    public bool IsFollowing() { return follow; }

    protected override void Start()
    {
        base.Start();

        //this.onSeeChange = new List<Action<bool>>();
        this.targetPos = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    private void FixedUpdate()
    {
        this.CheckIfSee();
    }

    public void AddOnSeeChangeFunction(Action<bool> callback)
    {
        this.onSeeChange.Add(callback);
    }

    public static bool CanSee(Vector2 currentPos, Vector2 toPos)
    {
        RaycastHit2D hit = Physics2D.Linecast(currentPos, toPos, 1 << LayerMask.NameToLayer("Ground"));
        return hit.collider == null;
    }
    private void CheckIfSee()
    {
        float distance = Vector2.Distance(targetPos.position, this.transform.position);
        bool origSee = this.see;
        bool origFollow = this.follow;

        // If in follow radius
        if (distance < followRadius)
        {
            this.follow = true;
            this.see = distance < seeRadius;

        }
        else
        {
            this.follow = false;
            this.see = false;
        }

        // Checks if there was a change in follow or see
        if (origSee != this.see || origFollow != this.follow)
        {
            foreach (Action<bool> action in this.onSeeChange)
            {
                action(this.follow);
            }
        };
    }
}
