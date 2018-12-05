using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    private ItemDatabase db;

    public int slotX, slotY;
    public List<Item> slots = new List<Item>();

    private bool showInventory = false;

    public GUISkin skin;

    private bool showTooltip;
    private string tooltip;

    private bool dragItem;
    private Item draggedItem;
    private int prevIndex;

    private void Start()
    {
        slotX = 5;
        slotY = 4;

        for (int i = 0; i < slotX * slotY; i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
        db = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

        inventory[0] = db.items[0];
        //int k = 0;
        //foreach (var item in db.items)
        //{
        //    inventory[k] = item;
        //    k++;
        //}

        AddItem(0101);
        AddItem(0003);
        AddItem(0202);

        RemoveItem(0101);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
        }
    }

    private void OnGUI()
    {
        tooltip = "";
        GUI.skin = skin;

        if (showInventory)
        {
            DrawInventory();
        }
        if (showTooltip)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + 25, Event.current.mousePosition.y + 25, 200, 200), tooltip, skin.GetStyle("tooltip"));
        }
        if (dragItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x - 25, Event.current.mousePosition.y - 25, 50, 50), draggedItem.itemIcon);
        }
    }

    private void DrawInventory()
    {
        int k = 0;
        Event e = Event.current;

        for (int j = 0; j < slotY; j++)
        {
            for (int i = 0; i < slotX; i++)
            {
                Rect slotRect = new Rect(i * 52 + 100, j * 52 + 30, 50, 50);
                GUI.Box(slotRect, "", skin.GetStyle("slot background"));

                if (inventory[k] == null) continue;
                slots[k] = inventory[k];

                if (slots[k].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[k].itemIcon);

                    if (slotRect.Contains(e.mousePosition))
                    {
                        tooltip = CreateTooltip(slots[i]);
                        showTooltip = true;

                        if (e.button == 0 && e.type == EventType.MouseDrag && !dragItem)
                        {
                            dragItem = true;
                            prevIndex = k;
                            draggedItem = slots[k];
                            inventory[k] = new Item();
                        }
                        if (e.type == EventType.MouseUp && dragItem)
                        {
                            inventory[prevIndex] = inventory[k];
                            inventory[k] = draggedItem;
                            dragItem = false;
                            draggedItem = null;
                        }
                    }
                }
                else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseUp && dragItem)
                        {
                            inventory[k] = draggedItem;
                            dragItem = false;
                            draggedItem = null;
                        }
                    }
                }

                if (tooltip == "")
                {
                    showTooltip = false;
                }

                k++;
            }
        }
    }

    private string CreateTooltip(Item item)
    {
        tooltip = "Item name: <color=#a10000><b>" + item.itemName + "</b></color>\nItem Description: <color=#ffffff>" + item.itemDesc + "</color>";
        return tooltip;
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == null)
            {
                for (int j = 0; j < db.items.Count; j++)
                {
                    if (db.items[j].itemID == id)
                    {
                        inventory[i] = db.items[j];
                        return;
                    }
                }
            }
        }
    }

    private bool inventoryContains(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            return (inventory[i].itemID == id);
        }
        return false;
    }

    private void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }
}