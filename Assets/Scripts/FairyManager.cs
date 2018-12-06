using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyManager : MonoBehaviour
{
    private CharacterManager inst_char;
    private Transform target;

    [SerializeField]
    private float xDistance;

    [SerializeField]
    private float yDistance;

    [SerializeField]
    private float zDistance;

    [SerializeField]
    public float dampSpeed;

    // Use this for initialization
    private void Start()
    {
        inst_char = CharacterManager.getInstance();
        target = inst_char.gameObject.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 newPos = target.position + new Vector3(xDistance, yDistance, -zDistance);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * dampSpeed);
        transform.LookAt(target);
    }
}