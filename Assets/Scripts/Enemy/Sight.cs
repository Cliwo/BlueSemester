﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public bool inSight;

    private void Start()
    {
        inSight = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter : " + inSight);
        if (other.gameObject.tag == "Player")
        {
            inSight = true;
            Debug.Log("trigger enter player : " + inSight);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exit : " + inSight);
        if (other.gameObject.tag == "Player")
        {
            inSight = false;
            Debug.Log("trigger exit player : " + inSight);
        }
    }
}