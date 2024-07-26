using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
	public List<Item> items = new List<Item>();

	public void AddItem(Item item)
	{
		items.Add(item);
		Debug.Log("Item added to inventory: " + item.itemName);
	}
}
