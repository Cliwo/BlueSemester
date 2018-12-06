using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTreasureBox : InteractionObject
{
	[SerializeField]
    private Animator animator;
    public override float InteractingTime
    {
        get
        {
			return 0.1f;
        }
    }

	protected override void OnInteractionEnd()
    {
        base.OnInteractionEnd();
		animator.SetTrigger("open");
        inst_Input.InteractionBundles.Remove(bundle);
    }
}
