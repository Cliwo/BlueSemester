using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class InteractiveFacade : InteractionObject
{
	[SerializeField]
	private CheckDialog checkDialog;
    public override float InteractingTime
    {
        get
        {
			return 0.1f;
        }
    }
	protected override void OnInteractionEnd()
    {
        base.OnInteractionEnd();
        /* 원래 find를 최대한 자제해야함 혹시 시간나면 리팩토링하기 */
        SceneManager.sceneLoaded += (_, __)=>
        {
            GameObject socket = GameObject.Find("CharacterSocket");
            CharacterManager.getInstance().gameObject.transform.position = socket.transform.position;
            CharacterManager.getInstance().gameObject.transform.rotation = socket.transform.rotation; 
        };
		checkDialog.OpenDialog(CheckDialog.ENTER_DUNGEON, ()=> { SceneManager.LoadScene("Dungeon4_nonDuplication"); } );
    }
}
