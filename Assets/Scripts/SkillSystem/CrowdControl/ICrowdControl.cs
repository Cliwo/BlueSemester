using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICrowdControl {

	//Crowd Control 의 interface 
	public bool isDirty;
	protected float instantiatedTime;
	protected Pawn owner; 
	abstract public float ActiveDuration { get; }
	abstract public void Update();

	public ICrowdControl(Pawn owner)
	{
		instantiatedTime = Time.time;
		isDirty = false;
		this.owner = owner;
		OnActivate();
	}
	abstract public void OnActivate();

	abstract public void OnDeactivate();

	protected void CheckDeactivate()
	{
		if(Time.time > instantiatedTime + ActiveDuration)
		{
			OnDeactivate();
			isDirty = true;
		}
	}

}
