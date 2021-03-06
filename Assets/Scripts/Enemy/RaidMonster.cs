﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidMonster : MonoBehaviour
{
    private int currentSkill;
    private int[] skillSet = new int[5];
    // 1 : tornado
    // 2 : thunderstroke
    // 3 : wield
    // 4 : summon
    // 5 : pierce

    private float maxHP = 500;
    private float currentHP;
    private Transform targetPlayer;
    private CharacterManager inst_Character;

    [SerializeField]
    private GameObject rangedSpell;

    [SerializeField]
    private GameObject hitEffect;

    public GameObject tornado;
    public GameObject thunderStroke;
    public GameObject wield;
    public GameObject pierce;

    private void Start()
    {
        inst_Character = CharacterManager.getInstance();
        targetPlayer = inst_Character.GetComponent<Transform>();

        skillSet[0] = 1;
        skillSet[1] = 2;
        skillSet[2] = 3;
        skillSet[3] = 4;
        skillSet[4] = 5;
        currentHP = maxHP;
        currentSkill = 0;

        // 공격 시작 조건
        StartCoroutine("StartPattern");
    }

    private IEnumerator StartPattern()
    {
        while (currentHP > 0)
        {
            yield return new WaitForSeconds(5);
            UseSkill();
        }
    }

    private void UseSkill()
    {
        switch (skillSet[currentSkill])
        {
            case 1:
                Tornado();
                break;

            case 2:
                ThunderStroke();
                break;

            case 3:
                Wield();
                break;

            case 4:
                Summon();
                break;

            case 5:
                Pierce();
                break;
        }

        currentSkill++;
        if (currentSkill >= skillSet.Length)
        {
            currentSkill = 0;
        }
    }

    private void Tornado()
    {
        Debug.Log("Tornado");
        AttackEffect(tornado);
    }

    private void ThunderStroke()
    {
        Debug.Log("ThunderStroke");
        AttackEffect(thunderStroke);
    }

    private void Wield()
    {
        Debug.Log("Wield");
        GameObject clone = Instantiate(rangedSpell, this.transform.position, this.transform.rotation);
        clone.GetComponent<RangedSpell>().target = targetPlayer;
    }

    private void Summon()
    {
        Debug.Log("Summon");
        AttackEffect(thunderStroke);

        //StartEffect();
    }

    private void Pierce()
    {
        Debug.Log("Pierce");
        AttackEffect(thunderStroke);

        //GameObject clone = Instantiate(pierce, this.transform.position, this.transform.rotation);
        //clone.GetComponent<RangedSpell>().target = targetPlayer;
    }

    private void AttackEffect(GameObject spell)
    {
        Vector3 pos = new Vector3(targetPlayer.position.x, targetPlayer.position.y, targetPlayer.position.z);
        GameObject effect = Instantiate(spell, pos, targetPlayer.rotation);
        Destroy(effect, 5.0f);
    }
}