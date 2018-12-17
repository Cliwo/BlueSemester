using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Slime : MonsterController
{
    private Sequence root;

    private Sequence seqDeath;
    private Sequence seqChase;
    private Sequence seqAttack;

    private Selector selMove;
    private Selector selChase;

    private Patrol patrol;
    private IsDead isDead;
    private Death death;
    private InSight inSight;
    private Chase chase;
    private IsDamaged isDamaged;
    private InAttackRange inAttackRange;
    private Attack attack;

    override protected void Start()
    {
        root = new Sequence();
        seqDeath = new Sequence();
        seqChase = new Sequence();
        seqAttack = new Sequence();
        selMove = new Selector();
        selChase = new Selector();
        patrol = new Patrol(this);
        isDead = new IsDead(this);
        death = new Death(this);
        inSight = new InSight(this);
        chase = new Chase(this);
        isDamaged = new IsDamaged(this);
        inAttackRange = new InAttackRange(this);
        attack = new Attack(this);

        base.Start();
        Init();
        StartCoroutine("BehaviorProcess");
    }

    override protected void InitStatus()
    {
        maxHP = 50;
        damage = 10;
        hp = maxHP;
        horizontalSpeed = 1.6f;
    }

    private void Init()
    {
        //Patrol

        //Chase
        selChase.AddChild(inSight);
        selChase.AddChild(isDamaged);

        seqChase.AddChild(chase);
        seqChase.AddChild(selChase);

        //Attack
        seqAttack.AddChild(attack);
        seqAttack.AddChild(inAttackRange);

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

    public override void Attack()
    {
        effectManager.StartEffects("FX_SlimeAttack");
        base.Attack();
    }

    protected override void InitAttack()
    {
        attackActiveDuration = 0.5f;
        attackPreDelay = 0.2f;
        meleeAttack = true;
        attackRange.cooldownTime = 1;
    }
}