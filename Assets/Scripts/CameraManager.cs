using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager instance;
    public GameObject character;
    public float minDistance;
    public float maxDistance;
    public float accelationOnDrag;

    Vector2 mouseOrigin;
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
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            mouseOrigin = Input.mousePosition;
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            float normalizedDelta = Input.mousePosition.x - mouseOrigin.x != 0 ? (Input.mousePosition.x - mouseOrigin.x) / Mathf.Abs(Input.mousePosition.x - mouseOrigin.x) : 0;
            OnDrag(normalizedDelta * accelationOnDrag);
        }
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
            transform.Translate(dis.normalized * delta, Space.World);
        }
    }

    public void OnDrag(float delta)
    {
        transform.RotateAround(character.transform.position, Vector3.up, delta);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(character.transform.position, transform.position);
        
        Vector3 discCenter = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
        float radius = Vector3.Magnitude(discCenter - transform.position);
        
        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireDisc(discCenter, Vector3.up, radius);

    }
}
