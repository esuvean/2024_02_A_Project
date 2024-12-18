using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCrafter : MonoBehaviour
{
    public BuildingType buildingType;
    public CraftingRecipe[] recipes;
    public SurvivalStats survivalStats;
    public ConstructibleBuilding building;

    // Start is called before the first frame update
    void Start()
    {
        survivalStats = FindAnyObjectByType<SurvivalStats>();
        building = GetComponent<ConstructibleBuilding>();

        switch (buildingType)
        {
            case BuildingType.Kitchen:
                recipes = RecipeList.KitchenRecipes;   
                break; 
            case BuildingType.craftingTable:
                recipes = RecipeList.WorkbenchRecipes;
                break;
        }
    }
    public void Trycraft(CraftingRecipe recipe, PlayerInventory inventory)
    {
        if (!building.isConstructed)
        {
            FloatingTextMananger.instance?.Show("건설이 완료 되지 않았습니다.", transform.position + Vector3.up);
            return;
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)
        { 
            if(inventory.GetItemCount(recipe.requiredItems[i]) < recipe.requiredAmounts[i])
            {
                FloatingTextMananger.instance?.Show("재료가 부족합니다.", transform.position + Vector3.up);
                return;
            }
        }

        for(int i = 0; i < recipe.requiredItems.Length;i++)
        {
            inventory.RemoveItem(recipe.requiredItems[i] , recipe.requiredAmounts[i]);
        }

        survivalStats.DamageCrafting();

        inventory.AddItem(recipe.resultItem, recipe.resultAmount);
        FloatingTextMananger.instance?.Show($"{recipe.itemName}", transform.position + Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
