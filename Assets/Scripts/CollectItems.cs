using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    private Inventory inventory;
    private ItemDatabase db;

    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        db = GameObject.Find("Database").GetComponent<ItemDatabase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("collect tree item");
            Destroy(this.gameObject);

            inventory.AddItem(db.GetID(this.gameObject.name));
        }
    }
}