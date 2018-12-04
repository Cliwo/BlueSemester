using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidMonsterSpell : MonoBehaviour
{
    public float Damage { get; set; }
    public float AttackPreDelay { get; set; }
    public float AttackActiveDuration { get; set; }
    private bool canAttack = false;
    private float timer;

    private void Start()
    {
        timer = 0;
        Destroy(this.gameObject, AttackActiveDuration);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Pawn targetPawn = other.GetComponent<Pawn>();

        if (targetPawn != null && other.gameObject.tag == "Player" && timer > AttackPreDelay)
        {
            Debug.Log("OnTriggerEnter : " + timer);
            canAttack = true;
            ApplyDamage(targetPawn);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canAttack) return;

        Pawn targetPawn = other.GetComponent<Pawn>();

        if (targetPawn != null && other.gameObject.tag == "Player" && timer > AttackPreDelay)
        {
            canAttack = true;
            Debug.Log("OnTriggerStay : " + timer);
            ApplyDamage(targetPawn);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canAttack = false;
        }
    }

    private void ApplyDamage(Pawn target)
    {
        target.hp -= Damage;
        Debug.Log("Player damaged - " + Damage + " = " + target.hp);
    }
}