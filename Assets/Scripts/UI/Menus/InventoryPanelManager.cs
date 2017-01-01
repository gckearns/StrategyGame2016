using UnityEngine;
using System.Collections;
using UnityEditor;

public class InventoryPanelManager : UIMenu {
    public ModalItemPanel itemPanel;
    public GameObject inventoryGridPanel;
    GameObject player;
    PlayerInventory inventory;

    void Awake()
    {
        player = GameObject.Find("Player");
        inventory = player.GetComponent<PlayerInventory>();
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        PopulateMenu();
    }

    public void PopulateMenu()
    {
        ClearMenu();
        GameDatabase myDatabase = DatabaseManager.Database;
        GameItemList itemList = myDatabase[ItemType.Commodity];
        GameItem[] items = itemList.gameItems.ToArray();
        for (int i = 0; i < items.Length; i++)
        {
            Commodity item = (Commodity)items[i];
            ModalItemPanel modalPanel = Instantiate(itemPanel);
            modalPanel.transform.SetParent(inventoryGridPanel.transform);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(items[i].iconPath);
            modalPanel.BuildDialogue(GetDialogueInfo(item),
                sprite,item.itemID);
            gameObject.SetActive(true);
        }
    }

    //void OnGUI()
    //{
    //    UpdateMenu();
    //}

    public void UpdateMenu()
    {
        ModalItemPanel[] panels = GetComponentsInChildren<ModalItemPanel>();
        foreach (var item in panels)
        {
            item.UpdateDialogueSubtext(inventory.GetItemQuantity(item.itemID).ToString());
        }
        Debug.Log("updated amounts");
    }

    public void OnCancelClicked()
    {
        gameObject.SetActive(false);
    }

    public string[] GetDialogueInfo(Commodity item)
    {
        return new string[] { item.itemName, inventory.GetItemQuantity(item).ToString()};
    }

    public override void ClearMenu()
    {
        for (int i = 0; i < inventoryGridPanel.transform.childCount; i++)
        {
            GameObject g = inventoryGridPanel.transform.GetChild(i).gameObject;
            g.SetActive(false);
        }
    }   
}
