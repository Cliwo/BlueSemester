using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LockDoor : MonoBehaviour {
	
	public GameObject door;
	Animator animator;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		animator = door.GetComponent<Animator>();
	}
	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Enter");
	}
	void OnTriggerExit(Collider other)
	{
		Debug.Log("Exit");
		if(other.tag == "Player")
		{
			animator.SetTrigger("LockDoor");
		}
	}
}
