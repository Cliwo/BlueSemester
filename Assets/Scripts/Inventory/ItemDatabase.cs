using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Start()
    {
        Debug.Log("items");
        // string name, int id, string desc, ItemType type
        items.Add(new Item("Tree", "wood", 0001, "This is a tree", Item.ItemType.Resource));
        // TREE, SHELL, IRON_ORE, SULFUR
        items.Add(new Item("Shell", "seashell", 0002, "This is a shell", Item.ItemType.Resource));
        items.Add(new Item("Rock", "rock", 0003, "This is a rock", Item.ItemType.Resource));
        //items.Add(new Item("Sulfur", "sulfur", 0004, "This is a sulfur", Item.ItemType.Resource));
        //items.Add(new Item("Necklace", "necklace", 0101, "This is a necklace", Item.ItemType.Accessory));
        //items.Add(new Item("Earring", "earring", 0102, "This is a earring", Item.ItemType.Accessory));
        items.Add(new Item("Fire Fairy", "fire", 0201, "This is a fire fairy", Item.ItemType.Fairy));
        items.Add(new Item("Water Fairy", "water", 0202, "This is a water fairy", Item.ItemType.Fairy));
        items.Add(new Item("Electricity Fairy", "electricity", 0203, "This is a electricity fairy", Item.ItemType.Fairy));
    }

    public string GetName(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (id == items[i].itemID)
            {
                return items[i].itemName;
            }
        }
        return "Error : Not Found";
    }

    public int GetID(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (name.Contains(items[i].itemName))
            {
                return items[i].itemID;
            }
        }
        return 99999;
    }
}