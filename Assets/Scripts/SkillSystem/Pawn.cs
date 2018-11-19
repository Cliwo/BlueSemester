using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour {

	[HideInInspector]
	public List<ICrowdControl> states = new List<ICrowdControl>(); //TODO : 아래의 states, speed, hp는 ICrowdControl에 friend 처리 되는게 맞는 것 같음.
	[HideInInspector]
	public float horizontalSpeed;
	[HideInInspector]
	public float hp;
	[HideInInspector]
	/* if true, AI or User Input will be ignored */
	public bool lockOtherComponentInfluenceOnTransform = false; 

	// Use this for initialization
	virtual protected void Start () {
		InitStatus();
	}
	
	// Update is called once per frame
	virtual protected void Update () {
		List<ICrowdControl> dirtyLists = new List<ICrowdControl>();
		foreach(var state in states)
		{
			state.Update();
			if(state.isDirty)
			{
				dirtyLists.Add(state);
			}
		}
		foreach(var state in dirtyLists)
		{
			states.Remove(state);
		}
	}

	//Init all status fields
	protected abstract void InitStatus();

}
