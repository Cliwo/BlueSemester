using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavCharacterController : MonoBehaviour {

    public Transform destination;
    NavMeshAgent s_navMeshAgent;
    
	// Use this for initialization
	void Start () {
        s_navMeshAgent = GetComponent<NavMeshAgent>();
        s_navMeshAgent.destination = destination.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
