using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    public GameObject character;
    public float minDistance;
    public float maxDistance;
    public float currentDistance;

    public static CameraManager getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    // Use this for initialization
    private void Start()
    {
        InputManager inst_Input = InputManager.getInstance();
        inst_Input.mouseWheel += OnScroll;
        inst_Input.mouseRightDragging += OnDragging;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnScroll(float delta)
    {
        Vector3 dis = character.transform.position - transform.position;
        float currentSqrMag = dis.sqrMagnitude;
        if (currentSqrMag < minDistance * minDistance && delta > 0 || currentSqrMag > maxDistance * maxDistance && delta < 0)
        {
            return;
        }
        if ((transform.position + dis.normalized * delta).sqrMagnitude < minDistance * minDistance)
        {
            transform.position = character.transform.position + -dis.normalized * minDistance;
        }
        else if ((transform.position + dis.normalized * delta).sqrMagnitude > maxDistance * maxDistance)
        {
            transform.position = character.transform.position + -dis.normalized * maxDistance;
        }
        else
        {
            transform.Translate(dis.normalized * delta, Space.World);
        }
        currentDistance = currentSqrMag;
    }

    private void OnDragging(Vector3 delta)
    {
        Debug.Log("MouseDragging");
        float y_axis_delta = delta.x;
        transform.RotateAround(character.transform.position, Vector3.up, y_axis_delta);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(character.transform.position, transform.position);

        Vector3 discCenter = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
        float radius = Vector3.Magnitude(discCenter - transform.position);

        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireDisc(discCenter, Vector3.up, radius);
    }
}