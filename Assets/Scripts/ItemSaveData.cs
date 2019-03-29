using System;

/// <summary>
/// Used for saving ItemIds.
/// </summary>
[Serializable]
public class ItemSlotSaveData
{
    public string ItemId;

    public ItemSlotSaveData(string itemId)
    {
        this.ItemId = itemId;
    }
}

/// <summary>
/// Creates a list of item ids to be saved.
/// </summary>
[Serializable]
public class ItemContainerSaveData
{
    public ItemSlotSaveData[] SavedSlots;

    public ItemContainerSaveData(int numItems)
    {
        this.SavedSlots = new ItemSlotSaveData[numItems];
    }
}