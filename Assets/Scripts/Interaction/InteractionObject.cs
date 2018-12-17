using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public abstract class InteractionObject : MonoBehaviour 
{
    private static InterationToolTip inst_toolTipUI;
	abstract public float InteractingTime { get; }
    virtual protected string ToolTipText { get { return null; } }
    protected static InputManager inst_Input;
	protected static CharacterAnimationManager inst_Animation;
	protected InteractionEventBundle bundle;
	protected float startTime;
	protected bool isStarted = false;
	private bool isToolTipVisible = false;

	public class InteractionEventBundle
	{
		public InteractionObject eventOwner;
		public Action startAction;
		public Action cancelAction;
	}
	protected virtual void Start() {
		inst_Animation = CharacterAnimationManager.getInstance();
		inst_Input = InputManager.getInstance();
        inst_toolTipUI = InterationToolTip.getInstance();
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
            if(ToolTipText != null)
            {
                inst_toolTipUI.ShowToolTip(ToolTipText);
				isToolTipVisible = true;
            }
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.GetComponent<CharacterManager>() != null)
		{
			inst_Input.InteractionBundles.Remove(bundle);
            if (ToolTipText != null && inst_Input.InteractionBundles.Count == 0)
            {
                inst_toolTipUI.HideToolTip();
            }
		}
	}

	protected virtual void OnInteractionStart()
	{
		startTime = Time.time;
		isStarted = true;
		if(isToolTipVisible)
		{
			inst_toolTipUI.HideToolTip();
		}
	}
	protected virtual void OnInteracting()
	{

	}

	protected virtual void OnInteractionEnd()
	{
		isStarted = false;
		//TODO : 여기서 직접 Input을 참조하는게 맞는걸까?
		inst_Input.currentInteraction = null;
	}
	protected virtual void OnInteractionCancel()
	{
		isStarted = false;
		//TODO : 여기서 직접 Input을 참조하는게 맞는걸까?
		inst_Input.currentInteraction = null;
	}
}
