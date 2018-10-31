using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpawnArea : MonoBehaviour {

	//TODO : 10월 30일 modeling load하는거 비동기적으로 하는 코드 만들어야 할 수 도 있음.
	//COMMENT : 성능상의 향상을 원하면 큰 SpawnArea를 정의하지 않고, 큰 Area를 작게 쪼개서 만드는게 알고리즘상 유리함
	//          n제곱의 알고리즘에서 n을 크게만드는 것 보다 작은 n의 제곱을 더하는게 유리하기 때문
	public float maximumScale; /* 자원 최대 크기 */
	public float minimumScale; /* 자원 최소 크기 */
	public int MaximumCount; /* 최대 자원 갯수 */
	public int GeneratingCountAtOnce; /* 한 번에 생성 갯수 */
	public float UpdateInterval; /* 업데이트 주기 */

	private List<GameObject> InstantiatedResources;
	private List<Collider> OccupiedArea;
	public GameObject Resource; //Manager가 생기면 옮길 수도
	public SpawnObjectKind kind; //Manager가 생기면 옮길 수도
	public enum SpawnObjectKind //Manager가 생기면 옮길 수도
	{
		TREE, SHELL, IRON_ORE, SULFUR,
	}

	float minWorldX;
	float maxWorldX;
	float minWorldZ;
	float maxWorldZ;
	float timeBucket = float.MinValue;
	const float raycastStartYPos = 100f;
	void Start() 
	{
		InstantiatedResources = new List<GameObject>();
		OccupiedArea = new List<Collider>();

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
			if(InstantiatedResources.Count == MaximumCount)
			{
				return;
			}
			int count = Random.Range(1, Mathf.Min(GeneratingCountAtOnce + 1, MaximumCount - InstantiatedResources.Count));
			InstantiateRandomResources(count);
		}	
	}
	public void RemoveGameObject(GameObject g)
	{
		OccupiedArea.Remove(g.GetComponent<Collider>());
		InstantiatedResources.Remove(g);
		DestroyImmediate(g);
	}

	private void InstantiateRandomResources(int count)
	{
		for(int i = 0 ; i < count ; i++)
		{	
			Collider col = Resource.GetComponent<Collider>();
			Vector3 newPos;
			float desiredSize = Random.Range(minimumScale, maximumScale);
			if(GetRandomValidPosition(out newPos, col.bounds.size * desiredSize))
			{
				GameObject instantiated = GameObject.Instantiate(Resource);
				instantiated.gameObject.transform.position = newPos;
				instantiated.gameObject.transform.localScale *= desiredSize;
				InstantiatedResources.Add(instantiated);

				instantiated.transform.parent = transform;
				
				Collider instantiatedCol = instantiated.GetComponent<Collider>();
				instantiatedCol.enabled = false; /* 18.10.31 currently this is for unity bug */
				instantiatedCol.enabled = true;
				OccupiedArea.Add(instantiatedCol);
			}
			else
			{	
				//생성을 해야하는데 적당한 빈공간이 없는 경우 
				return;
			}
		}
	}
	private bool GetRandomValidPosition(out Vector3 pos, Vector3 colSize, int tryCount = 1000)
	{
		Vector3 copy;
		int index = 0; 
		do{
			float newX = Random.Range(minWorldX, maxWorldX);
			float newZ = Random.Range(minWorldZ, maxWorldZ);
			pos = new Vector3(newX, raycastStartYPos, newZ);
			RaycastHit hit;
			if(Physics.Raycast(pos, Vector3.down, out hit))
			{
				pos.y = hit.point.y;
			}
			copy = pos;
			index ++;

			if(InstantiatedResources.Count == 0)
			{
				break;
			}
		}
		while(OccupiedArea.Any( (g) => new Bounds(copy, colSize).Intersects(g.bounds)) && index < tryCount);

		if(index == tryCount)
		{
			pos = Vector3.zero;
			return false;
		}
		
		pos.y += GetMeshHalfHeight(Resource);
		return true;
	}

	private float GetMeshHalfHeight(GameObject g)
	{
		return g.GetComponent<MeshFilter>().mesh.bounds.extents.y * g.transform.localScale.y;
	}
}
