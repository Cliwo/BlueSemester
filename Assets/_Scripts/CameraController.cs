using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	GameObject PlayerCenter;

	Vector3 desPos;

	Vector3 vel;
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		PlayerCenter = GameObject.FindGameObjectWithTag("PCenter");
		desPos = new Vector3(transform.position.x, PlayerCenter.transform.position.y+2, transform.position.z);

		if(Game.Instance.Death == false)
		{
			//transform.position += Vector3.up / 2 * Time.deltaTime;
			transform.position = Vector3.SmoothDamp(transform.position, desPos, ref vel, .25f);
			transform.LookAt(new Vector3(0, PlayerCenter.transform.position.y, 0));
		}
	}
}
