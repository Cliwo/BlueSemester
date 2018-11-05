using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public List<Transform> effects;

    public void StartEffect(string effectName)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].name.CompareTo(effectName) == 0)
            {
                effects[i].gameObject.SetActive(false);
                effects[i].gameObject.SetActive(true);
                break;
            }
        }
    }
}