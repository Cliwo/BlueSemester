using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTree : InteractionObject
{
    public override float InteractingTime
    {
        get
        {
            return 3.0f;
        }
    }
    protected override void OnInteractionStart()
    {
        base.OnInteractionStart();
        inst_Animation.TriggerAnimator(CharacterAnimationManager.AnimatorTrigger.Punch);
    }
	protected override void OnInteracting()
	{
        
	}
    protected override void OnInteractionCancel()
    {
        base.OnInteractionCancel();
        inst_Animation.TriggerAnimator(CharacterAnimationManager.AnimatorTrigger.Idle);
    }
    protected override void OnInteractionEnd()
    {
        base.OnInteractionEnd();
        inst_Animation.TriggerAnimator(CharacterAnimationManager.AnimatorTrigger.Idle);
    }
}
