using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Skill
{
    /* 스킬 종류별로 쿨타임이 달라야하기 때문 */
	private static float LastTime;
    public override float SkillLastUsedTimePerKind 
    {
        get
        {
            return LastTime;
        }
        protected set
        {
            LastTime = value;
        }
    }
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
            return 3.0f;
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
			return 2.0f;
		}
	}

    protected override int targetLayer
    {
        get
        {
            return PLAYER_LAYER;
        }
    }

    protected override bool isProjectile
    {
        get
        {
            return false;
        }
    }

    protected override void ApplyCC(Pawn target)
    {
        target.states.Add(new KnockBack(target));
    }
}
