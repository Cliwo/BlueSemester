using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private float speed = 0.1f;
    private Vector3 dir;
    private CameraManager inst_Camera;
    private Camera myCamera;

    // Use this for initialization
    private void Start()
    {
        inst_Camera = CameraManager.getInstance();
        myCamera = inst_Camera.GetComponent<Camera>();
        Vector3 screenPos = Input.mousePosition;
        //screenPos.z = myCamera.farClipPlane;
        screenPos.z = inst_Camera.currentDistance;
        Vector3 worldPos = myCamera.ScreenToWorldPoint(screenPos);
        dir = (worldPos - this.transform.position);
        Destroy(this.gameObject, 5.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.Translate(dir * speed * Time.deltaTime);
    }
}