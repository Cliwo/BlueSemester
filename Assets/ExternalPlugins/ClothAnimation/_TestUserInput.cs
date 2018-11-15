using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestUserInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public List<Rigidbody> bodies;
	float y;
	// Update is called once per frame
	void Update () {
		float vert = Input.GetAxis("Vertical") *Time.deltaTime;
		float hori = Input.GetAxis("Horizontal") * Time.deltaTime; 

		if(Input.GetKeyDown(KeyCode.Space))
		{
			foreach(var body in bodies)
			{
				body.AddForce(Vector3.up * 10, ForceMode.Impulse);
			}
		}
		transform.Translate(new Vector3(hori, 0, vert));
	}
}
