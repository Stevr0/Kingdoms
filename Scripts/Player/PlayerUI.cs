using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;
    public Slider manaBar;
    public Text goldText;
    public Text materialsText;
    public Text foodText;
    public Text waterText;
    public CraftingUI craftingUI; // Reference to the crafting UI

    private Player player; // Reference to the player class

    void Start()
    {
        player = FindObjectOfType<Player>(); // Assuming there's a Player class
    }

    void Update()
    {
        UpdateHealth(player.CurrentHealth);
        UpdateStamina(player.CurrentStamina);
        UpdateMana(player.CurrentMana);
        UpdateGold(player.CurrentGold);
        UpdateMaterials(player.AvailableMaterials);
        UpdateFood(player.CurrentFood);
        UpdateWater(player.CurrentWater);
    }

    public void UpdateHealth(float health)
    {
        healthBar.value = health;
    }

    public void UpdateStamina(float stamina)
    {
        staminaBar.value = stamina;
    }

    public void UpdateMana(float mana)
    {
        manaBar.value = mana;
    }

    public void UpdateGold(int gold)
    {
        goldText.text = "Gold: " + gold;
    }

    public void UpdateMaterials(List<Material> materials)
    {
        materialsText.text = "Materials: " + GetMaterialsString(materials);
    }

    public void UpdateFood(float food)
    {
        foodText.text = "Food: " + food;
    }

    public void UpdateWater(float water)
    {
        waterText.text = "Water: " + water;
    }

    private string GetMaterialsString(List<Material> materials)
    {
        string result = "";
        foreach (Material material in materials)
        {
            result += material.materialName + ": " + material.quantity + " ";
        }
        return result;
    }
}
