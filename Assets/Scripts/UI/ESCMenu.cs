using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCMenu : MonoBehaviour {
	private static InputManager inst_input;
	public GameObject panel;
	public SaveSlotUI saveSlotUI;
	public LoadSlotUI loadSlotUI;
	public bool IsOpen{ get; private set; }
	void Start()
	{
		inst_input = InputManager.getInstance();
		inst_input.OnEscMenu += ()=> 
		{
			if(saveSlotUI.IsOpen)
			{
				saveSlotUI.CloseMenu();
				return;
			}
			if(loadSlotUI.IsOpen)
			{
				loadSlotUI.CloseMenu();
				return;
			}
			if(!IsOpen)
			{
				OpenMenu();
			} 
			else
			{
				CloseMenu();
			}
		};
		IsOpen = false;
	}
	public void OpenMenu()
	{
		panel.SetActive(true);
		IsOpen = true;
	}

	public void CloseMenu()
	{
		panel.SetActive(false);
		IsOpen = false;
	}

	public void OnTrySave()
	{
		saveSlotUI.OpenMenu();
	}
	public void OnTryLoad()
	{
		loadSlotUI.OpenMenu();
	}
	public void OnConfigureSettings()
	{

	}

	public void GoBackToGame()
	{
		CloseMenu();
	}

}
