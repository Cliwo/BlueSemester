using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager instance;

    public GameObject character;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    // Use this for initialization
    void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnScroll(float delta)
    {
        Vector3 dir = character.transform.position - transform.position;
        transform.Translate(dir * delta);
    }

    public void OnDrag(float delta)
    {
        
    }

}
