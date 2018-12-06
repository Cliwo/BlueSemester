using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class GameStartButton : MonoBehaviour {
	[HideInInspector]
	public bool continueAvailable;
	void Awake()
	{
		bool file01 = File.Exists(GameStateModel.GetSaveMetaLocation(SaveLoadConst.FILE01NAME));
		bool file02 = File.Exists(GameStateModel.GetSaveMetaLocation(SaveLoadConst.FILE02NAME));
		bool file03 = File.Exists(GameStateModel.GetSaveMetaLocation(SaveLoadConst.FILE03NAME));

		continueAvailable = file01 || file02 || file03;
		if(continueAvailable) 
		{
			ContinueAvailable();
		}
		else
		{
			BrandNewStart();
		}
	}

	void ContinueAvailable()
	{
		//ui 자체를 바꾸는 코드 필요 (플레이 -> 이어하기 새로하기)
	}

	void BrandNewStart()
	{

	}

}
