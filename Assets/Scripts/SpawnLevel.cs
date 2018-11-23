using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpawnLevel : MonoBehaviour {

	public List<Pawn> enemies;

	private bool isClear = false;

	public void StartLevel()
	{
		foreach(var p in enemies)
		{
			p.gameObject.SetActive(true);
			//Particle 작동시키기 (소환)
		}
	}
	public bool IsClear()
	{
		return isClear;
	}

	void Awake()
	{
		foreach(var p in enemies)
		{
			p.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		isClear = enemies.All((p) =>
		{
			return p.hp < 0;
		});
	}
}
