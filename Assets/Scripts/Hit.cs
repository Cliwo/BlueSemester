using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    private bool isHit;

    private void Start()
    {
        isHit = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Hit!!!!!!!!!!!!!!!");
            isHit = true;
        }
    }
}