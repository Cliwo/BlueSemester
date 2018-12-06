using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDungeonDoorTrigger : MonoBehaviour {
	private InputManager inst_input;
	private CameraEffect_Cinema cameraEffect;
	public SpawnEnemy spawn;
	public Animator door;
	public ParticleSystem particle;
	public float freezeTime;

	private bool IsDoorOpen = false;
	private void Start() {
		inst_input = InputManager.getInstance();
		/* 임시 코드 */
		cameraEffect = FindObjectOfType<CameraEffect_Cinema>();
	}
	void OnTriggerExit(Collider other) {
		if(!IsDoorOpen)
		{
			IsDoorOpen = true;
			door.SetTrigger("Appear");
			StartCoroutine("WaitSomeWhile");	
			inst_input.DisableInput();
			spawn.StartStage();
		}
	}

	IEnumerator WaitSomeWhile()
	{
		cameraEffect.StartAnimation();
		yield return new WaitForSeconds(freezeTime);
		cameraEffect.CancelAnimation();
		particle.Play();
		inst_input.AllowInput();
	}
}
