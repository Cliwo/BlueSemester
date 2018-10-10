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

    public void Shoot(float range)
    {
        Debug.Log("Snare");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
    }

    public float Damage(float hp)
    {
        Debug.Log("Snare Damaged");
        return hp;
    }
}