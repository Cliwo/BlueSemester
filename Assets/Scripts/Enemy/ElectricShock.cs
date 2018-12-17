using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShock : MonoBehaviour
{
    public float damage = 10;
    public GameObject effect;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter!");
        Pawn pawnScript = other.gameObject.GetComponent<Pawn>();

        if (pawnScript != null && other.gameObject.tag == "Player")
        {
            Debug.Log("player!");

            GameObject go = Instantiate(effect, other.gameObject.transform.position, Quaternion.identity);
            Destroy(go, 3);
            ApplyDamage(pawnScript);
        }
    }

    private void ApplyDamage(Pawn target)
    {
        target.hp -= damage;
        Debug.Log("Player damaged - " + damage + " = " + target.hp);
    }
}