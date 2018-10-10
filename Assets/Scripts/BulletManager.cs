using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private float speed = 0.1f;
    private Vector3 dir;
    private CameraManager inst_Camera;
    private Camera myCamera;
    public SphereCollider collider;

    // Use this for initialization
    private void Start()
    {
        inst_Camera = CameraManager.getInstance();
        myCamera = inst_Camera.GetComponent<Camera>();
        collider = GetComponent<SphereCollider>();
        Vector3 screenPos = Input.mousePosition;
        //screenPos.z = myCamera.farClipPlane;
        screenPos.z = inst_Camera.currentDistance; // 방향이 정확하진 않음 나중에 수정할 수 있으면 하자
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