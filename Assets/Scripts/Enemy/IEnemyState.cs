using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Enter(Enemy enemy);

    IEnumerator Execute();

    void Exit();
}