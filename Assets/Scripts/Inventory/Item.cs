﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Item
{
    public string itemName;
    public string itemCode;
    public int itemID;
    public string itemDesc;
    public Texture2D itemIcon;
    public ItemType itemType;
    public string itemStringType;
    public int itemCount;

    public enum ItemType
    {
        Resource,
        Accessory,
        Fairy
    }

    // 자원, 장신구, 요정(도감 형식)

    public Item()
    {
    }

    // name : 화면에 표시되는 이름, code : 스크립트 내에서 사용하기 위한 이름, id : 고유번호, desc : 게임에 표시될 아이템 설명 내용, type : 분류
    public Item(string name, string code, int id, string desc, ItemType type)
    {
        itemName = name;
        itemCode = code;
        itemID = id;
        itemDesc = desc;
        itemType = type;
        itemStringType = RequestItemStringType(type);
        itemIcon = Resources.Load<Texture2D>("UI/Inventory/" + itemStringType + "Icon/" + itemStringType.ToLower() + "Icon_" + code); // 아이템 아이콘 나오면 위치 수정 필요
        itemCount = 1;
    }

    private string RequestItemStringType(ItemType type)
    {
        switch (type)
        {
            case ItemType.Resource:
                return "Resource";

            case ItemType.Accessory:
                return "Accessory";

            case ItemType.Fairy:
                return "Fairy";
        }
        return "ERROR";
    }
}