using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : Pawn
{
    protected CharacterManager inst_Character;
    protected EffectManager effectManager;

    public float maxHP;
    public float damage;
    public float atkSpeed;
    public bool raidMonster = false;

    [SerializeField]
    protected Transform[] patrolPoints;

    protected int currentPoint;
    protected int beforePoint;

    protected Sight sight;
    protected Transform target;
    protected AttackRange attackRange;
    protected CapsuleCollider collider;

    protected float attackInitTime;
    protected float attackActiveDuration;
    protected float attackPreDelay;
    protected bool meleeAttack = false;
    protected bool canAttack = false;

    private void Awake()
    {
        sight = transform.Find("Sight").GetComponent<Sight>();
        attackRange = transform.Find("AttackRange").GetComponent<AttackRange>();
        effectManager = GetComponentInChildren<EffectManager>();
    }

    override protected void InitStatus()
    {
        maxHP = 50;
        damage = 10;
        hp = maxHP;
        horizontalSpeed = 1;
    }

    override protected void Start()
    {
        base.Start();
        inst_Character = CharacterManager.getInstance();
        target = inst_Character.transform;

        collider = GetComponent<CapsuleCollider>();

        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
        currentPoint = 0;
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

    public virtual void Death()
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
        InitAttack();
        Invoke("OnAttack", attackPreDelay);
    }

    protected virtual void InitAttack()
    {
    }

    protected void OnAttack()
    {
        if (canAttack)
        {
            ApplyDamage(target.GetComponent<Pawn>());
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collider, other);
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canAttack = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canAttack = false;
        }
    }

    private void ApplyDamage(Pawn targetPawn)
    {
        targetPawn.hp -= damage;
        Debug.Log("Player damaged - " + damage + " = " + targetPawn.hp);
    }
}