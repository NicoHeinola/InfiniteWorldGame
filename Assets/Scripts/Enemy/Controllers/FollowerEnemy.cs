using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : EnemyBaseController
{
    //public string followWithTag;
    //  private Transform toFollow;

    private RandomMoveAI randomMoveAI;
    private SimpleFollowAI followAI;
    private SeeAI seeAI;

    protected override void Start()
    {
        base.Start();

        this.randomMoveAI = gameObject.GetComponent<RandomMoveAI>();
        this.randomMoveAI.SetEnabled();
        this.AddAI(this.randomMoveAI);

        this.followAI = gameObject.GetComponent<SimpleFollowAI>();
        this.followAI.SetDisabled();
        this.AddAI(this.followAI);

        this.seeAI = gameObject.GetComponent<SeeAI>();
        this.seeAI.SetEnabled();
        this.seeAI.AddOnSeeChangeFunction(this.OnSeeChange);
        this.AddAI(this.seeAI);
    }

    private void OnSeeChange(bool see)
    {
        if (see)
        {
            this.randomMoveAI.SetDisabled();
            this.followAI.SetEnabled();
        }
        else
        {
            this.randomMoveAI.ResetPosition();

            this.randomMoveAI.SetEnabled();
            this.followAI.SetDisabled();
        }
    }
}
