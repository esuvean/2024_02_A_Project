using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    craftingTable,
    Funace,
    Kitchen,
    Storage
}

[System.Serializable]
public class CraftingRecipe
{
    public string itemName;
    public ItemType resultItem;
    public int resultAmount = 1;

    public float hungerRestoreAmount;
    public float repairAmount;

    public ItemType[] requiredItems;
    public int[] requiredAmounts;
}