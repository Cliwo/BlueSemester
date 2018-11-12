using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
public abstract class Skill : MonoBehaviour{
	protected const int PLAYER_LAYER = 9;
	protected const int ENEMY_LAYER = 10;

	protected abstract int targetLayer { get; }
	[SerializeField]
	protected GameObject Particle;
	[SerializeField]
	protected Collider Collider;
	// protected abstract ICrowdControl[] skillEffect { get; }
	private float GeneratedTime;

	/* 이 아래들을 abstract로 안하고, 생성자에서 init후, 기본 생성자가 없으면 무조건 해당 생성자를 override하는 조건을 이용해서 해결할 수 있지 않을까 */
	public abstract float SkillLastUsedTimePerKind { get; protected set; }
	public abstract float SkillPreDelay { get; }
	public abstract float SkillPostDelay { get; }
	public abstract float SkillCoolDownTime { get; }
	public abstract float SkillActiveDuration { get; }
	public abstract string SoundEffectID{ get; }
	
	//TODO : (IDEA) Collider를 애니메이션 시키면, 조금 더 정확한 피격판정이 가능할 듯.
	static public bool GenerateSkill(GameObject skillObj, Vector3 worldPos, [DefaultValue("Quaternion.identity")] Quaternion rot)
	{
		Skill static_check = skillObj.GetComponent<Skill>();
		if(Time.time < static_check.SkillLastUsedTimePerKind + static_check.SkillCoolDownTime)
		{
			Debug.Log("CoolTime");
			return false;
		}

		GameObject instantiatedSkill = Instantiate<GameObject>(skillObj, worldPos, rot);
		Skill s = instantiatedSkill.GetComponent<Skill>();
		s.GeneratedTime = Time.time;
		s.SkillLastUsedTimePerKind = Time.time;
		return true;
	}

	virtual protected void OnTriggerEnter(Collider other)
	{
		//TODO : Layer를 설정해서 몬스터의 스킬은 몬스터들끼리는 맞지 않고, 캐릭터 본인의 스킬은 본인이 안맞도록해야함
		Pawn pawnScript = other.GetComponent<Pawn>();
		if(pawnScript != null && pawnScript.gameObject.layer == targetLayer)
		{
			ApplyCC(pawnScript);
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
}
