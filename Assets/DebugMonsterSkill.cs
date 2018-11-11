using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMonsterSkill : MonoBehaviour {
	public Transform target;
	public GameObject skill;
	public float waitFor;
	// Use this for initialization
	void Start () {
		StartCoroutine(UseSkill());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator UseSkill()
	{
		yield return new WaitForSeconds(waitFor);
		Skill.GenerateSkill(skill, target.position, Quaternion.identity);
	}
}
