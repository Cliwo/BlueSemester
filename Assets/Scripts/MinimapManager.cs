using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinimapManager : MonoBehaviour
{
    public Camera minimapCamera;
    public GameObject target; //for now this is character

    public static MinimapManager instance;

    private Vector3 destination;
    private bool shouldMove = false;

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

    private void Start()
    {
        minimapCamera = GetComponent<Camera>();   
    }

    private void LateUpdate()
    {
        Vector3 newPos = target.transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90.0f, target.transform.eulerAngles.y, 0.0f);
    }

    private void Update()
    {
        if(shouldMove)
        {
            target.transform.position = Vector3.Lerp(target.transform.position, destination, Time.deltaTime);
        }
    }

    public void OnMinimapPinEneabled(Vector2 normalizedDirection)
    {
        float minimapHalfSize = minimapCamera.orthographicSize;
        normalizedDirection *= 2;

        Vector2 pos = normalizedDirection * minimapHalfSize;
        destination = new Vector3(pos.x, target.transform.position.y, pos.y);
        shouldMove = true;
    }

   

}
