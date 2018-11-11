using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill {

	ICrowdControl[] skillEffect;
	public abstract Collider GetSkillRegion{ get; }
	public abstract GameObject Particle{ get; }
	public abstract string SoundEffectID{ get; }
	public void GenerateSkill(Vector3 worldPos, Vector3 dir)
	{
		//1. 파티클 
		//2. sound
		//3. 
	}

	//충돌 체크는 어떻게? -> 차라리 스킬 형태를 정의하는 prefab을 만들어서 instantiate 해도 될 듯. sound, particle, region 처리 다하는
	//SkillController 가 

	public void ApplyCC(Pawn target)
	{
		for(int i = 0 ; i < skillEffect.Length; i++)
		{
			target.states.Add(skillEffect[i]);
		}
	}


}
