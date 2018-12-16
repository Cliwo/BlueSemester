using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyTrigger : MonoBehaviour {
	
	public string mainSceneName;
	private BootstrapManager inst_boot;

	void Start()
	{
		inst_boot = BootstrapManager.getInstance();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			gameObject.SetActive(false);
			inst_boot.model.FireDungeonCleared = true; //던전 깼음을 표시

			//inst_boot.LoadScene(mainSceneName);
		}
	}


}
