using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveCharacter : InteractionObject {

    public string textKey;
    private static ConversationManager inst_Conv;
	private static CharacterManager inst_Char;
    private static CameraManager inst_Cam;
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
        inst_Cam = CameraManager.getInstance();
    }

    protected override void OnInteractionStart()
	{
        inst_Cam.StartShake(2.0f);
        inst_Cam.StartCinema();
        base.OnInteractionStart();
		transform.LookAt(inst_Char.transform.position);
        inst_Conv.StartConversation(textKey, OnInteractionEnd, OnInteractionCancel);
	}

    protected override void OnInteractionCancel()
    {
        base.OnInteractionCancel();
        inst_Cam.CancelCinema();
        Debug.Log("Conversation Canceld");
        inst_Conv.EndConversation();
    }

    protected override void OnInteractionEnd()
    {
        base.OnInteractionEnd();
        inst_Cam.CancelCinema();
        Debug.Log("Conversation End");
        inst_Conv.EndConversation();
    }

}
