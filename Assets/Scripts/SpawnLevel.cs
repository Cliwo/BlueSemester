using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
	using UnityEditor;
#endif
public class SpawnLevel : MonoBehaviour {
	private static ParticleMaster inst_particle;
	public List<Pawn> enemies;

	private bool isClear = false;

	public void StartLevel()
	{
		foreach(var p in enemies)
		{
			p.gameObject.SetActive(true);
			inst_particle.GenerateParticle(ParticleMaster.ParticleID.SUMMON_FIRE_DUNGEON, p.transform.position, Quaternion.identity, 1.0f);
			//위는 디버그 코드임. BT_Slime이 MonsterController를 상속받게 되면 고칠 것.
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

	void Start()
	{
		inst_particle = ParticleMaster.getInstance();
	}

	void Update()
	{
		isClear = enemies.All((p) =>
		{
			//이 방식 현재 문제있음. MonsterController에서 Destroy가 불가능하게 하거나
			//Destroy시 무조건 Level의 List를 비워주는 형태로 해야함
			return p.hp < 0;
		});
		bool allNull = true;
		foreach(var enemy in enemies)
		{
			if(enemy != null)
			{
				allNull = false;
			}
		}
		isClear = allNull;
	}

}

#if UNITY_EDITOR
// TODO Editor에서 주기적으로 children에서 Pawn컴포넌트를 가진애들을 enemies에 추가하는 코드 만들기
	[CustomEditor(typeof(SpawnLevel))]
	[CanEditMultipleObjects]
	class SpawnLevelCustomEditor : Editor
	{
		SerializedProperty r_enemies;

		void OnEnable()
		{
			r_enemies = serializedObject.FindProperty("enimies");
		}
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			DrawDefaultInspector();
			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("Show All"))
			{
				SpawnLevel script = (SpawnLevel) target;
				foreach(var e in script.enemies)
				{
					e.gameObject.SetActive(true);
				}
			}
			if(GUILayout.Button("Hide All"))
			{
				SpawnLevel script = (SpawnLevel) target;
				foreach(var e in script.enemies)
				{
					e.gameObject.SetActive(false);
				}
			}
			EditorGUILayout.EndHorizontal();
		}	
	}

#endif
