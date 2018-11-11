using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Skill
{
    public override float SkillPreDelay
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public override float SkillPostDelay
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public override float SkillCoolDownTime
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public override float SkillActiveDuration
    {
        get
        {
            return 1.0f;
        }
    }

    public override string SoundEffectID
    {
        get
        {
            return "WaterSkill";
        }
    }

    protected override void ApplyCC(Pawn target)
    {
        throw new System.NotImplementedException();
    }
}
