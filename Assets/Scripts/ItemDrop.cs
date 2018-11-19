using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] items = new GameObject[1];

    private Transform trans;

    private GameObject obj;

    public bool canDrop = false;

    private void Start()
    {
        trans = GetComponent<Transform>();
        obj = GetComponent<GameObject>();

        StartCoroutine("DropTheItems");
    }

    private IEnumerator DropTheItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (canDrop)
            {
                int maxItems = 5;
                int rand = Random.Range(1, maxItems);
                Debug.Log("rand : " + rand);

                for (int i = 0; i < rand; i++)
                {
                    yield return new WaitForSeconds(0.3f);
                    Debug.Log(trans.position);

                    Instantiate(items[0], trans.position, Quaternion.identity);
                }

                // 여러가지 아이템 나오는 경우
                //for (int i = 0; i < maxItems; i++)
                //{
                //    int rand = Random.Range(0, 3);
                //    yield return new WaitForSeconds(0.3f);
                //    Instantiate(items[rand], trans.position, Quaternion.identity);
                //}

                //Destroy(this.gameObject);

                break;
            }
        }
    }
}