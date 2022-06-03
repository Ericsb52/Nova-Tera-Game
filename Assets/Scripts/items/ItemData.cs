using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Resource,
    Equipable,
    Consumable
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



}
