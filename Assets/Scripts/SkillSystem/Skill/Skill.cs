﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
public abstract class Skill : MonoBehaviour{
	protected const int PLAYER_LAYER = 9; //TODO : 리팩토링 필요 
	protected const int ENEMY_LAYER = 10;
	[SerializeField]
	protected GameObject Particle;
	[SerializeField]
	protected Collider Collider;
	private float GeneratedTime;
	private static CharacterManager inst_character;
	private static MinimapManager inst_minimap;

	/* 이 아래들을 abstract로 안하고, 생성자에서 init후, 기본 생성자가 없으면 무조건 해당 생성자를 override하는 조건을 이용해서 해결할 수 있지 않을까 */
	public abstract float SkillLastUsedTimePerKind { get; protected set; }
	public abstract float SkillPreDelay { get; }
	public abstract float SkillPostDelay { get; }
	public abstract float SkillCoolDownTime { get; }
	public abstract float SkillActiveDuration { get; }
	public abstract float Damage { get; }
	public abstract string SoundEffectID{ get; }
	protected abstract int targetLayer { get; }
	protected abstract bool isProjectile { get; }
	
	//TODO : (IDEA) Collider를 애니메이션 시키면, 조금 더 정확한 피격판정이 가능할 듯.
	static public bool GenerateSkill(GameObject skillObj, Vector3 worldPos)
	{
		Skill static_check = skillObj.GetComponent<Skill>();
		if(Time.time < static_check.SkillLastUsedTimePerKind + static_check.SkillCoolDownTime)
		{
			Debug.Log("CoolTime");
			return false;
		}
		bool isProjectile = static_check.isProjectile;
		GameObject instantiatedSkill = Instantiate<GameObject>(skillObj, worldPos, 
				isProjectile ? Quaternion.LookRotation(CalcMouseDirectionInWorldSpace()) : Quaternion.identity);

		Skill s = instantiatedSkill.GetComponent<Skill>();
		s.GeneratedTime = Time.time;
		s.SkillLastUsedTimePerKind = Time.time;
		return true;
	}

	virtual protected void OnTriggerEnter(Collider other)
	{
		Pawn pawnScript = other.GetComponent<Pawn>();
		if(pawnScript != null && pawnScript.gameObject.layer == targetLayer)
		{
			ApplyCC(pawnScript);
			ApplyDamage(pawnScript);
			Destroy(gameObject); //이거 분리해야할듯, Collider만 끄는 방식 / 모든 이펙트와 함께 사라지는 방식으로 나뉘어야함. 
		}
	}

	virtual protected void Update()
	{
		if(Time.time > GeneratedTime + SkillActiveDuration)
		{
			Destroy(gameObject);
		}
	}

	abstract protected void ApplyCC(Pawn target);
	
	private void ApplyDamage(Pawn target)
	{
		target.hp -= Damage;
	}

	static private Vector3 CalcMouseDirectionInWorldSpace()
	{
		if(inst_character == null)
		{
			inst_character = CharacterManager.getInstance();
		}
		if(inst_minimap == null)
		{
			inst_minimap = MinimapManager.getInstance();
		}
		Vector3 clippedPos = Input.mousePosition;
		clippedPos.x = Mathf.Clamp(clippedPos.x , 0, Screen.width);
		clippedPos.y = Mathf.Clamp(clippedPos.y , 0, Screen.height);

		Ray ray = Camera.main.ScreenPointToRay(clippedPos);
		
		RaycastHit hit;
		int mask = 1<<inst_minimap.layerMask;
		if(Physics.Raycast(ray, out hit , float.MaxValue , mask)) 
		{
			return (hit.point - inst_character.transform.position).normalized;
		}

		//이 아래줄은 실행되면 안 됨. 스킬은 어떤 경우에서도 나가야하므로.  
		return Vector3.zero;
	}

}
