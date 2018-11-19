using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Skill
{
    private float Speed = 10f;
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

    public override float SkillActiveDuration
    {
        get
        {
            return 1.0f;
        }
    }
    public override float Damage
    {
        get
        {
            return 3.0f;
        }
    }

    public override string SoundEffectID
    {
        get
        {
            return "WaterSkill";
        }
    }
    protected override int targetLayer
    {
        get
        {
            return ENEMY_LAYER;
        }
    }

    protected override bool isProjectile
    {
        get
        {
            return true;
        }
    }


    override protected void ApplyCC(Pawn target)
    {
        target.states.Add(new Slow(target));
    }
    
    override protected void Update()
    {
        base.Update();
        //transform.Translate(marchingDirection * Time.deltaTime * Speed);
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
}
