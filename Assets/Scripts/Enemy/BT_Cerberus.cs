using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Cerberus : MonsterController
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

    [SerializeField]
    private GameObject breathSkill;

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

        attackRange.cooldownTime = 3;
    }

    override protected void InitStatus()
    {
        maxHP = 350;
        damage = 200;
        hp = maxHP;
        horizontalSpeed = 0.7f;
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
        GameObject go = Instantiate(breathSkill, this.transform.position, this.transform.rotation);
        RangedSpell spell = go.GetComponent<RangedSpell>();
        spell.spellSpeed = 5;
        spell.damage = damage;
        spell.target = target;
    }
}