using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaster : MonoBehaviour {

	//기존의 ParticleManager에 손대지 않기 위해 만든 클래스임
	//ParticleManager와 겹친다면, 두 개를 머지할 것.
	private static MinimapManager inst_minimap;
	public List<GameObject> Particles;

	public enum ParticleID
	{
		SUMMON_FIRE_DUNGEON,
		SUMMON_WATER_ELECTICITY_DUNGEON
	}

	public static ParticleMaster getInstance()
	{
		return instance;
	}
	private static ParticleMaster instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		if(instance != this)
		{
			DestroyImmediate(this);
		}
	}
	void Start() 
	{
		inst_minimap = MinimapManager.getInstance();		
	}

	public void GenerateParticle(ParticleID iD, Vector3 worldPos, Quaternion rotation, float duration = -1.0f)
	{
		RaycastHit hit;
		int mask = 1<<inst_minimap.layerMask;
		if(Physics.Raycast(worldPos, Vector3.down, out hit, float.MaxValue, mask))
		{
			Vector3 pos = hit.point;
			pos.y +=0.1f; //z-fighting
			GameObject generate = Instantiate(Particles[ (int)iD ], pos, rotation);
			if(duration > 0)
			{
				StartCoroutine("KillParticle", new object[2]{duration, generate});
			}
			
		}
	}

	IEnumerator KillParticle(object[] parms) //전달 순서 주의
	{
		float duration = (float)parms[0];
		GameObject particle = (GameObject)parms[1];
		yield return new WaitForSeconds(duration);
		Destroy(particle);
		yield break;
	}

}
