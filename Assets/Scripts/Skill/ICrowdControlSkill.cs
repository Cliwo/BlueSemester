using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrowdControlSkill
{
    void Shoot(SphereCollider range);

    void GetTarget(Rigidbody rigidbody);

    float Damage(float hp);
}