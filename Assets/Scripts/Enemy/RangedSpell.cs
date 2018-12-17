using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    public Transform target;
    private CharacterManager inst_Character;
    private float lifespan = 5.0f;
    public float spellSpeed = 8.0f;
    public float damage;

    private void Start()
    {
        inst_Character = CharacterManager.getInstance();
        if (target == null)
        {
            target = inst_Character.GetComponent<Transform>();
        }
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z); // y를 일자에서 target의 y로 수정
        this.transform.LookAt(targetPos);

        Destroy(this.gameObject, lifespan);
    }

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, this.transform.position);
            if (distance > 0)
            {
                transform.Translate(Vector3.forward * spellSpeed * Time.deltaTime);
            }
            else
            {
                HitTarget();
            }
        }
    }

    private void HitTarget()
    {
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        Pawn pawnScript = other.GetComponent<Pawn>();

        if (pawnScript != null && other.gameObject.tag == "Player")
        {
            ApplyDamage(pawnScript);
            Destroy(this.gameObject);
        }
    }

    private void ApplyDamage(Pawn target)
    {
        target.hp -= damage;
        Debug.Log("Player damaged - " + damage + " = " + target.hp);
    }
}