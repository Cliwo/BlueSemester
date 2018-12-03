using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : Pawn
{
    protected CharacterManager inst_Character;

    protected float maxHP = 12;
    private float attack = 10;
    protected float atkSpeed = 10;

    [SerializeField]
    protected Transform[] patrolPoints;

    protected EffectManager effectManager;

    //public Transform spawnPoint;

    protected int currentPoint;
    protected int beforePoint;

    protected Sight sight;
    protected Transform target;
    protected AttackRange attackRange;
    protected CapsuleCollider collider;
    public bool raidMonster = false;

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

        if (attackRange.waitingTime == 0)
        {
            attackRange.waitingTime = 2;
        }
    }

    private void OnTriggerEnter(Collider other) //bullet 과 충돌 시
    {
        // TODO 1115 이 부분 현재 수정 필요. 충돌판정을 이 스크립트에서 하지 않아야함. Skill 에서 해야함.
        // 이건 그냥 플레이어와 몬스터가 서로 겹쳤을 때 그냥 지나칠 수 있게 하는 코드이므로 둬도 괜찮음.
        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collider, other);
        }
    }

    virtual protected void Init()
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
        if (lockOtherComponentInfluenceOnTransform)
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
        if (attackRange.inAttackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void Attack()
    {
        Debug.Log("Monster Attacked");
    }
}