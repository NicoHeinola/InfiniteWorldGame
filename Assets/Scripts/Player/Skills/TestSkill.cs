using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/TestSkill")]
public class TestSkill : BaseSkill
{
    public override void Initialize(SkillController controller)
    {
        base.Initialize(controller);
    }
    private void OnUpdate()
    {
        this.controller.GetPlayerStats().AddXp(1);
    }

    public override void OnActivate()
    {
        this.controller.AddSkillUpdate(this.OnUpdate);
        base.OnActivate();
    }
    public override void OnDeactivate()
    {
        this.controller.RemoveSkillUpdate(this.OnUpdate);
        base.OnDeactivate();
    }
}
