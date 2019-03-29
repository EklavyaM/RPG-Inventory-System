using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image icon;
    [SerializeField] protected Image hoveringIcon;

    private Item item;

    public Action<Item> OnRightClickEvent;

    private void Start()
    {
        if (hoveringIcon != null) hoveringIcon.enabled = false;
    }

    private void OnValidate()
    {
        if (icon == null) icon = GetComponent<Image>();
        hoveringIcon = GetComponentsInChildren<Image>()[1];
    }

    public Item Item
    {
        get { return item; }
        set
        {
            item = value;
            if (item == null)
            {
                icon.enabled = false;
            }
            else
            {
                icon.sprite = item.icon;
                icon.enabled = true;
            }
        }
    }

    /// <summary>
    /// When clicked ShowItemInfo in ItemInfo class will be called.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData != null && pointerEventData.button != PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(Item);
            }
        }
    }

    /// <summary>
    /// Displays the hovering icon.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        hoveringIcon.enabled = true;
    }

    /// <summary>
    /// Hides the hovering icon.
    /// </summary>
    /// <param name="pointerEventData"></param>
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        hoveringIcon.enabled = false;
    }
}
