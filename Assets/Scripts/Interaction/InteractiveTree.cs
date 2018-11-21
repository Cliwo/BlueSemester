using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTree : InteractionObject
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private ParticleSystem particle_rock;

    [SerializeField]
    private ParticleSystem particle_dust;

    private ItemDrop itemDrop;

    public override float InteractingTime
    {
        get
        {
            return 2.0f;
        }
    }

    protected override void OnInteractionStart()
    {
        base.OnInteractionStart();
        inst_Animation.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, false);
        inst_Animation.TriggerAnimator(CharacterAnimationManager.AnimatorTrigger.Punch);
        particle_dust.Play();
        particle_rock.Play();
    }

    protected override void OnInteracting()
    {
    }

    protected override void OnInteractionCancel()
    {
        base.OnInteractionCancel();
        inst_Animation.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, true);
    }

    protected override void OnInteractionEnd()
    {
        base.OnInteractionEnd();
        inst_Animation.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, true);

        itemDrop = GetComponent<ItemDrop>();
        itemDrop.canDrop = true;
        Destroy(this.gameObject, 1f);
        inst_Input.InteractionBundles.Remove(bundle);
        Debug.Log("Bundle Removed");
        animator.SetTrigger("Fall");
    }
}