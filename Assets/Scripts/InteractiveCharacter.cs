using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveCharacter : InteractionObject {

	private CharacterManager inst_Char;
    public override float InteractingTime
    {
        get
        {
            return float.PositiveInfinity;
        }
    }

    protected override void OnInteractionStart()
	{
		if(inst_Char == null)
			inst_Char = CharacterManager.getInstance();
		
		transform.LookAt(inst_Char.transform.position);
	}
}
