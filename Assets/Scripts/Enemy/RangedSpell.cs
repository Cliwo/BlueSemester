using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    public Transform target;
    private CharacterManager inst_Character;
    private float lifespan = 5.0f;

    private void Start()
    {
        inst_Character = CharacterManager.getInstance();
        if (target == null)
        {
            target = inst_Character.GetComponent<Transform>();
        }
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
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
                transform.Translate(Vector3.forward * 8.0f * Time.deltaTime);
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