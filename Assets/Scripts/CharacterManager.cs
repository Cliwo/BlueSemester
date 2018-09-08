using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	/*
	TODO : NavMesh 를 통해서 이동하는 기능을 포함해야함. 후에 추가할 것
	
	 */
	private Vector3 moveDirection = Vector3.zero;
	private bool isJumped = false;
	private const float gravity = -0.16f;
	private CameraManager inst_Camera;
	private InputManager inst_Input;

	private CharacterController s_characterController;
	void Start() 
	{
		inst_Camera = CameraManager.getInstance();
		inst_Input = InputManager.getInstance();

		s_characterController = GetComponent<CharacterController>();

		inst_Input.OnTranslate += OnTranslate;
		inst_Input.OnJump += OnJump;
	}
	void Update() 
	{
		if(!s_characterController.isGrounded)
		{
			moveDirection += Vector3.up * gravity;
		}
		else if (s_characterController.isGrounded && moveDirection.y < 0)
		{
			moveDirection.y = 0f;
			isJumped = false;
		}
		s_characterController.Move(moveDirection);
		
	}
	
	void OnTranslate()
	{
		float verticalWeight = Input.GetAxis("Vertical");
		float horizontalWeight = Input.GetAxis("Horizontal");

		Vector3 rightUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(1, 0, 0);
		Vector3 forwardUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(0, 0, 1);

		moveDirection = verticalWeight * forwardUnitVec + horizontalWeight * rightUnitVec;
		//moveDirection += horizontalWeight * rightUnitVec;
	}

	void OnJump()
	{
		if(!isJumped)
		{
			moveDirection += Vector3.up * 4;	
			isJumped = true;
		}
		Debug.Log("OnJump");
	}

}
