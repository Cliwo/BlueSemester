using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour {

	public string TargetScene = "";
	public void ChangeScene()
	{
		SceneManager.LoadScene(TargetScene);
	}
}
