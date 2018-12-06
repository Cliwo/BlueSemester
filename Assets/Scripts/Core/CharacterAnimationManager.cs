using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    public Animator animator;
    private static CharacterAnimationManager instance;

    public static CharacterAnimationManager getInstance()
    {
        return instance;
    }

    private InputManager inst_input;

    private void Awake()
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

    private void Start()
    {
        inst_input = InputManager.getInstance();
    }

    public void TriggerAnimator(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public class AnimatorTrigger
    {
        public const string Punch = "Punch";
        public const string Idle = "Idle";
        public const string Translation = "TranslationMagnitude";
        public const string Skill1 = "Skill1";
        public const string Skill2 = "Skill2";
        public const string Skill3 = "Skill3";
        public const string MeleeAttack = "MeleeAttack";
    }
}