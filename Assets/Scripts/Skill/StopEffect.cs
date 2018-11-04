using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEffect : MonoBehaviour
{
    public float stopTime = 1.0f;

    private void OnEnable()
    {
        StartCoroutine(TimeStop());
    }

    private IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(stopTime);
        gameObject.SetActive(false);
    }
}