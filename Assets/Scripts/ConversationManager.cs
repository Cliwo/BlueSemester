using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
public class ConversationManager : MonoBehaviour {

	private static ConversationManager instace;
	public static ConversationManager getInstance()
	{
		return instace;
	}

	private LocalizationManager inst_Local;

	

	[HideInInspector]
	public bool isConversationBoxOpen = false;

	[SerializeField]
	private GameObject conversationUI;
	[SerializeField]
	private Text conversationTextBox;
	private Action endAction;
	private Action cancelAction;
	private string currentTextKey ;

	private int scriptCursor = 0;
	void Awake() {
		if(instace == null)
		{
			instace = this;
		}
		if(instace != this)
		{
			DestroyImmediate(this);
		}
	}
	void Start() {
		inst_Local = LocalizationManager.getInstance();	
	}
	public void StartConversation(string textKey)
	{
		scriptCursor = -1;
		currentTextKey = textKey;
		NextConversation();
		ShowConversationBox();
	}

	public void StartConversation(string textKey, Action endAction, Action cancelAction)
	{
		this.endAction = endAction;
		this.cancelAction = cancelAction;
		StartConversation(textKey);
	}

	public void NextConversation()
	{
		scriptCursor++;
		string text = inst_Local.GetText(GetIndexedKey(currentTextKey));
		if(IsValid(text))
		{
			conversationTextBox.text = text;
		}
		else
		{
			//대화가 모두 끝난 경우 
			EndConversation();
		}
	}
	
	public void EndConversation()
	{
		endAction();
		HideConversationBox();
	}

	public void CancelConversation()
	{
		cancelAction();
		HideConversationBox();
	}
	private void ShowConversationBox()
	{
		isConversationBoxOpen = true;
		conversationUI.SetActive(true);
	}

	private void HideConversationBox()
	{
		cancelAction = null;
		endAction = null;
		currentTextKey = null;
		isConversationBoxOpen = false;
		conversationUI.SetActive(false);
	}

	private bool IsValid(string str)
	{
		return str != null && str != "";
	}
	private string GetIndexedKey(string str)
	{
		return string.Format("{0}{1:D2}", str, scriptCursor);
	}
}
