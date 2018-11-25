using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Slime : MonsterController
{
    // 어떤 파티클을 쓸지 각각의 몬스터마다 다르니깐 여기서 재정의해주기

    private Sequence root = new Sequence();
    private Sequence seqDeath = new Sequence();
    private Sequence seqChase = new Sequence();
    private Sequence seqDamage = new Sequence();
    private Sequence seqAttack = new Sequence();

    private Selector selMove = new Selector();
    private Selector selChase = new Selector();

    private Patrol patrol = new Patrol();
    private IsDead isDead = new IsDead();
    private Death death = new Death();
    private InSight inSight = new InSight();
    private Chase chase = new Chase();
    private IsDamaged isDamaged = new IsDamaged();
    private InAttackRange inAttackRange = new InAttackRange();
    private Attack attack = new Attack();

    override protected void Start()
    {
        base.Start();
        Init();
        StartCoroutine("BehaviorProcess");
    }

    override protected void Init()
    {
        //Chase
        selChase.AddChild(inSight);
        selChase.AddChild(isDamaged);

        seqDamage.AddChild(chase);
        seqDamage.AddChild(selChase);

        //Attack
        seqAttack.AddChild(attack);
        seqAttack.AddChild(inAttackRange);

        //Patrol

        selMove.AddChild(patrol);
        selMove.AddChild(seqChase);
        selMove.AddChild(seqAttack);

        //Death
        seqDeath.AddChild(death);
        seqDeath.AddChild(isDead);

        root.AddChild(seqDeath);
        root.AddChild(selMove);
    }

    private IEnumerator BehaviorProcess()
    {
        while (!root.Invoke())
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Behavior Process Exit");
    }
}