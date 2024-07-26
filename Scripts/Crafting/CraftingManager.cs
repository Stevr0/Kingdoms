using UnityEngine;
using System.Collections.Generic;

public class CraftingManager : MonoBehaviour
{
    public List<Material> availableMaterials = new List<Material>(); // Player's current materials
    public List<Recipe> craftingRecipes = new List<Recipe>(); // List of available recipes
    public Inventory playerInventory; // Player's inventory for crafted items

    public void CraftItem(Recipe recipe)
    {
        // Check if the player has enough materials for the recipe
        if (CanCraft(recipe))
        {
            // Deduct materials
            foreach (var material in recipe.requiredMaterials)
            {
                DeductMaterial(material);
            }

            // Create item with random properties
            Item newItem = CreateCraftedItem(recipe);

            // Add item to player's inventory
            playerInventory.AddItem(newItem);

            Debug.Log("Item crafted: " + newItem.itemName);
        }
        else
        {
            Debug.Log("Not enough materials to craft " + recipe.result.itemName);
        }
    }

    private bool CanCraft(Recipe recipe)
    {
        foreach (var material in recipe.requiredMaterials)
        {
            if (!HasMaterial(material))
                return false;
        }
        return true;
    }

    private bool HasMaterial(Material material)
    {
        foreach (var playerMaterial in availableMaterials)
        {
            if (playerMaterial.materialName == material.materialName && playerMaterial.quantity >= material.quantity)
                return true;
        }
        return false;
    }

    private void DeductMaterial(Material material)
    {
        foreach (var playerMaterial in availableMaterials)
        {
            if (playerMaterial.materialName == material.materialName)
            {
                playerMaterial.quantity -= material.quantity;
                return;
            }
        }
    }

    private Item CreateCraftedItem(Recipe recipe)
    {
        Item craftedItem = recipe.result;
        craftedItem.properties = GenerateRandomProperties();
        return craftedItem;
    }

    private ItemProperties GenerateRandomProperties()
    {
        ItemProperties properties = new ItemProperties
        {
            damage = Random.Range(5f, 20f),
            defense = Random.Range(5f, 20f),
            durability = Random.Range(50f, 100f)
        };
        return properties;
    }
}

[System.Serializable]
public class Recipe
{
    public Item result;
    public List<Material> requiredMaterials;
}
