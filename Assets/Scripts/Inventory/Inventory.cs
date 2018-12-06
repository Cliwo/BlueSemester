using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;

    public static Inventory getInstance()
    {
        return instance;
    }

    public List<Item> inventory = new List<Item>();

    private ItemDatabase db;

    public int slotX, slotY;
    public List<Item> slotsResource = new List<Item>();
    public List<Item> slotsAccessory = new List<Item>();
    public List<Item> slotsFairy = new List<Item>();

    public float iconSizeX = 150;
    public float iconSizeY = 150;
    public float paddingX = 38.6f;
    public float paddingY = 0.24f;
    public float paddingLeft = 928.3f;
    public float paddingUp = 641.5f;

    private bool showInventory = false;

    public GUISkin skin;

    private bool showTooltip;
    private string tooltip;

    private bool dragItem;
    private Item draggedItem;
    private int prevIndex;

    private GameObject background;

    public Sprite[] backgroundSprite = new Sprite[3];
    public Sprite currentBackgroundSprite;
    private string currentType = "Resource";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        slotX = 4;
        slotY = 2;

        background = GameObject.Find("Background");
        currentBackgroundSprite = background.GetComponent<Image>().sprite;
        background.SetActive(false);

        for (int i = 0; i < slotX * slotY; i++)
        {
            slotsResource.Add(new Item());
            slotsAccessory.Add(new Item());
            slotsFairy.Add(new Item());
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

        AddItem(0001);
        AddItem(0002);
        AddItem(0003);
        AddItem(0201);
        AddItem(0202);
        AddItem(0203);

        //RemoveItem(0202);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
            if (showInventory) background.SetActive(true);
            else if (!showInventory) background.SetActive(false);
        }
    }

    private void OnGUI()
    {
        tooltip = "";
        GUI.skin = skin;

        if (showInventory)
        {
            if (currentType == "Resource")
            {
                DrawInventory(slotsResource);
            }
            else if (currentType == "Accessory")
            {
                DrawInventory(slotsAccessory);
            }
            else if (currentType == "Fairy")
            {
                DrawInventory(slotsFairy);
            }
        }
        // 툴팁을 잠시 막아놓는다
        //if (showTooltip)
        //{
        //    GUI.Box(new Rect(Event.current.mousePosition.x + tooltipPaddingX, Event.current.mousePosition.y + tooltipPaddingY, tooltipSizeX, tooltipSizeY), tooltip, skin.GetStyle("tooltip"));
        //}
        if (dragItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x - 25, Event.current.mousePosition.y - 25, 50, 50), draggedItem.itemIcon);
        }
    }

    private void DrawInventory(List<Item> slots)
    {
        background.GetComponent<Image>().sprite = currentBackgroundSprite;
        background.SetActive(true);

        int b = 0;
        int c = 0;
        Event e = Event.current;

        for (int a = 0; a < slotX * slotY; a++)
        {
            if (inventory[a] == null) continue;
            if (inventory[a].itemStringType != currentType) continue;

            Rect slotRect = new Rect(b * (iconSizeX + paddingX) + paddingLeft, c * (iconSizeY + paddingY) + paddingUp, iconSizeX, iconSizeY);
            GUI.Box(slotRect, inventory[a].itemCount.ToString(), skin.GetStyle("slot background"));

            slots[a] = inventory[a];

            if (slots[a].itemName != null)
            {
                GUIStyle style = GUIStyle.none;

                GUI.DrawTexture(slotRect, slots[a].itemIcon);

                if (slotRect.Contains(e.mousePosition))
                {
                    tooltip = CreateTooltip(slots[b]);
                    showTooltip = true;

                    // 아이템 스왑도 잠시 막아놓음
                    //if (e.button == 0 && e.type == EventType.MouseDrag && !dragItem)
                    //{
                    //    dragItem = true;
                    //    prevIndex = a;
                    //    draggedItem = slots[a];
                    //    inventory[a] = new Item();
                    //}
                    //if (e.type == EventType.MouseUp && dragItem)
                    //{
                    //    inventory[prevIndex] = inventory[a];
                    //    inventory[a] = draggedItem;
                    //    dragItem = false;
                    //    draggedItem = null;
                    //}
                }
            }
            //else
            //{
            //    if (slotRect.Contains(e.mousePosition))
            //    {
            //        if (e.type == EventType.MouseUp && dragItem)
            //        {
            //            inventory[a] = draggedItem;
            //            dragItem = false;
            //            draggedItem = null;
            //        }
            //    }
            //}

            if (tooltip == "")
            {
                showTooltip = false;
            }

            b++;
            if (b > slotX)
            {
                b = 0;
                c++;
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
            if (inventory[i].itemID == id)
            {
                inventory[i].itemCount++;
                return;
            }
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

    public void RequestResourceInventory()
    {
        Debug.Log("RequestResourceInventory");
        currentType = "Resource";
        currentBackgroundSprite = backgroundSprite[0];
    }

    public void RequestAccessoryInventory()
    {
        Debug.Log("RequestAccessoryInventory");
        currentType = "Accessory";
        currentBackgroundSprite = backgroundSprite[1];
    }

    public void RequestFairyInventory()
    {
        Debug.Log("RequestFairyInventory");
        currentType = "Fairy";
        currentBackgroundSprite = backgroundSprite[2];
    }

    public void CloseInventory()
    {
        // TODO : 한번에 껐다 켜도록 수정하기
        background.SetActive(false);
        showInventory = false;
        showTooltip = false;
    }
}