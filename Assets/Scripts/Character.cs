using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(AttributeManager))]
public class Character : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private AttributeManager attributeManager;
    [SerializeField] private ItemSaveManager itemSaveManager;

    private void Awake()
    {
        inventoryManager.ItemInfo.OnEquipButtonClickedEvent = Equip;
        inventoryManager.ItemInfo.OnUnequipButtonClickedEvent = Unequip;
    }

    /// <summary>
    /// Loads Inventory state stored in the last session.
    /// </summary>
    private void Start()
    {
        itemSaveManager.LoadInventory(this);
        itemSaveManager.LoadEquipments(this);
    }

    /// <summary>
    /// Saves Inventory state before exiting.
    /// </summary>
    private void OnDestroy()
    {
        itemSaveManager.SaveInventory(this);
        itemSaveManager.SaveEquipments(this);
    }

    private void OnValidate()
    {
        if (inventoryManager == null) inventoryManager = GetComponent<InventoryManager>();
        if (attributeManager == null) attributeManager = GetComponent<AttributeManager>();

    }

    /// <summary>
    /// Equip Item and apply status effects
    /// </summary>
    /// <param name="item"></param>
    public void Equip(Item item)
    {
        Item lastItem;
        if (inventoryManager.Equip(item, out lastItem))
        {
            if (lastItem != null) attributeManager.RemoveStatusEffects(lastItem);
            attributeManager.ApplyStatusEffects(item);
        }
    }

    /// <summary>
    /// Unequip Item and remove status effects
    /// </summary>
    /// <param name="item"></param>
    public void Unequip(Item item)
    {
        if (inventoryManager.Unequip(item)) attributeManager.RemoveStatusEffects(item);
    }

    public InventoryManager InventoryManager
    {
        get
        {
            return inventoryManager;
        }
    }
}
