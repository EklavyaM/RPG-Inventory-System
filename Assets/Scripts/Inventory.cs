using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemConfig itemConfig;
    [SerializeField] private SortingListManager sortingListManager;

    [SerializeField] private Transform itemsParent;
    [SerializeField] private List<ItemSlot> itemSlots;

    private List<Item> items = new List<Item>();

    public Action<Item> OnItemRightClickedEvent;

    /// <summary>
    /// Retrieves items from the scriptable object ItemConfig and resets inventory.
    /// Also assigns the sorting manager's on sort action.
    /// </summary>
    private void Awake()
    {
        if (itemConfig != null)
        {
            items = new List<Item>(itemConfig.items.Select(item => item.GetClone()));
            ResetInventory();
        }
        sortingListManager.OnInventorySortPressed = SortBy;
    }

    /// <summary>
    /// Assigns each slot's onRightClickEvent to OnItemRightClickedEvent which will be assigned in the inventory manager.
    /// </summary>
    private void Start()
    {
        foreach (ItemSlot slot in itemSlots) slot.OnRightClickEvent += OnItemRightClickedEvent;
    }

    private void OnValidate()
    {
        if (itemsParent != null) itemSlots = new List<ItemSlot>(itemsParent.GetComponentsInChildren<ItemSlot>());
    }

    /// <summary>
    /// Sorts Inventory.
    /// </summary>
    /// <param name="style"></param>
    private void SortBy(SortingStyle style)
    {
        switch (style)
        {
            case SortingStyle.ByType:
                items.Sort((a, b) => a.itemType.CompareTo(b.itemType));
                break;
            case SortingStyle.ByRarity:
                items.Sort((a, b) => a.itemRarity.CompareTo(b.itemRarity));
                break;
            case SortingStyle.ByName:
                items.Sort((a, b) => a.itemName.CompareTo(b.itemName));
                break;
        }
        ResetInventory();
    }

    /// <summary>
    /// Resets Inventory.
    /// </summary>
    public void ResetInventory()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].Item = (i < items.Count) ? items[i] : null;
        }
    }

    /// <summary>
    /// Adds an item to the Inventory.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItem(Item item)
    {
        if (IsFull()) return false;

        items.Add(item);
        ResetInventory();
        return true;
    }

    /// <summary>
    /// Removes item passed from the Inventory.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            ResetInventory();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns true if there are more items than slots.
    /// </summary>
    /// <returns></returns>
    public bool IsFull()
    {
        return items.Count >= itemSlots.Count;
    }

    /// <summary>
    /// Clears items and Resets Inventory.
    /// </summary>
    public void Clear()
    {
        items.Clear();
        ResetInventory();
    }

    public List<ItemSlot> ItemSlots
    {
        get
        {
            return itemSlots;
        }
    }
}
