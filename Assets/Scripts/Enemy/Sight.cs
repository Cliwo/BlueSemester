using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    private SphereCollider sight;
    public bool inSight;

    private void Start()
    {
        sight = GetComponent<SphereCollider>();
        inSight = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inSight = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inSight = false;
        }
    }
}