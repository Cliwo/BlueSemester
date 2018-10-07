using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LockDoor : MonoBehaviour {
	
	public GameObject door;
	Animator animator;

	void Awake()
	{
		animator = door.GetComponent<Animator>();
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			animator.SetTrigger("LockDoor");
		}
	}
}
