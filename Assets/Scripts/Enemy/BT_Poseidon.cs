using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Poseidon : MonsterController
{
    private Sequence root;

    private Sequence seqDeath;

    private InAttackRange inAttackRange;
    private IsDead isDead;
    private Death death;

    private int currentSkill;
    private int[] skillSet = new int[5];
    // 1 : tornado
    // 2 : thunderstroke
    // 3 : wield
    // 4 : summon
    // 5 : pierce

    //private float maxHP = 500;
    private float currentHP;

    //private Transform target;
    //private CharacterManager inst_Character;

    [SerializeField]
    private GameObject rangedSpell;

    [SerializeField]
    private GameObject hitEffect;

    public GameObject tornado;
    public GameObject thunderStroke;
    public GameObject wield;
    public GameObject pierce;

    protected override void Start()
    {
        root = new Sequence();
        seqDeath = new Sequence();
        inAttackRange = new InAttackRange(this);
        isDead = new IsDead(this);
        death = new Death(this);

        base.Start();
        Init();
        StartCoroutine("BehaviorProcess");

        //inst_Character = CharacterManager.getInstance();
        //target = inst_Character.GetComponent<Transform>();

        raidMonster = true;

        skillSet[0] = 1;
        skillSet[1] = 2;
        skillSet[2] = 3;
        skillSet[3] = 4;
        skillSet[4] = 5;
        currentHP = maxHP;
        currentSkill = 0;
    }

    override protected void InitStatus()
    {
        base.InitStatus();
        horizontalSpeed = 0;
    }

    override protected void Init()
    {
        //Death
        seqDeath.AddChild(death);
        seqDeath.AddChild(isDead);

        root.AddChild(seqDeath);
        root.AddChild(inAttackRange);
    }

    private IEnumerator BehaviorProcess()
    {
        Debug.Log("BehaviorProcess");

        while (!root.Invoke() && !inAttackRange.Invoke())
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Behavior Process Exit");

        if (!isDead.Invoke() && inAttackRange.Invoke())
        {
            StartCoroutine("StartPattern");
        }
    }

    private IEnumerator StartPattern()
    {
        Debug.Log("StartPattern");

        while (!isDead.Invoke() && inAttackRange.Invoke())
        {
            yield return new WaitForSeconds(5);
            UseSkill();
        }

        StartCoroutine("BehaviorProcess");
    }

    private void UseSkill()
    {
        Debug.Log("UseSkill + " + currentSkill);
        switch (skillSet[currentSkill])
        {
            case 1:
                Tornado();
                break;

            case 2:
                ThunderStroke();
                break;

            case 3:
                Wield();
                break;

            case 4:
                Summon();
                break;

            case 5:
                Pierce();
                break;
        }

        currentSkill++;
        if (currentSkill >= skillSet.Length)
        {
            currentSkill = 0;
        }
    }

    private void Tornado()
    {
        Debug.Log("Tornado");
        AttackEffect(tornado);
    }

    private void ThunderStroke()
    {
        Debug.Log("ThunderStroke");
        AttackEffect(thunderStroke);
    }

    private void Wield()
    {
        Debug.Log("Wield");
        GameObject clone = Instantiate(rangedSpell, this.transform.position, this.transform.rotation);
        clone.GetComponent<RangedSpell>().target = target;
    }

    private void Summon()
    {
        Debug.Log("Summon");
        AttackEffect(thunderStroke);

        //StartEffect();
    }

    private void Pierce()
    {
        Debug.Log("Pierce");
        AttackEffect(thunderStroke);

        //GameObject clone = Instantiate(pierce, this.transform.position, this.transform.rotation);
        //clone.GetComponent<RangedSpell>().target = targetPlayer;
    }

    private void AttackEffect(GameObject spell)
    {
        Vector3 pos = new Vector3(target.position.x, target.position.y, target.position.z);
        GameObject effect = Instantiate(spell, pos, target.rotation);
        Destroy(effect, 5.0f);
    }
}