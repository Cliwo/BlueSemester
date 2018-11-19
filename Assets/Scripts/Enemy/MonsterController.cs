using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : Pawn
{
    private CharacterManager inst_Character;

    private float maxHP = 50;
    private float attack = 10;
    private float atkSpeed = 10;

    [SerializeField]
    private Transform[] patrolPoints;
    private EffectManager effectManager;

    //public Transform spawnPoint;

    private int currentPoint;
    private int beforePoint;

    private Sight sight;
    private Transform target;
    private AttackRange attackRange;
    private CapsuleCollider collider;

    override protected void InitStatus()
    {
        hp = maxHP;
        horizontalSpeed = 1.0f;
    }

    private void Awake()
    {
        sight = transform.Find("Sight").GetComponent<Sight>();
        attackRange = transform.Find("AttackRange").GetComponent<AttackRange>();
        effectManager = GetComponentInChildren<EffectManager>();
    }

    override protected void Start()
    {
        base.Start();
        inst_Character = CharacterManager.getInstance();
        target = inst_Character.transform;

        collider = GetComponent<CapsuleCollider>();

        hp = maxHP;
        //patrolPoints[0].position = spawnPoint.position;
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
        currentPoint = 0;
        //effectManager.StartEffects("MagicCircle");
    }
    private void OnTriggerEnter(Collider other) //bullet 과 충돌 시 
    {
        // TODO 1115 이 부분 현재 수정 필요. 충돌판정을 이 스크립트에서 하지 않아야함. Skill 에서 해야함.
        // if (other.gameObject.tag == "Player")
        // {
        //     effectManager.StartEffects("SkillFire");
        //     Physics.IgnoreCollision(collider, other);
        // }

        // if (other.gameObject.tag == "Bullet")
        // {
        //     effectManager.StartEffects("SlimeAttack");
        //     effectManager.StartEffects("FireSkill");
        // }
    }

    public void Init()
    {
        hp = maxHP;
    }

    public bool IsDead()
    {
        if (hp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Patrol()
    {
        if (patrolPoints.Length > 0 && !lockOtherComponentInfluenceOnTransform)
        {
            if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 0.5f)
            {
                currentPoint++;
            }
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, horizontalSpeed * Time.deltaTime);
            transform.LookAt(patrolPoints[currentPoint].position);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public bool InSight()
    {
        if (sight.inSight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Chase()
    {
        if(lockOtherComponentInfluenceOnTransform)
            return;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), horizontalSpeed * Time.deltaTime);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    public bool IsDamaged()
    {
        if (hp < maxHP)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool InAttackRange()
    {
        Debug.Log("attackrange? : " + attackRange.inAttackRange);
        if (attackRange.inAttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Attack()
    {
        Debug.Log("Monster Attacked");
    }

    public void Tornado()
    {
    }

    public void ThunderStroke()
    {
    }

    public void Wield()
    {
    }

    public void Pierce()
    {
    }

    public void Summon()
    {
    }

    public bool IsCorrectSkill()
    {
        // true, false 분기 지정 필요
        return true;
    }

    public bool IsRestOver()
    {
        // true, false 분기 지정 필요
        // 스킬간의 간격이 길 경우 텀을 주기 위해
        return true;
    }

    public void RestInit()
    {
        // 스킬간의 간격이 길 경우 텀을 주기 위해
    }

    
}