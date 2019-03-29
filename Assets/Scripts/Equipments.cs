using System;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    [SerializeField] private Transform equipmentSlotsParent;
    [SerializeField] private EquipmentSlot[] equipmentSlots;

    public Action<Item> OnItemRightClickedEvent;

    private void Start()
    {
        foreach (EquipmentSlot slot in equipmentSlots) slot.OnRightClickEvent += OnItemRightClickedEvent;
    }

    private void OnValidate()
    {
        if (equipmentSlotsParent != null) equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    /// <summary>
    /// Adds item to corresponding equipment slot.
    /// Takes an out parameter to return the previous item
    /// stored in the equipment slot.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="previousItem"></param>
    /// <returns></returns>
    public bool AddItem(Item item, out Item previousItem)
    {
        foreach (EquipmentSlot equipmentSlot in equipmentSlots)
        {
            if (equipmentSlot.equipmentType == item.itemType)
            {
                previousItem = equipmentSlot.Item;
                equipmentSlot.Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    /// <summary>
    /// Removes Item from equipment slot.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool RemoveItem(Item item)
    {
        foreach (EquipmentSlot equipmentSlot in equipmentSlots)
        {
            if (equipmentSlot.Item == item)
            {
                equipmentSlot.Item = null;
                return true;
            }
        }
        return false;
    }

    public EquipmentSlot[] EquipmentSlots
    {
        get
        {
            return equipmentSlots;
        }
    }
}
