using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SortingStyle
{
    ByType = 1,
    ByRarity = 2,
    ByName = 3
}

public class SortingListManager : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;

    public Action<SortingStyle> OnInventorySortPressed;

    private List<string> options = new List<string>() { "Sort Inventory", "Sort by Type", "Sort by Rarity", "Sort by Name" };

    private void Start()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        dropdown.onValueChanged.AddListener(delegate { OnDropDownValueChanged(dropdown); });
    }

    /// <summary>
    /// Sorts inventory based on the SortStyle chosen.
    /// </summary>
    /// <param name="dropdown"></param>
    private void OnDropDownValueChanged(Dropdown dropdown)
    {
        if (dropdown.value == 0 || OnInventorySortPressed == null) return;
        switch (dropdown.value)
        {
            case 1:
                OnInventorySortPressed(SortingStyle.ByType);
                break;
            case 2:
                OnInventorySortPressed(SortingStyle.ByRarity);
                break;
            case 3:
                OnInventorySortPressed(SortingStyle.ByName);
                break;
        }
        dropdown.value = 0;
    }

    private void OnValidate()
    {
        if (dropdown == null) dropdown = GetComponent<Dropdown>();
    }

    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveAllListeners();
    }
}
