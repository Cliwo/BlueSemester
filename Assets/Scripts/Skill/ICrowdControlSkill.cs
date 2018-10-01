using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrowdControlSkill
{
    void Shoot();

    void GetTarget(Rigidbody rigidbody);

    void Damage();
}