﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class GameContinueSlotUI : MonoBehaviour {
	private BootstrapManager inst_boot;
	public GameObject panel;
	public Text slot01Text;
	public Text slot02Text;
	public Text slot03Text;
	private string selectedPath;

	void Start()
	{
		inst_boot = BootstrapManager.getInstance();
	}
	public void OpenMenu()
	{
		panel.SetActive(true);
		InitAllSlots();
	}
	public void CloseMenu()
	{
		panel.SetActive(false);
	}

	public void OnSlotSelected(int index)
	{
		Text text = GetSelectedSlotText(index);
		if(text.text == SaveLoadConst.EmptySlotTextInLoad)
		{
			return;
		}
		selectedPath = GetSelectedSlotSavePath(index);
		Load();
	}
	private void Load()
	{
		byte[] save = File.ReadAllBytes(GameStateModel.GetSaveLocation(selectedPath));
		byte[] meta = File.ReadAllBytes(GameStateModel.GetSaveMetaLocation(selectedPath));
		
		inst_boot.RebootGameWithData(GameStateModel.Deserialize<GameStateModel>(save), 
			GameStateModel.Deserialize<GameStateModel.SaveMeta>(meta));		
	}
	private Text GetSelectedSlotText(int index)
	{
		switch(index)
		{
			case 0:
			return slot01Text;
			case 1:
			return slot02Text;
			case 2:
			return slot03Text;
		}
		return null;
	} 
	
	private string GetSelectedSlotSavePath(int index)
	{
		switch(index)
		{
			case 0:
			return SaveLoadConst.FILE01NAME;
			case 1:
			return SaveLoadConst.FILE02NAME;
			case 2:
			return SaveLoadConst.FILE03NAME;
		}
		return null;
	}
	private void InitAllSlots()
	{
		GameStateModel.SaveMeta meta01 = CheckMetaFile(SaveLoadConst.FILE01NAME);
		GameStateModel.SaveMeta meta02 = CheckMetaFile(SaveLoadConst.FILE02NAME);
		GameStateModel.SaveMeta meta03 = CheckMetaFile(SaveLoadConst.FILE03NAME);

		slot01Text.text = meta01 == null ? SaveLoadConst.EmptySlotTextInLoad : meta01.savedTime + " " + meta01.locationAtSavedTime;
		slot02Text.text = meta02 == null ? SaveLoadConst.EmptySlotTextInLoad : meta02.savedTime + " " + meta02.locationAtSavedTime;
		slot03Text.text = meta03 == null ? SaveLoadConst.EmptySlotTextInLoad : meta03.savedTime + " " + meta03.locationAtSavedTime;
	}
	private GameStateModel.SaveMeta CheckMetaFile(string name)
	{
		string path = GameStateModel.GetSaveMetaLocation(name);
		byte[] metaData = null;
		try
		{
			metaData = File.ReadAllBytes(path);
			return GameStateModel.Deserialize<GameStateModel.SaveMeta>(metaData);
		}
		catch
		{	
			return null;
		}
	}
}
