using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseSkill : ScriptableObject
{
    // Name & such
    public string skillName = "Template name";
    public string description = "Template description";

    [SerializeField] protected bool active = false;
    private bool doUpdate = false;
    protected SkillController controller;

    public virtual void Initialize(SkillController controller)
    {
        this.controller = controller;
    }
    public virtual void OnActivate()
    {
        this.active = true;
    }
    public virtual void OnDeactivate()
    {
        this.active = false;
    }

    public bool IsActive()
    {
        return this.active;
    }
}
