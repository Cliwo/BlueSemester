using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	/*
	TODO : NavMesh 를 통해서 이동하는 기능을 포함해야함. 후에 추가할 것
	
	 */
	
	private CameraManager inst_Camera;
	private InputManager inst_Input;
	void Start() {
		inst_Camera = CameraManager.getInstance();
		inst_Input = InputManager.getInstance();

		inst_Input.OnTranslate += OnTranslate;
	}
	
	void OnTranslate()
	{
		Debug.Log("Translate");
		float verticalWeight = Input.GetAxis("Vertical");
		float horizontalWeight = Input.GetAxis("Horizontal");

		Vector4 rightUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(1, 0, 0);
		Vector4 forwardUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(0, 0, 1);

		transform.Translate(verticalWeight * forwardUnitVec);
		transform.Translate(horizontalWeight * rightUnitVec);
	}

}
