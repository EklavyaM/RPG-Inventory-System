using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSaveManager : MonoBehaviour
{
    [SerializeField] private ItemConfig itemConfig;

    private const string InventoryFileName = "Inventory";
    private const string EquipmentFileName = "Equipment";

    /// <summary>
    /// Saves data of item slots in the given filename.
    /// </summary>
    /// <param name="itemSlots"></param>
    /// <param name="fileName"></param>
    private void SaveItems(IList<ItemSlot> itemSlots, string fileName)
    {
        var saveData = new ItemContainerSaveData(itemSlots.Count);
        for (int i = 0; i < itemSlots.Count; i++)
        {
            ItemSlot slot = itemSlots[i];
            if (slot.Item == null)
            {
                saveData.SavedSlots[i] = null;
            }
            else
            {
                saveData.SavedSlots[i] = new ItemSlotSaveData(slot.Item.Id);
            }
        }
        ItemSaveIO.SaveItems(saveData, fileName);
    }

    /// <summary>
    /// Saves the state of the inventory.
    /// </summary>
    /// <param name="character"></param>
    public void SaveInventory(Character character)
    {
        SaveItems(character.InventoryManager.Inventory.ItemSlots, InventoryFileName);
    }

    /// <summary>
    /// Saves the state of the equipments.
    /// </summary>
    /// <param name="character"></param>
    public void SaveEquipments(Character character)
    {
        SaveItems(character.InventoryManager.Equipments.EquipmentSlots, EquipmentFileName);
    }

    /// <summary>
    /// Loads state of the inventory.
    /// </summary>
    /// <param name="character"></param>
    public void LoadInventory(Character character)
    {
        ItemContainerSaveData savedInventorySlots = ItemSaveIO.LoadItems(InventoryFileName);
        if (savedInventorySlots != null)
        {
            character.InventoryManager.Inventory.Clear();
            for (int i = 0; i < savedInventorySlots.SavedSlots.Length; i++)
            {
                ItemSlot itemSlot = character.InventoryManager.Inventory.ItemSlots[i];
                ItemSlotSaveData savedSlot = savedInventorySlots.SavedSlots[i];

                itemSlot.Item = (savedSlot != null) ? itemConfig.GetItemClone(savedSlot.ItemId) : null;
                if (itemSlot.Item != null) character.InventoryManager.Inventory.AddItem(itemSlot.Item);
            }
        }
    }

    /// <summary>
    /// Loads state of the equipments.
    /// </summary>
    /// <param name="character"></param>
    public void LoadEquipments(Character character)
    {
        ItemContainerSaveData savedEquipmentSlots = ItemSaveIO.LoadItems(EquipmentFileName);
        if (savedEquipmentSlots == null) return;

        foreach (ItemSlotSaveData savedSlot in savedEquipmentSlots.SavedSlots)
        {
            if (savedSlot == null)
            {
                continue;
            }

            Item item = (savedSlot != null) ? itemConfig.GetItemClone(savedSlot.ItemId) : null;
            if (item != null)
            {
                character.InventoryManager.Inventory.AddItem(item);
                character.Equip(item);
            }
        }
    }
}
