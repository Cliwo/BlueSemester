using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class CheckDialog : MonoBehaviour {
	public GameObject panel;
	public bool IsOpen{ get; private set; }
	
	private Action AcceptCallback;
	private Action DenyCallback;

	public void OpenDialog(Action onAccept, Action onDeny = null) //이 함수는 스태틱으로 변경해도 됨
	{
		AcceptCallback = onAccept;
		DenyCallback = onDeny ?? CloseDialog;

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
