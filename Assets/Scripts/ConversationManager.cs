using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ConversationManager : MonoBehaviour {

	private static ConversationManager instace;
	public static ConversationManager getInstance()
	{
		return instace;
	}
	public GameObject conversationUI;
	[HideInInspector]
	public bool isConversationBoxOpen = false;

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

	public void ShowConversationBox()
	{
		isConversationBoxOpen = true;
		conversationUI.SetActive(true);
	}

	public void HideConversationBox()
	{
		isConversationBoxOpen = false;
		conversationUI.SetActive(false);
	}

	public void NextConversation()
	{

	}

	
}
