using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : ICrowdControl
{
    public Stun(Pawn owner) : base(owner)
    {
    }

    public override float ActiveDuration
    {
        get
        {
            return 1.5f;
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
    }
}
