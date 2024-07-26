using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftingUI : MonoBehaviour
{
    public CraftingManager craftingManager; // Reference to the crafting manager
    public Transform recipeContainer; // Container for recipe buttons
    public GameObject recipeButtonPrefab; // Prefab for individual recipe buttons
    public Text materialsText; // Text to show available materials
    public Text feedbackText; // Text to show feedback or errors

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        UpdateMaterialsText();
        UpdateRecipes();
    }

    void UpdateMaterialsText()
    {
        materialsText.text = "Materials: " + GetMaterialsString();
    }

    void UpdateRecipes()
    {
        // Clear existing recipe buttons
        foreach (Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }

        // Create a button for each recipe
        foreach (Recipe recipe in craftingManager.craftingRecipes)
        {
            GameObject buttonObject = Instantiate(recipeButtonPrefab, recipeContainer);
            Button recipeButton = buttonObject.GetComponent<Button>();
            Text buttonText = buttonObject.GetComponentInChildren<Text>();

            buttonText.text = recipe.result.itemName;

            recipeButton.onClick.AddListener(() => OnRecipeButtonClicked(recipe));
        }
    }

    void OnRecipeButtonClicked(Recipe recipe)
    {
        bool success = craftingManager.CraftItem(recipe);

        if (success)
        {
            feedbackText.text = "Crafted " + recipe.result.itemName + " successfully!";
        }
        else
        {
            feedbackText.text = "Not enough materials to craft " + recipe.result.itemName;
        }

        UpdateUI(); // Refresh the UI to show updated materials and recipes
    }

    string GetMaterialsString()
    {
        // Generate a string representation of materials
        // This example assumes CraftingManager has a list or dictionary of materials
        string result = "";
        foreach (var material in craftingManager.materials)
        {
            result += material.itemName + ": " + material.quantity + " ";
        }
        return result;
    }
}

