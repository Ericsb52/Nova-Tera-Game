using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingRecipeUI : MonoBehaviour
{

    public CraftingRecipe recipe;
    public Image backgroundImg;
    public Image icon;
    public TextMeshProUGUI itemName;
    public Image[] resourceCosts;

    public Color canCraftColor;
    public Color cantCraftColor;

    private bool canCraft;

    private void Start()
    {
        icon.sprite = recipe.item_to_Craft.icon;
        itemName.text = recipe.item_to_Craft.displayname;

        for(int i = 0;i<resourceCosts.Length; i++)
        {
            if(i < recipe.cost.Length)
            {
                resourceCosts[i].gameObject.SetActive(true);
                resourceCosts[i].sprite = recipe.cost[i].item.icon;
                resourceCosts[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = recipe.cost[i].quantity.ToString();


            }
            else
            {
                resourceCosts[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        updateCanCraft();
    }
    public void updateCanCraft()
    {
        canCraft = true;
        backgroundImg.color = canCraftColor;
        for (int i = 0; i < recipe.cost.Length; i++)
        {
            if (!Inventory.instance.hasItem(recipe.cost[i].item, recipe.cost[i].quantity))
            {
                canCraft = false;
                backgroundImg.color = cantCraftColor;
                break;
            }

        }
    }

    public void onClickButton()
    {
        if (canCraft)
        {
            Crafting_Window.instance.craft(recipe);
        }
    }

}
