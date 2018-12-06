using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
public class CheckDialog : MonoBehaviour {
	public static readonly string SAVE_TEXT = "Data will be overwritten. Proceed?";
	public static readonly string LOAD_TEXT = "All unsaved progress will be discard. Proceed?";
	public static readonly string ENTER_DUNGEON = "Will you go to Dungeon of Fire";
	public GameObject panel;
	public Text text;
	public bool IsOpen{ get; private set; }
	
	private Action AcceptCallback;
	private Action DenyCallback;

	public void OpenDialog(string text, Action onAccept, Action onDeny = null) //이 함수는 스태틱으로 변경해도 됨
	{
		AcceptCallback = onAccept;
		DenyCallback = onDeny ?? CloseDialog;

		this.text.text = text;
		panel.SetActive(true);
		IsOpen = true;
	}

	public void OnAccept()
	{
		if(AcceptCallback != null)
			AcceptCallback();

		CloseDialog();	
	}
	public void OnDeny()
	{
		if(DenyCallback != null)
			DenyCallback();
		
		CloseDialog();
	}

	public void CloseDialog()
	{
		panel.SetActive(false);
		IsOpen = false;
	}

}
