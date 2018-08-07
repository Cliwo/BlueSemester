using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        vert *= Time.deltaTime * 10.0f;

        transform.Translate(new Vector3(0, 0, vert));
        transform.Rotate(new Vector3(0, horiz, 0));
    }
}
