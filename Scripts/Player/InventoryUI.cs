using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
	public Inventory playerInventory;
	public GameObject itemSlotPrefab; // Prefab for an item slot
	public Transform itemSlotContainer; // Container where item slots will be added

	void Start()
	{
		UpdateInventoryUI();
	}

	public void UpdateInventoryUI()
	{
		// Clear existing slots
		foreach (Transform child in itemSlotContainer)
		{
			Destroy(child.gameObject);
		}

		// Create a new slot for each item in the inventory
		foreach (Item item in playerInventory.items)
		{
			GameObject slot = Instantiate(itemSlotPrefab, itemSlotContainer);
			Image itemIcon = slot.transform.Find("ItemIcon").GetComponent<Image>();
			Text itemName = slot.transform.Find("ItemName").GetComponent<Text>();

			itemIcon.sprite = item.itemIcon;
			itemName.text = item.itemName;
		}
	}
}
