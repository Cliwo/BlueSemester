using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

	public List<SpawnLevel> levels;
	private bool isStarted = false;
	private int currentLevel = 0;

	public event Action ConsumeEvents = () => { };
	public event Action PersistEvents = ()=> { };
	
	public void AllCleared()
	{
		ConsumeEvents.Invoke();
		PersistEvents.Invoke();

		ConsumeEvents = () => {};
	}
	public void StartStage()
	{
		isStarted = true;
		currentLevel = 0;
		levels[currentLevel].StartLevel();
	}

	// void Start()
	// {
	// 	//Debug
	// 	StartStage();
	// }

	void Update()
	{
		if(levels[currentLevel].IsClear() && isStarted)
		{
			if(currentLevel == levels.Count -1 )
			{
				AllCleared();
			}
			else
			{
				levels[++currentLevel].StartLevel();
			}
		}
	}	
	
}
