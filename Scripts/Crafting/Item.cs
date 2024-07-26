using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType; // Weapon, Armor, Tool, etc.
    public int itemID;
    public ItemProperties properties; // Unique properties for the item
}

public enum ItemType
{
    Weapon,
    Armor,
    Tool,
    Consumable
}

[System.Serializable]
public class ItemProperties
{
    public float damage;
    public float defense;
    public float durability;
    // Add other properties as needed
}
