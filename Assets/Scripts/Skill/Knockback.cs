using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : ICrowdControlSkill
{
    private float attack = 5; // 공격력
    private float range = 2; // 적용 범위
    private float value = 10; //밀려나는 거리
    private float activeTime = 2; // 적용되는 시간

    private Rigidbody target;

    public void Shoot(float range)
    {
        range = this.range;
        Debug.Log("Knockback");
    }

    public void GetTarget(Rigidbody rigidbody)
    {
        target = rigidbody;
    }

    public float Damage(float hp)
    {
        Debug.Log("Before hp : " + hp);
        OnDamage();
        hp -= attack;
        Debug.Log("Knockback Damaged // hp : " + hp);

        return hp;
    }

    private IEnumerator OnDamage()
    {
        target.AddForce(-100, 0, 0);
        yield return null;
    }
}