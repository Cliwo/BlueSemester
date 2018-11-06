using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWater : ICrowdControlSkill
{
    private float attack = 20; // 공격력
    private float range = 0.15f; // 적용 범위

    public void Shoot(SphereCollider range)
    {
        range.radius = this.range;
        Debug.Log("FireWater");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
    }

    public float Damage(float hp)
    {
        Debug.Log("Damage Method - " + hp);
        hp -= attack;
        Debug.Log("FireWater Damaged");
        Debug.Log("attack " + attack + "// hp " + hp);

        return hp;
    }
}