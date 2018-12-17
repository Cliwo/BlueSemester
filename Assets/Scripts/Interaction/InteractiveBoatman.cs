using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class InteractiveBoatman : InteractionObject {

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
    protected override string ToolTipText
    {
        get
        {
            return InterationToolTip.BASIC_INTERACTION_TOOLTIP;
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
        base.OnInteractionStart();
        XZIgnoreLookAt(transform, inst_Char.transform.position);
        inst_Conv.StartConversation(textKey, OnInteractionEnd, OnInteractionCancel);
	}

    protected override void OnInteractionCancel()
    {
        base.OnInteractionCancel();
        inst_Cam.CancelCinema();
        inst_Conv.EndConversation();
    }

    protected override void OnInteractionEnd()
    {
        base.OnInteractionEnd();
        inst_Cam.CancelCinema();
        inst_Conv.EndConversation();
		SceneManager.sceneLoaded += (_, __)=>
        {
            GameObject socket = GameObject.Find("CharacterSocket");
            CharacterManager.getInstance().gameObject.transform.position = socket.transform.position;
            CharacterManager.getInstance().gameObject.transform.rotation = socket.transform.rotation; 
        };
		SceneManager.LoadScene("WaterElectricityDungeon_nonDuplicate");
    }

    private void XZIgnoreLookAt(Transform targetTransform, Vector3 position)
    {   
        Vector2 originRotXZ = new Vector2(targetTransform.rotation.eulerAngles.x , targetTransform.rotation.eulerAngles.z);
		targetTransform.LookAt(position);
        targetTransform.rotation = Quaternion.Euler(originRotXZ.x, targetTransform.rotation.eulerAngles.y, originRotXZ.y);
    }
}
