using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Poseidon : MonoBehaviour
{
    private Sequence root = new Sequence();
    private Sequence seqDeath = new Sequence();
    private Sequence seqSkills = new Sequence();

    private IsDead isDead = new IsDead();
    private Death death = new Death();
    private Tornado tornado = new Tornado();
    private ThunderStroke thunderStroke = new ThunderStroke();
    private Wield wield = new Wield();
    private Pierce pierce = new Pierce();
    private Summon summon = new Summon();

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

        isDead.MonController = monController;
        death.MonController = monController;
        tornado.MonController = monController;
        thunderStroke.MonController = monController;
        wield.MonController = monController;
        pierce.MonController = monController;
        summon.MonController = monController;

        seqSkills.AddChild(summon);
        seqSkills.AddChild(pierce);
        seqSkills.AddChild(wield);
        seqSkills.AddChild(thunderStroke);
        seqSkills.AddChild(tornado);

        seqDeath.AddChild(death);
        seqDeath.AddChild(isDead);

        root.AddChild(seqDeath);
        root.AddChild(seqSkills);
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