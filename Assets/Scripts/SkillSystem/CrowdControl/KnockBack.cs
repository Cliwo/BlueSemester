using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : ICrowdControl
{
	private static float knockBackMagnitude = 0.2f;
    public KnockBack(Pawn owner) : base(owner)
    {
    }

    public override float ActiveDuration
    {
        get
        {
            return 0.1f;
        }
    }

    public override void OnActivate()
    {
		owner.lockOtherComponentInfluenceOnTransform = true; 
    }

    public override void OnDeactivate()
    {
        owner.lockOtherComponentInfluenceOnTransform = false;
    }

    public override void Update()
    {
        CheckDeactivate();
		owner.transform.Translate(Vector3.back * knockBackMagnitude);
    }
}
