using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
public class SceneChanger : MonoBehaviour {
	
	public GameStartButton startStatus;
	public GameContinueSlotUI slotUI;
	public string TargetScene = "";

	public void ChangeScene()
	{
		if(startStatus.continueAvailable)
		{
			slotUI.OpenMenu();
		}
		else
		{
			SceneManager.LoadScene(TargetScene);
		}
		
	}

}
