using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : ICrowdControl
{
    public Slow(Pawn owner) : base(owner)
    {
    }

    public override float ActiveDuration
    {
        get
        {
            return 2.0f;
        }
    }

    public override void OnActivate()
    {
        owner.horizontalSpeed -= 0.1f;
    }

    public override void OnDeactivate()
    {
        owner.horizontalSpeed += 0.1f;
    }

    public override void Update()
    {
        CheckDeactivate();
    }
}
