using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour {

	private static BootstrapManager instance;
	public static BootstrapManager getInstance()
	{
		return instance;
	}

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
	public void RebootGameWithData(GameStateModel model, GameStateModel.SaveMeta meta)
	{
		ChangeScene(meta.locationAtSavedTime , model);
	}
	private void ChangeScene(string scene, GameStateModel model)
	{
		//Unload를 해도 manager들은 죽으면 안 됨 
		SceneManager.sceneLoaded += (_, __) => 
		{
			MatData(model);
		};
		SceneManager.LoadScene(scene);
	}
	public void MatData(GameStateModel model)
	{
		//각 Manager들에서 데이터를 넣을 부분을 뽑아와야함.
		CharacterManager inst_character = CharacterManager.getInstance();
		inst_character.gameObject.transform.position = model.lastPosition.ToVector3();
		
	}

}
