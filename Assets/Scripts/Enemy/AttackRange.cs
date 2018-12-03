using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public bool inAttackRange;
    private float timer;
    public float waitingTime = 0;
    private bool raidMonster;

    private void Start()
    {
        inAttackRange = false;
        timer = 0;

        raidMonster = transform.parent.GetComponent<MonsterController>().raidMonster;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && raidMonster)
        {
            inAttackRange = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && raidMonster == false)
        {
            if (timer > waitingTime || timer == 0)
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