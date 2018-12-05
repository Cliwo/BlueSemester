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
		checkDialog.OpenDialog(CheckDialog.ENTER_DUNGEON, ()=> { SceneManager.LoadScene("Dungeon4"); } );
    }
}
