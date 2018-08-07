using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private Transform transform;

    [SerializeField]
    private float moveSpeed = 5.0f;


	// Use this for initialization
	void Start ()
    {
        transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = (Vector3.forward * vertical) + (Vector3.right * horizontal);
        transform.Translate(moveDirection.normalized * Time.deltaTime * moveSpeed, Space.Self);
	}
}
