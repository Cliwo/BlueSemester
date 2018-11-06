using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : ICrowdControlSkill
{
    private float attack = 5; // 공격력
    private float range = 0.15f; // 적용 범위
    private float value = 10; //밀려나는 거리
    private float activeTime = 2; // 적용되는 시간

    private Rigidbody target;

    public void Shoot(SphereCollider range)
    {
        range.radius = this.range;
        Debug.Log("Knockback");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
        target = rigidbody;
    }

    public float Damage(float hp)
    {
        OnDamage();
        hp -= attack;
        Debug.Log("Knockback Damaged");
        Debug.Log("attack " + attack + "// hp " + hp);

        return hp;
    }

    private IEnumerator OnDamage()
    {
        target.AddForce(-100, 0, 0);
        yield return null;
    }
}