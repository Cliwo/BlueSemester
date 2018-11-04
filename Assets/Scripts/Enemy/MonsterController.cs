using System.Collections;
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

    private int currentPoint;
    private int beforePoint;

    private Sight sight;
    private Transform target;
    private ICrowdControlSkill skill;
    private EffectManager effectManager;

    private void Awake()
    {
        sight = transform.Find("Sight").GetComponent<Sight>();
        effectManager = GetComponentInChildren<EffectManager>();
    }

    private void Start()
    {
        inst_Character = CharacterManager.getInstance();
        target = inst_Character.transform;

        currentHP = maxHP;
        transform.position = patrolPoints[0].position;
        currentPoint = 0;
        effectManager.StartEffects("MagicCircle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            effectManager.StartEffects("SkillFire");
            skill = other.gameObject.GetComponent<BulletManager>().skill;
            currentHP = skill.Damage(currentHP);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            effectManager.StartEffects("SlimeAttack");
        }
    }

    public void Init()
    {
        Debug.Log("MonsterController Init");
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
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target.position);
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
}