using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveCharacter : InteractionObject {

    public string textKey;
    private static ConversationManager inst_Conv;
	private static CharacterManager inst_Char;
    public override float InteractingTime
    {
        get
        {
            return float.PositiveInfinity;
        }
    }

    override protected void Start() {
        base.Start();
        inst_Conv = ConversationManager.getInstance();
        inst_Char = CharacterManager.getInstance();
    }

    protected override void OnInteractionStart()
	{
        CameraManager.getInstance().StartShake(2.0f);
        base.OnInteractionStart();
		transform.LookAt(inst_Char.transform.position);
        inst_Conv.StartConversation(textKey, OnInteractionEnd, OnInteractionCancel);
	}

    protected override void OnInteractionCancel()
    {
        base.OnInteractionCancel();
        Debug.Log("Conversation Canceld");
        inst_Conv.EndConversation();
    }

    protected override void OnInteractionEnd()
    {
        base.OnInteractionEnd();
        Debug.Log("Conversation End");
        inst_Conv.EndConversation();
    }

}
