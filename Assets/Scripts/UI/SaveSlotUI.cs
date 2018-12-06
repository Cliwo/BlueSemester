using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class SaveSlotUI : MonoBehaviour 
{	
	private CharacterManager inst_character;
	public GameObject panel;
	public CheckDialog checkDialog;
	public Text slot01Text;
	public Text slot02Text;
	public Text slot03Text;

	private string selectedPath;
	public bool IsOpen{ get; private set; }

	void Start()
	{
		inst_character = CharacterManager.getInstance();
	}

	public void OpenMenu()
	{
		panel.SetActive(true);
		IsOpen = true;
		InitAllSlots();
	}

	public void CloseMenu()
	{
		if(checkDialog.IsOpen) //이 방식 맞는걸까..?
		{
			checkDialog.CloseDialog();
			return;
		}

		panel.SetActive(false);
		IsOpen = false;
	}

	public void OnSlotSelected(int index)
	{
		Text text = GetSelectedSlotText(index);
		selectedPath = GetSelectedSlotSavePath(index); // 이 코드 매우 좋지 않음, 원래는 Save의 인자로 같이 넘기는게 맞다.
		if(text.text == SaveLoadConst.EmptySlotTextInSave)
		{
			Save();
		}
		else
		{
			checkDialog.OpenDialog(CheckDialog.SAVE_TEXT , Save);
		}
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
	private void Save()
	{
		GameStateModel model = new GameStateModel();
		model.lastPosition = new GameStateModel.SerializableVector3(inst_character.transform.position);
		model.currentHP = inst_character.hp;

		//TODO 1125 : 아이템에 관한 부분이 빠짐 추가해야함.

		GameStateModel.SerializeAndMakeFile(selectedPath, model);
		if(checkDialog.IsOpen) //여기도 코드 더러움
		{
			checkDialog.CloseDialog();
		}
		CloseMenu();
	}

	private void InitAllSlots()
	{
		GameStateModel.SaveMeta meta01 = CheckMetaFile(SaveLoadConst.FILE01NAME);
		GameStateModel.SaveMeta meta02 = CheckMetaFile(SaveLoadConst.FILE02NAME);
		GameStateModel.SaveMeta meta03 = CheckMetaFile(SaveLoadConst.FILE03NAME);

		slot01Text.text = meta01 == null ? SaveLoadConst.EmptySlotTextInSave : meta01.savedTime + " " + meta01.locationAtSavedTime;
		slot02Text.text = meta02 == null ? SaveLoadConst.EmptySlotTextInSave : meta02.savedTime + " " + meta02.locationAtSavedTime;
		slot03Text.text = meta03 == null ? SaveLoadConst.EmptySlotTextInSave : meta03.savedTime + " " + meta03.locationAtSavedTime;
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
