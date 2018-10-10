using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterManager inst_Character;

    private IEnemyState currentState;

    private ICrowdControlSkill skillFirst;
    private ICrowdControlSkill skillSecond;
    private ICrowdControlSkill skillCombo;

    private float hp = 500;
    private float attack = 10;
    private float speed = 9;
    private float atkSpeed = 10;

    private Rigidbody rigidbody;

    private static Enemy instance;

    public static Enemy getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            DestroyImmediate(this);
        }
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    private void Start()
    {
        inst_Character = CharacterManager.getInstance();
        rigidbody = GetComponent<Rigidbody>();

        skillFirst = inst_Character.skillFirst;
        skillSecond = inst_Character.skillSecond;
        skillCombo = inst_Character.skillCombo;

        ChangeState(new IdleState());
    }

    // Update is called once per frame
    private void Update()
    {
        currentState.Execute();
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
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
            hp = skillFirst.Damage(hp);
        }
        if (other.gameObject.tag == "Skill2")
        {
            Debug.Log("Trigger Damaged Skill2");
            Destroy(other.gameObject);
            skillSecond.GetTarget(rigidbody);
            hp = skillSecond.Damage(hp);
        }
        if (other.gameObject.tag == "Skill3")
        {
            Debug.Log("Trigger Damaged Skill3");
            Destroy(other.gameObject);
            hp = skillCombo.Damage(hp);
        }
    }
}