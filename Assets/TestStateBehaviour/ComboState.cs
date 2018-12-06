using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboState : StateMachineBehaviour {
	private static CharacterManager character;
	public int comboID;
	public float duration;

	private float timeBucket;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
	{
		if(character == null)
		{
			character = CharacterManager.getInstance();
		}
		character.currentAnimationFSM = this;
		timeBucket = Time.time;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
	{
		if(Time.time - timeBucket > duration)
		{
			character.OnComboTimeOut();
			Debug.Log("ID : " + comboID);

		}
	}
	public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
	{
		if(comboID == 2)
			character.OnComboTimeOut();
	}
	
}
