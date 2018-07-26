using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager instance;
    public GameObject character;
    public float minDistance;
    public float maxDistance;

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
		OnScroll(Input.mouseScrollDelta.y);
        Debug.Log(Input.mouseScrollDelta);
	}

    public void OnScroll(float delta)
    {
        Vector3 dis = character.transform.position - transform.position;
        float currentSqrMag = dis.sqrMagnitude;
        if(currentSqrMag < minDistance*minDistance && delta > 0 || currentSqrMag > maxDistance*maxDistance && delta < 0)
        {
            return;
        }
        if((transform.position + dis.normalized * delta).sqrMagnitude < minDistance*minDistance)
        {
            transform.position = character.transform.position + -dis.normalized * minDistance;
        }
        else if((transform.position + dis.normalized * delta).sqrMagnitude > maxDistance*maxDistance)
        {
            transform.position = character.transform.position + -dis.normalized * maxDistance;
        }
        else 
        {
            transform.Translate(dis.normalized * delta);
        }
    }

    public void OnDrag(float delta)
    {
        
    }

}
