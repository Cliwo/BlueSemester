using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDoor : InteractionObject {

	public GameObject door;

	 public override float InteractingTime
    {
        get
        {
            return 0.8f;
        }
    }

	protected override void OnInteractionStart()
	{
		door.transform.position = Vector2.Lerp(door.transform.position, 
		door.transform.position + (Vector3.down * Time.deltaTime), Time.deltaTime);
	}
}
