using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : ICrowdControlSkill
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Shoot(SphereCollider range)
    {
        Debug.Log("Blind");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
    }

    public float Damage(float hp)
    {
        Debug.Log("Blind Damaged");
        return hp;
    }
}