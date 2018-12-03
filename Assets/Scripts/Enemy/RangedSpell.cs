using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    public Transform target;
    private CharacterManager inst_Character;
    private float lifespan = 5.0f;
    public float spellSpeed = 8.0f;

    private void Start()
    {
        inst_Character = CharacterManager.getInstance();
        if (target == null)
        {
            target = inst_Character.GetComponent<Transform>();
        }
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z); // 모든 range skill의 y값을 일정하게 해도 될까 의문.
        this.transform.LookAt(targetPos);

        Destroy(this.gameObject, lifespan);
    }

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, this.transform.position);
            if (distance > 2.0f)
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
}