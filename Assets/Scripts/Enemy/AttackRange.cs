using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public bool inAttackRange;
    private float timer;
    public float cooldownTime = 0;
    private bool raidMonster;

    private void Start()
    {
        inAttackRange = false;
        timer = 0;

        raidMonster = transform.parent.GetComponent<MonsterController>().raidMonster;
    }

    // TODO : 공격하는 시점과 횟수가 왜인지 모르게 정확하지 않다. 수정 필요.
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && raidMonster)
        {
            inAttackRange = true;
        }

        if (other.gameObject.tag == "Player" && raidMonster == false)
        {
            inAttackRange = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && raidMonster == false)
        {
            if (timer > cooldownTime)
            {
                inAttackRange = true;
                timer = 0;
            }
            else
            {
                inAttackRange = false;
            }

            timer += Time.deltaTime;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inAttackRange = false;
            timer = 0;
        }
    }
}