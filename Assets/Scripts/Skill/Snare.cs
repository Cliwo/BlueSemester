using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snare : ICrowdControlSkill
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Shoot()
    {
        Debug.Log("Snare");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
    }

    public void Damage()
    {
        Debug.Log("Snare Damaged");
    }
}