﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private CharacterManager inst_Character;

    private float maxHP = 50;
    private float attack = 10;
    private float speed = 1;
    private float atkSpeed = 10;
    private float currentHP;

    [SerializeField]
    private Transform[] patrolPoints;

    private EffectManager effectManager;

    //public Transform spawnPoint;

    private int currentPoint;
    private int beforePoint;

    private Sight sight;
    private Transform target;
    private ICrowdControlSkill skill;
    private AttackRange attackRange;
    private CapsuleCollider collider;

    private void Awake()
    {
        sight = transform.Find("Sight").GetComponent<Sight>();
        attackRange = transform.Find("AttackRange").GetComponent<AttackRange>();
        effectManager = GetComponentInChildren<EffectManager>();
    }

    private void Start()
    {
        inst_Character = CharacterManager.getInstance();
        target = inst_Character.transform;

        collider = GetComponent<CapsuleCollider>();

        currentHP = maxHP;
        //patrolPoints[0].position = spawnPoint.position;
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
        currentPoint = 0;
        //effectManager.StartEffects("MagicCircle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collider, other);
        }

        if (other.gameObject.tag == "Bullet")
        {
            effectManager.StartEffects("FireSkill");
            skill = other.gameObject.GetComponent<BulletManager>().skill;
            currentHP = skill.Damage(currentHP);
        }
    }

    public void Init()
    {
        currentHP = maxHP;
    }

    public bool IsDead()
    {
        if (currentHP <= 0)
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
        if (patrolPoints.Length > 0)
        {
            if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < 0.5f)
            {
                currentPoint++;
            }
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, speed * Time.deltaTime);
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
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), speed * Time.deltaTime);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    public bool IsDamaged()
    {
        if (currentHP < maxHP)
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