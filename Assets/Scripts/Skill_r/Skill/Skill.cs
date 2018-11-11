using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
public abstract class Skill : MonoBehaviour{

	[SerializeField]
	protected GameObject Particle;
	[SerializeField]
	protected Collider Collider;
	// protected abstract ICrowdControl[] skillEffect { get; }

	/* 이 아래들을 abstract로 안하고, 생성자에서 init후, 기본 생성자가 없으면 무조건 해당 생성자를 override하는 조건을 이용해서 해결할 수 있지 않을까 */
	public abstract float SkillPreDelay { get; }
	public abstract float SkillPostDelay { get; }
	public abstract float SkillCoolDownTime { get; }
	public abstract float SkillActiveDuration { get; }
	public abstract string SoundEffectID{ get; }
	
	//TODO : (IDEA) Collider를 애니메이션 시키면, 조금 더 정확한 피격판정이 가능할 듯.
	static public void GenerateSkill(GameObject skillObj, Vector3 worldPos, [DefaultValue("Quaternion.identity")] Quaternion rot)
	{
		Instantiate<GameObject>(skillObj, worldPos, rot);
	}

	virtual public void OnTriggerEnter(Collider other)
	{
		Pawn pawnScript = other.GetComponent<Pawn>();
		if(pawnScript != null)
		{
			ApplyCC(pawnScript);
		}
	}

	abstract protected void ApplyCC(Pawn target);
}
