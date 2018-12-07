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

    private Vector3 skillPosition;

    public GameObject tornado;
    public GameObject thunderStroke;
    public GameObject wield;
    public GameObject pierce;
    public GameObject warning;

    public Animator animator;

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
        maxHP = 1000;
        damage = 200;
        hp = maxHP;
        horizontalSpeed = 0;
    }

    private void Init()
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
            yield return new WaitForSeconds(4.5f);
            animator.SetBool("Idle", false);
            if (currentSkill != 2 && currentSkill != 3) // 휘두르기, 소환 제외
            {
                ShowWarningSign();
            }
            yield return new WaitForSeconds(0.5f);
            UseSkill();
            animator.SetBool("Idle", true);
        }

        StartCoroutine("BehaviorProcess");
    }

    private void ShowWarningSign()
    {
        skillPosition = target.position;
        GameObject warningSign = Instantiate(warning, target.position, target.rotation);
        Destroy(warningSign, 1);
    }

    private void UseSkill()
    {
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
        animator.SetTrigger("Tornado");
        GameObject clone = Instantiate(tornado, skillPosition, Quaternion.identity);
        RaidMonsterSpell spell = clone.GetComponent<RaidMonsterSpell>();
        spell.Damage = 200;
        spell.AttackActiveDuration = 4;
        spell.AttackPreDelay = 0.5f;
    }

    private void ThunderStroke()
    {
        Debug.Log("ThunderStroke");
        animator.SetTrigger("ThunderStroke");
        GameObject clone = Instantiate(thunderStroke, skillPosition, Quaternion.identity);

        RaidMonsterSpell spell = clone.GetComponent<RaidMonsterSpell>();
        spell.Damage = 100;
        spell.AttackActiveDuration = 1.5f;
        spell.AttackPreDelay = 0.3f;
    }

    private void Wield()
    {
        Debug.Log("Wield");
        animator.SetTrigger("Wield");
        GameObject clone = Instantiate(wield, this.transform.position, this.transform.rotation);

        RaidMonsterSpell spell = clone.GetComponent<RaidMonsterSpell>();
        spell.Damage = 30;
        spell.AttackActiveDuration = 1;
        spell.AttackPreDelay = 0.3f;
    }

    private void Summon()
    {
        Debug.Log("Summon");
        animator.SetTrigger("Summon");
    }

    private void Pierce()
    {
        Debug.Log("Pierce");
        animator.SetTrigger("Pierce");
        GameObject clone = Instantiate(pierce, this.transform.position, this.transform.rotation);
        clone.transform.LookAt(target);

        RaidMonsterSpell spell = clone.GetComponent<RaidMonsterSpell>();
        spell.Damage = 50;
        spell.AttackActiveDuration = 1.2f;
        spell.AttackPreDelay = 0.2f;
    }

    public override void Death()
    {
        animator.SetBool("Idle", false);
        animator.SetTrigger("Death");
        base.Death();
    }
}