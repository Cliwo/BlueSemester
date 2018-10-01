using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : ICrowdControlSkill
{
    private Rigidbody target;

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
        Debug.Log("Knockback");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
        target = rigidbody;
    }

    public void Damage()
    {
        Debug.Log("Knockback Damaged");
        OnDamage();
    }

    private IEnumerator OnDamage()
    {
        target.AddForce(-100, 0, 0);
        yield return null;
    }
}