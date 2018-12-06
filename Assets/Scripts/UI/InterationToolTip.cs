using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InterationToolTip : MonoBehaviour {
	private static InterationToolTip instance;
	public static InterationToolTip getInstance()
	{
		return instance;
	}
	
	public GameObject panel;
	public Text text;	
	public static readonly string BASIC_INTERACTION_TOOLTIP = "[F] 상호작용";

	void Awake() {
		if(instance == null)
		{
			instance = this;
		}
		if(instance != this)
		{
			Destroy(this);
		}
	}


	public void ShowToolTip(string text)
	{
		panel.SetActive(true);
		this.text.text = text;
	}

	public void HideToolTip()
	{
		panel.SetActive(false);
	}
}
