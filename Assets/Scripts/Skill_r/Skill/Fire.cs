using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Skill
{
	
    public override string SoundEffectID
    {
        get
        {
            return "FireSkill";
        }
    }

    public override float SkillActiveDuration
    {
        get
        {
            return 2.0f;
        }
    }

    public override float SkillPreDelay
    {
        get
        {
            return 0.0f;
        }
    }

    public override float SkillPostDelay
	{
		get
		{
			return 0.0f;
		}
	}

    public override float SkillCoolDownTime
	{
		get
		{
			return 0.0f;
		}
	}

    protected override void ApplyCC(Pawn target)
    {
        target.states.Add(new KnockBack(target));
    }
}
