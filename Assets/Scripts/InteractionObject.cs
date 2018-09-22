using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public abstract class InteractionObject : MonoBehaviour {
	abstract public float InteractingTime { get; }
	protected static InputManager inst_Input;
	protected static CharacterAnimationManager inst_Animation;
	protected InteractionEventBundle bundle;
	protected float startTime;
	protected bool isStarted = false;
	public class InteractionEventBundle
	{
		public InteractionObject eventOwner;
		public Action startAction;
		public Action cancelAction;
	}
	void Start() {
		inst_Animation = CharacterAnimationManager.getInstance();
		inst_Input = InputManager.getInstance();
		bundle = new InteractionEventBundle
				{
					eventOwner = this,
					startAction = OnInteractionStart,
					cancelAction = OnInteractionCancel
				};
	}
	void Update() {
		if(isStarted)
		{
			if(startTime + InteractingTime > Time.time)
			{
				OnInteracting();
			}
			else
			{
				OnInteractionEnd();
			}
		}
	}
	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<CharacterManager>() != null)
		{
			inst_Input.InteractionBundles.Add(bundle);
			Debug.Log("Bundle Add");
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.GetComponent<CharacterManager>() != null)
		{
			inst_Input.InteractionBundles.Remove(bundle);
			Debug.Log("Bundle Removed");
		}
	}

	protected virtual void OnInteractionStart()
	{
		startTime = Time.time;
		isStarted = true;
		Debug.Log("Start");
	}
	protected virtual void OnInteracting()
	{

	}

	protected virtual void OnInteractionEnd()
	{
		isStarted = false;
		//TODO : 여기서 직접 Input을 참조하는게 맞는걸까?
		inst_Input.currentInteraction = null;
		Debug.Log("Complete");
	}
	protected virtual void OnInteractionCancel()
	{
		isStarted = false;
		//TODO : 여기서 직접 Input을 참조하는게 맞는걸까?
		inst_Input.currentInteraction = null;
		Debug.Log("Canceled");
	}
}
