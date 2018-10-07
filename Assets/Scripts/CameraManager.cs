using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private static CameraManager instance;
    public GameObject character;
    public float minDistance;
    public float maxDistance;

    private Vector3 dragStartRotation;

    public static CameraManager getInstance()
    {
        return instance;
    }
    void Awake()
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
        InputManager inst_Input = InputManager.getInstance();
        inst_Input.mouseWheel += OnScroll;
        inst_Input.mouseRightDragging += OnDragging;
        inst_Input.mouseRightDragStart += OnDragStart;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnScroll(float delta)
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

    void OnDragStart()
    {
        dragStartRotation = transform.rotation.eulerAngles;
    }
    void OnDragging(Vector3 origin, Vector3 delta)
    {
        float y_axis_delta = delta.x / Screen.width;

        float destinationAngle = y_axis_delta*(180.0f) + dragStartRotation.y;
        float angle = destinationAngle - transform.rotation.eulerAngles.y;

        transform.RotateAround(character.transform.position, Vector3.up, angle);
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
