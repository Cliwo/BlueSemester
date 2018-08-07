using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinimapManager : MonoBehaviour
{
    public GameObject target; //for now this is character

    public static MinimapManager instance;

    private Vector3 dir;
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
            target.transform.Translate(dir);
        }
    }

    public void OnMinimapPinEneabled(Vector2 normalizedDirection)
    {
        dir = new Vector3(normalizedDirection.x, 0, normalizedDirection.y);
        shouldMove = true;
    }

   

}
