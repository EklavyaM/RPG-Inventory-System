using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;
    [SerializeField] private Text itemDamage;
    [SerializeField] private Text itemDefense;
    [SerializeField] private Text itemStrength;
    [SerializeField] private Text itemIntelligence;
    [SerializeField] private Text itemAgility;

    [SerializeField] private Image itemImage;
    [SerializeField] private Button itemEquipButton;

    public Action<Item> OnEquipButtonClickedEvent;
    public Action<Item> OnUnequipButtonClickedEvent;

    private Item selectedItem;

    private void Awake()
    {
        itemEquipButton.onClick.AddListener(EquipButtonListener);
        gameObject.SetActive(false);
    }

    private void EquipButtonListener()
    {
        if (selectedItem == null) return;

        if (selectedItem.isEquipped && OnUnequipButtonClickedEvent != null) OnUnequipButtonClickedEvent(selectedItem);
        else if (!selectedItem.isEquipped && OnEquipButtonClickedEvent != null) OnEquipButtonClickedEvent(selectedItem);

        HideItemInfo();
    }

    public void ShowItemInfo(Item item)
    {
        selectedItem = item;

        itemName.text = item.itemName;
        itemDescription.text = item.description;
        itemDamage.text = item.damage.ToString();
        itemDefense.text = item.defence.ToString();
        itemStrength.text = item.strength.ToString();
        itemIntelligence.text = item.intel.ToString();
        itemAgility.text = item.agility.ToString();
        itemImage.sprite = item.icon;

        if (!item.isEquipped) itemEquipButton.GetComponentInChildren<Text>().text = "Equip";
        else itemEquipButton.GetComponentInChildren<Text>().text = "Unequip";

        gameObject.SetActive(true);
    }

    public void HideItemInfo()
    {
        gameObject.SetActive(false);
    }

}
