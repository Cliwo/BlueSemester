using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrowdControlSkill
{
    void Shoot(float range);

    void GetTarget(Rigidbody rigidbody);

    float Damage(float hp);
}