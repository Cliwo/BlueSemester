using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour {
    public GameObject target;
    public static MinimapManager instance;
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


}
