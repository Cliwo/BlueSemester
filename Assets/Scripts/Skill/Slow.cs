using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : ICrowdControlSkill
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
        Debug.Log("Slow");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
    }

    public void Damage()
    {
        Debug.Log("Slow Damaged");
    }
}