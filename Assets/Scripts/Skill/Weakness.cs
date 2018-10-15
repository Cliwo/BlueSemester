using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakness : ICrowdControlSkill
{
    private float attack = 5; // 공격력
    private float range = 1; // 적용 범위
    private float value = 10; // 약화 수치
    private float activeTime = 5; // 적용되는 시간

    public void Shoot(SphereCollider range)
    {
        range.radius = this.range;
        Debug.Log("Weakness");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
    }

    public float Damage(float hp)
    {
        hp -= attack;
        Debug.Log("Weakness Damaged");
        Debug.Log("attack " + attack + "// hp " + hp);

        return hp;
    }
}