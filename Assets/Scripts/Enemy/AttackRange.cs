using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public bool inAttackRange;
    private float timer;
    private float waitingTime;

    private void Start()
    {
        inAttackRange = false;
        timer = 0;
        waitingTime = 2f;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
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