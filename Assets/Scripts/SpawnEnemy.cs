using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

	public List<SpawnLevel> levels;
	private int currentLevel;

	public event Action ConsumeEvents = () => { };
	public event Action PersistEvents = ()=> { };
	
	public void AllCleared()
	{
		ConsumeEvents.Invoke();
		PersistEvents.Invoke();

		ConsumeEvents = () => {};
	}

	void Update()
	{
		if(levels[currentLevel].IsClear())
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
