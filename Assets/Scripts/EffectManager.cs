using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public List<Transform> effects;

    public void StartEffects(string effectName)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].name.CompareTo(effectName) == 0) //모든 객체의 동일한 이름의 이펙트를 한번에 발동시키는 형태
            {
                effects[i].gameObject.SetActive(false);
                effects[i].gameObject.SetActive(true);
                break;
            }
        }
    }
}