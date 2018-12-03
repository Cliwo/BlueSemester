using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
public class SceneChanger : MonoBehaviour {
	private BootstrapManager inst_boot;
	public string TargetScene = "";

	void Awake() 
	{
		inst_boot = BootstrapManager.getInstance();
	}
	public void ChangeScene()
	{
		string fileName = "Save02";
		string path = Application.dataPath + '/' + fileName;
		// SceneManager.LoadScene(TargetScene);
		inst_boot.RebootGameWithData(DebugDummyModelLoad(path), DebugDummyMetaLoad(path));	
	}

	private GameStateModel DebugDummyModelLoad(string path)
	{
		return GameStateModel.Deserialize<GameStateModel>(File.ReadAllBytes(path+".bin"));
	}
	private GameStateModel.SaveMeta DebugDummyMetaLoad(string path)
	{
		return GameStateModel.Deserialize<GameStateModel.SaveMeta>(File.ReadAllBytes(path+".metaBin"));
	}
}
