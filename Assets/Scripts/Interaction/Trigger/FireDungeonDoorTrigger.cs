using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDungeonDoorTrigger : MonoBehaviour {
	public Animator door;
	public ParticleSystem particle;
	void OnTriggerEnter(Collider other) {
		door.SetTrigger("Appear");
		StartCoroutine("WaitSomeWhile");	
	}

	IEnumerator WaitSomeWhile()
	{
		yield return new WaitForSeconds(1.7f);
		particle.Play();
	}
}
