using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour //Manager 클래스가 아님, 모든 monster에 붙음 
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
        effectManager.StartEffects("MagicCircle"); //맵에 존재하는 모든 몬스터에 일괄적으로 effect를 발동시킨다.  
        //개별로 생성될 때 effect가 필요하지 않나? (SpawnManager가 필요)
    }

    private void OnTriggerEnter(Collider other) //bullet 과 충돌 시 
    {
        if (other.gameObject.tag == "Bullet")
        {
            effectManager.StartEffects("SkillFire");
            skill = other.gameObject.GetComponent<BulletManager>().skill; //!? 모든 bullet에 bulletManager가 붙어있음.. 
            currentHP = skill.Damage(currentHP);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            effectManager.StartEffects("SlimeAttack"); // 슬라임의 '타격' 처리 (유저의 '피격')
        }
    }

    public void Init()
    {
        Debug.Log("MonsterController Init");
        currentHP = maxHP;
    }

    public bool IsDead() //Dead 시 움직임을 막을 수 있어야함. BT 코드안에 있을 수 있음. 
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