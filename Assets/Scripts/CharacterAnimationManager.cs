using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour {

	public Animator animator;
	private static CharacterAnimationManager instance;
	public static CharacterAnimationManager getInstance()
	{
		return instance;
	}

	void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            DestroyImmediate(this);
        }
        DontDestroyOnLoad(this);
    }
	
	public void TriggerAnimator(string trigger)
	{
		animator.SetTrigger(trigger);
	}

	public class AnimatorTrigger
	{
		public const string Punch = "Punch";
		public const string Idle = "Idle";
		public const string Walking = "Walk";

	}
}
