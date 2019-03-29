using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : ItemSlot
{
    public ItemType equipmentType;

    /// <summary>
    /// Changes name of the equipment slot according to its type
    /// and sets it empty.
    /// </summary>
    private void Awake()
    {
        gameObject.name = equipmentType.ToString() + " Slot";
        Item = null;
    }
}
