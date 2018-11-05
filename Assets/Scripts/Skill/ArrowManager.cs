using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    private float speed = 100f;
    private Vector3 dir;
    private CameraManager inst_Camera;
    private Ray ray;
    private RaycastHit hit;

    [SerializeField]
    private GameObject go;

    private LineRenderer lr;
    private Vector3 startPos, endPos;
    private CharacterManager inst_Character;

    public ICrowdControlSkill skill;

    private void Start()
    {
        inst_Character = CharacterManager.getInstance();

        Vector3 groundPos = new Vector3(dir.x, 0, dir.z).normalized;

        lr = GetComponent<LineRenderer>();
        lr.startWidth = .05f;
        lr.endWidth = .05f;

        startPos = inst_Character.transform.position;
        startPos.y = 2.5f;
    }

    private void Update()
    {
        inst_Camera = CameraManager.getInstance();
        Camera myCamera = inst_Camera.GetComponent<Camera>();
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = myCamera.farClipPlane;
        //screenPos.z = inst_Camera.currentDistance; // 방향이 정확하진 않음 나중에 수정할 수 있으면 하자
        screenPos.z = 12;
        Vector3 worldPos = myCamera.ScreenToWorldPoint(screenPos);
        //Debug.Log("world : " + worldPos);
        ray = new Ray(worldPos, Vector3.down);

        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject sphere = Instantiate(go, worldPos, Quaternion.identity);
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, sphere.GetComponent<Transform>().position);

            if (Physics.Raycast(ray, out hit))
            {
                Renderer rend = hit.collider.transform.GetComponent<Renderer>();
                rend.material.color = Color.red;
                Debug.Log("hit : " + hit + " " + hit.collider.transform.position);
            }
        }

        dir = (worldPos - this.transform.position);
        //Debug.Log(dir);
        //transform.Rotate(worldPos * Time.deltaTime * speed);
    }
}