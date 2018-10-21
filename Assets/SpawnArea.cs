using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour {

	public int MaximumCount; /* 최대 자원 갯수 */
	public int GeneratingCountAtOnce; /* 한 번에 생성 갯수 */
	public float MinimumDistance; /* 자원끼리 가지는 최소 거리 */
	public float UpdateInterval; /* 업데이트 주기 */
	public List<GameObject> Areas;
	public List<GameObject> Resources;
	public GameObject Resource; //Manager가 생기면 옮길 수도
	public SpawnObjectKind kind; //Manager가 생기면 옮길 수도
	public enum SpawnObjectKind //Manager가 생기면 옮길 수도
	{
		TREE, SHELL, IRON_ORE, SULFUR,
	}

	float timeBucket = float.NegativeInfinity;

	void Update()
	{
		if(Time.time > UpdateInterval + timeBucket)
		{
			timeBucket = Time.time;
			if(Resources.Count == MaximumCount)
			{
				return;
			}
			int count = Random.Range(1, Mathf.Min(GeneratingCountAtOnce + 1, MaximumCount - Resources.Count));
			for(int i = 0 ; i < count ; i++)
			{
				Vector3 newPos;
				if(GetRandomValidPosition(out newPos))
				{
					GameObject instantiated = GameObject.Instantiate(Resource);
					instantiated.gameObject.transform.position = newPos;
					
					Resources.Add(instantiated);
				}
				else
				{
					return;
				}
			}
		}	
	}
	public void RemoveGameObject(GameObject g)
	{
		Areas.Remove(g);
	}

	bool GetRandomValidPosition(out Vector3 pos)
	{
		bool isSpaceEnable = IsGeneratingEnable();
		if(!isSpaceEnable)
		{
			pos = Vector3.zero;
			return isSpaceEnable;
		}

		pos = new Vector3();//
		return isSpaceEnable;
	}

	bool IsGeneratingEnable()
	{
		return true;//
	}
	
	
}
