using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private CharacterManager inst_Character;

    private ICrowdControlSkill skillFirst;
    private ICrowdControlSkill skillSecond;
    private ICrowdControlSkill skillCombo;

    private float maxHP = 50;
    private float attack = 10;
    private float speed = 5;
    private float atkSpeed = 10;
    private float currentHP;

    [SerializeField]
    private Transform[] patrolPoints;

    private int currentPoint;

    private Rigidbody rigidbody;
    private Sight sight;
    private Transform target;

    private void Start()
    {
        inst_Character = CharacterManager.getInstance();
        rigidbody = GetComponent<Rigidbody>();
        sight = transform.Find("Sight").GetComponent<Sight>();
        target = inst_Character.transform;

        skillFirst = inst_Character.skillFirst;
        skillSecond = inst_Character.skillSecond;
        skillCombo = inst_Character.skillCombo;

        currentHP = maxHP;
        transform.position = patrolPoints[0].position;
        currentPoint = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Trigger Damaged Bullet");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Skill1")
        {
            Debug.Log("Trigger Damaged Skill1");
            Destroy(other.gameObject);
            skillFirst.GetTarget(rigidbody);
            currentHP = skillFirst.Damage(currentHP);
        }
        if (other.gameObject.tag == "Skill2")
        {
            Debug.Log("Trigger Damaged Skill2");
            Destroy(other.gameObject);
            skillSecond.GetTarget(rigidbody);
            currentHP = skillSecond.Damage(currentHP);
        }
        if (other.gameObject.tag == "Skill3")
        {
            Debug.Log("Trigger Damaged Skill3");
            Destroy(other.gameObject);
            currentHP = skillCombo.Damage(currentHP);
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
    }
}