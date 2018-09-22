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

	protected override void OnInteracting()
	{
		Debug.Log("Tree!");
	}
}
