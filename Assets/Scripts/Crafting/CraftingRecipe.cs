using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Crafting Recipe", menuName ="New Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public ItemData item_to_Craft;
    public ResourceCost[] cost;
}

[System.Serializable]
public class ResourceCost
{
    public ItemData item;
    public int quantity;
}
