using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Slime : MonoBehaviour
{
    private Sequence root = new Sequence();
    private Sequence seqDeath = new Sequence();
    private Sequence seqChase = new Sequence();
    private Sequence seqDamage = new Sequence();
    private Sequence seqAttack = new Sequence();

    private Selector selector = new Selector();

    private Patrol patrol = new Patrol();
    private IsDead isDead = new IsDead();
    private Death death = new Death();
    private InSight inSight = new InSight();
    private Chase chase = new Chase();
    private IsDamaged isDamaged = new IsDamaged();
    private InAttackRange inAttackRange = new InAttackRange();
    private Attack attack = new Attack();

    private MonsterController monController;

    private void Start()
    {
        Init();
        StartCoroutine("BehaviorProcess");
    }

    private void Init()
    {
        monController = gameObject.GetComponent<MonsterController>();
        monController.Init();

        patrol.MonController = monController;
        isDead.MonController = monController;
        death.MonController = monController;
        inSight.MonController = monController;
        chase.MonController = monController;
        isDamaged.MonController = monController;
        inAttackRange.MonController = monController;
        attack.MonController = monController;

        seqDamage.AddChild(chase);
        seqDamage.AddChild(isDamaged);

        seqAttack.AddChild(attack);
        seqAttack.AddChild(inAttackRange);

        seqChase.AddChild(chase);
        seqChase.AddChild(inSight);

        selector.AddChild(patrol);
        selector.AddChild(seqChase);
        selector.AddChild(seqDamage);

        seqDeath.AddChild(death);
        seqDeath.AddChild(isDead);

        root.AddChild(seqDeath);
        root.AddChild(seqAttack);
        root.AddChild(selector);
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