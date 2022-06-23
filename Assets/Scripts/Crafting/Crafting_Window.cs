using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting_Window : MonoBehaviour
{
    public CraftingRecipeUI[] recipeUIs;
    public static Crafting_Window instance;


    private void Awake()
    {
        instance = this;
    }

    void onOpenInventory()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Inventory.instance.onOpenInventory.AddListener(onOpenInventory);
    }

    private void OnDisable()
    {
        Inventory.instance.onOpenInventory.RemoveListener(onOpenInventory);
    }

    public void craft(CraftingRecipe recipe)
    {
        for(int i =0;i< recipe.cost.Length; i++)
        {
            for (int x = 0; x < recipe.cost[i].quantity; x++)
            {
                Inventory.instance.removeItem(recipe.cost[i].item);
            }
        }

        Inventory.instance.addItem(recipe.item_to_Craft);

        for(int i = 0; i < recipeUIs.Length; i++)
        {
            recipeUIs[i].updateCanCraft();
        }
    }
}
