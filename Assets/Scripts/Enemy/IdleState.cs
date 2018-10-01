using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public IEnumerator Execute()
    {
        yield return null;
    }

    public void Exit()
    {
    }

    private void Idle()
    {
    }
}