using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBox : InteractionObject
{
    public override float InteractingTime
    {
        get
        {
			return 2.0f;
        }
    }

	protected override void OnInteracting()
	{
		Debug.Log("Box!");
	}
}
