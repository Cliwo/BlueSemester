using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : ICrowdControlSkill
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
        Debug.Log("Stun");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
    }

    public float Damage(float hp)
    {
        Debug.Log("Stun Damaged");
        return hp;
    }
}