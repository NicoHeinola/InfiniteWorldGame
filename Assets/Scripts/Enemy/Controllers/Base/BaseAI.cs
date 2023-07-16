using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    protected EnemyStatsController eStats;
    public bool Active { get { return this.enabled; } }

    protected virtual void Start()
    {
        this.eStats = GetComponent<EnemyStatsController>();
    }

    public void SetEnabled(bool enabled)
    {
        if (enabled) this.SetEnabled();
        else this.SetDisabled();
    }

    public virtual void SetEnabled()
    {
        this.enabled = true;
    }
    public virtual void SetDisabled()
    {
        this.enabled = false;
    }
}
