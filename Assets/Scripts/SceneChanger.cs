using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
public class SceneChanger : MonoBehaviour {
	
	public GameStartButton startStatus;
	public GameContinueSlotUI slotUI;
	public string TargetScene = "";

	public void LoadGame()
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

	public void NewGame()
	{
		SceneManager.LoadScene(TargetScene);
	}

}
