using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Slime : MonoBehaviour
{
    private Sequence root = new Sequence();
    private Sequence seqDeath = new Sequence();
    private Sequence seqChase = new Sequence();

    private Selector selector = new Selector();

    private Patrol patrol = new Patrol();
    private IsDead isDead = new IsDead();
    private Death death = new Death();
    private InSight inSight = new InSight();
    private Chase chase = new Chase();

    private MonsterController monController;

    private void Start()
    {
        Init();
        StartCoroutine("BehaviorProcess");
    }

    private void Init()
    {
        Debug.Log("BT_Slime Init");
        monController = gameObject.GetComponent<MonsterController>();
        monController.Init();

        patrol.MonController = monController;
        isDead.MonController = monController;
        death.MonController = monController;
        inSight.MonController = monController;
        chase.MonController = monController;

        seqChase.AddChild(chase);
        seqChase.AddChild(inSight);

        selector.AddChild(patrol);
        selector.AddChild(seqChase);

        seqDeath.AddChild(death);
        seqDeath.AddChild(isDead);

        root.AddChild(seqDeath);
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