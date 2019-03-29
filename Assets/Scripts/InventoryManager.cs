using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Equipments equipments;
    [SerializeField] private ItemInfo itemInfo;

    /// <summary>
    /// Assigns inventory's and equipment's OnItemRightClickedEvents to ShowItemInfo.
    /// Hence right clicking on an item displays info about the item.
    /// </summary>
    private void Awake()
    {
        inventory.OnItemRightClickedEvent = equipments.OnItemRightClickedEvent = itemInfo.ShowItemInfo;
    }

    /// <summary>
    /// Checks if item can be equipped. Also equips it.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="lastItem"></param>
    /// <returns></returns>
    public bool Equip(Item item, out Item lastItem)
    {
        if (inventory.RemoveItem(item))
        {
            Item previousItem;
            if (equipments.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    previousItem.isEquipped = false;
                    inventory.AddItem(previousItem);
                }
                item.isEquipped = true;
                lastItem = previousItem;
                return true;
            }
            else
            {
                inventory.AddItem(item);
            }
        }
        lastItem = null;
        return false;
    }

    /// <summary>
    /// Checks if an item can be unequipped. Also unequips it. 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Unequip(Item item)
    {
        if (!inventory.IsFull() && equipments.RemoveItem(item))
        {
            item.isEquipped = false;
            inventory.AddItem(item);
            return true;
        }
        return false;
    }

    public ItemInfo ItemInfo
    {
        get
        {
            return itemInfo;
        }
    }

    public Inventory Inventory
    {
        get
        {
            return inventory;
        }
    }

    public Equipments Equipments
    {
        get
        {
            return equipments;
        }
    }

}
