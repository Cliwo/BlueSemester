using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Sprite[] background = new Sprite[3];
    public Sprite currentBackground;

    private void Start()
    {
        currentBackground = GetComponent<Image>().sprite;
    }

    public void RequestResourceInventory()
    {
        Debug.Log("RequestResourceInventory");
        currentBackground = background[0];
    }

    public void RequestAccessoryInventory()
    {
        Debug.Log("RequestAccessoryInventory");
        currentBackground = background[1];
    }

    public void RequestFairyInventory()
    {
        Debug.Log("RequestFairyInventory");
        currentBackground = background[2];
    }

    private void OnGUI()
    {
        GetComponent<Image>().sprite = currentBackground;
    }
}