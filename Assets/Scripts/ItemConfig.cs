using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class ItemConfig : ScriptableObject
{
    public List<Item> items;

    public ItemConfig()
    {
        items = new List<Item>();
    }

    public Item GetReference(string itemId)
    {
        foreach (Item item in items)
        {
            if (item.Id == itemId) return item;
        }
        return null;
    }

    public Item GetItemClone(string itemId)
    {
        Item item = GetReference(itemId);
        return item != null ? item.GetClone() : null;
    }
}

[Serializable]
public enum ItemRarity
{
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Legendary = 4,
    Mythical = 5,
}

[Serializable]
public enum ItemType
{
    Weapon = 0,
    Head = 1,
    Body = 2,
    Feet = 3,
}

[Serializable]
public class Item
{
    public Sprite icon;
    public string itemName;
    public string description;

    public ItemRarity itemRarity;
    public ItemType itemType;

    public float damage;
    public float defence;
    public float strength;
    public float agility;
    public float intel;

    [HideInInspector] public bool isEquipped;
    [HideInInspector] public string Id = System.Guid.NewGuid().ToString();

    public Item GetClone()
    {
        return (Item)this.MemberwiseClone();
    }
}