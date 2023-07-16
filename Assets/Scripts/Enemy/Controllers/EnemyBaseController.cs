using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBaseController : MonoBehaviour
{
    protected Rigidbody2D rgBody;
    protected Animator animator;
    protected EnemyStatsController eStats; // Enemy stats

    public GlobalEnemyOptions gEnemyOptions;
    private Transform centerPos;

    protected bool doUpdate;

    private List<BaseAI> AIs;
    private List<bool> enabledStatuses;

    protected virtual void Start()
    {
        this.AIs = new List<BaseAI>();
        this.enabledStatuses = new List<bool>();

        this.centerPos = GameObject.FindGameObjectWithTag(this.gEnemyOptions.findCenterPositionWithTag).transform;
        this.animator = GetComponent<Animator>();

        // Create a rigidbody
        this.rgBody = this.AddComponent<Rigidbody2D>();
        this.rgBody.angularDrag = 0;
        this.rgBody.drag = 0;
        this.rgBody.gravityScale = 0;
        this.rgBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        this.checkUpdate(true);
    }

    protected void AddAI(BaseAI ai)
    {
        this.AIs.Add(ai);
        this.enabledStatuses.Add(false);
    }

    private void FixedUpdate()
    {
        this.checkUpdate();
    }

    private void checkUpdate(bool ignore = false)
    {
        bool update = Vector3.Distance(this.centerPos.position, this.transform.position) < this.gEnemyOptions.maxUpdateDistance;

        if (this.doUpdate != update || ignore)
        {
            this.doUpdate = update;
            if (update)
            {
                this.OnEnableUpdate();
            }
            else
            {
                this.OnDisableUpdate();
            }

            this.animator.enabled = update;
        }

    }

    protected virtual void OnDisableUpdate()
    {
        for (int i = 0; i < this.AIs.Count; i++)
        {
            BaseAI ai = this.AIs[i];
            this.enabledStatuses[i] = ai.Active;
            ai.SetDisabled();
        }
    }

    protected virtual void OnEnableUpdate()
    {
        for (int i = 0; i < this.AIs.Count; i++)
        {
            BaseAI ai = this.AIs[i];
            if (this.enabledStatuses[i])
            {
                ai.SetEnabled();
            }
        }
    }
}
