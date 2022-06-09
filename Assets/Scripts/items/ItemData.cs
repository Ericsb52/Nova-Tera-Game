using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Hunger,
    Thirst,
    Health,
    Sleep
}


[CreateAssetMenu(fileName ="Item",menuName =" new Item")]
public class ItemData : ScriptableObject
{
    [Header("info")]
    public string displayname;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;


    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip Prefab")]
    public GameObject equipPrefab;

}
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}
