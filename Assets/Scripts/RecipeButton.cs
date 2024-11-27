using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeButton : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI recipeName;
    public TextMeshProUGUI materialsText;
    public Button craftButton;

    private CraftingRecipe recipe;
    private BuildingCrafter crafter;
    private PlayerInventory playerInventory;

    public void Setup(CraftingRecipe recipe , BuildingCrafter crafter)
    {
        this.recipe = recipe;
        this.crafter = crafter;
        playerInventory = FindObjectOfType<PlayerInventory>();

        recipeName.text = recipe.itemName;
        UpdateMaterialsText();

        craftButton.onClick.AddListener(OnCraftButtonClicked);
    }

    private void UpdateMaterialsText()
    {
        string materials = "필요 재료 : Wn";
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            ItemType item = recipe.requiredItems[i];
            int required = recipe.requiredAmounts[i];
            int has = playerInventory.GetItemCount(item);
            materials += $"{item}: {has}/{required}Wn";
        }
        materialsText.text = materials;
    }

    private void OnCraftButtonClicked()
    {
        crafter.Trycraft(recipe, playerInventory);
        UpdateMaterialsText();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
