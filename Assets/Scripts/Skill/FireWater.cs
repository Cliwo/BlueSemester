using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWater : ICrowdControlSkill
{
    private float attack = 20; // 공격력
    private float range = 3; // 적용 범위

    private Rigidbody target;

    public void Shoot(SphereCollider range)
    {
        range.radius = this.range;
        Debug.Log("FireWater");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
        target = rigidbody;
    }

    public float Damage(float hp)
    {
        hp -= attack;
        Debug.Log("FireWater Damaged");
        Debug.Log("attack " + attack + "// hp " + hp);

        return hp;
    }
}