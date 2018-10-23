using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject enemy;
    private int maxMonster = 4;

    [SerializeField]
    private Transform[] spawnPoints;

    private void RandomSpawn()
    {
        float randomX = Random.Range(-50f, 50f);
        float randomZ = Random.Range(-50f, 50f);
        if (enableSpawn)
        {
            GameObject go = Instantiate(enemy, new Vector3(randomX, 2f, randomZ), Quaternion.identity);
        }
    }

    private void Start()
    {
        InvokeRepeating("RandomSpawn", 3, 3);
        //InvokeRepeating("PointSpawn", 3, 5);
    }

    private void PointSpawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject go = Instantiate(enemy, spawnPoints[i].position, Quaternion.identity);
            go.GetComponent<MonsterController>().spawnPoint = spawnPoints[i];
        }
    }
}