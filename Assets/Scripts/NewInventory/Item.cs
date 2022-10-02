using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Item/Create New Item")]
//can create an item object in unity
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public int amount;
    public Sprite icon;
    public itemType type;

    public enum itemType
    {
        Building,
        Crop,
        Seed,
    }
}
