using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

    public List<string> itemIDs = new List<string>();
    public List<int> itemAmounts = new List<int>();
    public List<BuildingSaveState> buildings = new List<BuildingSaveState>();

	// Use this for initialization
	void Start () {
        GameDatabase myDatabase = DatabaseManager.Database;
        foreach (var item in myDatabase[ItemType.Commodity])
        {
            itemIDs.Add(item.itemID);
            itemAmounts.Add(0);
        }
	}
	
	public int GetItemQuantity(string itemID)
    {
        return itemAmounts[itemIDs.IndexOf(itemID)];
    }

    public int GetItemQuantity(Commodity item)
    {
        return itemAmounts[itemIDs.IndexOf(item.itemID)];
    }

    public void SetItemQuantity(string itemID, int setAmount)
    {
        itemAmounts[itemIDs.IndexOf(itemID)] = setAmount;
    }

    public void SetItemQuantity(Commodity item, int setAmount)
    {
        itemAmounts[itemIDs.IndexOf(item.itemID)] = setAmount;
    }

    public void ModItemQuantity(string itemID, int modAmount)
    {
        int i = itemIDs.IndexOf(itemID);
        int newAmount = itemAmounts[i] + modAmount;
        itemAmounts[i] = newAmount;
    }

    public void ModItemQuantity(Commodity item, int modAmount)
    {
        int i = itemIDs.IndexOf(item.itemID);
        int newAmount = itemAmounts[i] + modAmount;
        itemAmounts[i] = newAmount;
    }

    public void Test()
    {
        ModItemQuantity("metal", 500);
        GameObject.Find("InventoryPanel").GetComponent<InventoryPanelManager>().UpdateMenu();
    }
}
