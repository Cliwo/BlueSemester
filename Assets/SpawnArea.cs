using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpawnArea : MonoBehaviour {

	//TODO : 10월 30일 modeling load하는거 비동기적으로 하는 코드 만들어야 할 수 도 있음.
	//COMMENT : 성능상의 향상을 원하면 큰 SpawnArea를 정의하지 않고, 큰 Area를 작게 쪼개서 만드는게 알고리즘상 유리함
	// n제곱의 알고리즘에서 n을 크게만드는 것 보다 작은 n의 제곱을 더하는게 유리하기 때문

	public int MaximumCount; /* 최대 자원 갯수 */
	public int GeneratingCountAtOnce; /* 한 번에 생성 갯수 */
	public float UpdateInterval; /* 업데이트 주기 */

	[HideInInspector]
	public List<GameObject> Resources;
	public GameObject Resource; //Manager가 생기면 옮길 수도
	public SpawnObjectKind kind; //Manager가 생기면 옮길 수도
	public enum SpawnObjectKind //Manager가 생기면 옮길 수도
	{
		TREE, SHELL, IRON_ORE, SULFUR,
	}

	float timeBucket = float.MinValue;
	float minWorldX;
	float maxWorldX;
	float minWorldZ;
	float maxWorldZ;
	void Start() 
	{
		Collider instantiateAvailableArea = GetComponent<Collider>(); 
		GetComponent<MeshRenderer>().enabled = false;

		minWorldX = instantiateAvailableArea.bounds.min.x;
		minWorldZ = instantiateAvailableArea.bounds.min.z;
		maxWorldX = instantiateAvailableArea.bounds.max.x;
		maxWorldZ = instantiateAvailableArea.bounds.max.z;

		instantiateAvailableArea.enabled = false;
	}
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
				Collider col = Resource.GetComponent<Collider>();
				Vector3 newPos;
				if(GetRandomValidPosition(out newPos, col.bounds.size))
				{
					GameObject instantiated = GameObject.Instantiate(Resource);
					instantiated.gameObject.transform.position = newPos;
					instantiated.transform.parent = transform;
					Resources.Add(instantiated);
				}
				else
				{	
					//생성을 해야하는데 적당한 빈공간이 없는 경우 
					return;
				}
			}
		}	
	}
	public void RemoveGameObject(GameObject g)
	{
		Resources.Remove(g);
		DestroyImmediate(g);
	}

	bool GetRandomValidPosition(out Vector3 pos, Vector3 colSize, int tryCount = 1000)
	{
		Vector3 copy;
		int index = 0; 
		do{
			float newX = Random.Range(minWorldX, maxWorldX);
			float newZ = Random.Range(minWorldZ, maxWorldZ);
			pos = new Vector3(newX, 100f, newZ); //100 보다 float.MaxValue가 낫지 않을까 
			RaycastHit hit;
			if(Physics.Raycast(pos, Vector3.down, out hit))
			{
				pos.y = hit.point.y;
			}
			copy = pos;
			index ++;

			if(Resources.Count == 0)
			{
				break;
			}
		}
		while(Resources.Any( (g) => new Bounds(copy, colSize).Intersects(g.GetComponent<Collider>().bounds) ) && index < tryCount);
		// 개선 가능 : 아에 Resources 의 bounds만 따로 저장하기

		if(index == tryCount)
		{
			pos = Vector3.zero;
			return false;
		}
		
		return true;
	}
}
