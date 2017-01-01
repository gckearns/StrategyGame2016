using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEditor;

public class BuildMenu : UIMenu {

    private List<Building> listedBuildings = new List<Building>();

    public GameObject buildMenu;
    public ModalPanel BuildPanel;

    public void PopulateMenu(int bldgCategory)
    {
        ClearMenu();
        GameDatabase myDatabase = DatabaseManager.Database;
        GameItemList buildings = myDatabase[ItemType.Building];
        GameItem[] bldgs = buildings.gameItems.ToArray();
        int count = 0;
        for (int i = 0; i < bldgs.Length; i++)
        {
            Building bldg = (Building)bldgs[i];
            if ((int)bldg.category == (bldgCategory + 1))
            {
                listedBuildings.Add(bldg);
                ModalPanel modalPanel = Instantiate<ModalPanel>(BuildPanel);
                modalPanel.transform.SetParent(content);
                modalPanel.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -254 * count, 0);
                modalPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 254);
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(bldg.iconPath);
                modalPanel.BuildDialogue(GetBuildingDialogueInfo(bldg).GetStrings(),
                    sprite, GetBuildAction(count), GetCancelAction());
                count++;
            }
            content.sizeDelta = new Vector2(0, 254 * count + 1);
            gameObject.SetActive(true);
        }
    }

    UnityAction GetBuildAction(int building)
    {
        return delegate { OnBuildClicked(building); };
    }

    UnityAction GetCancelAction()
    {
        return delegate { OnCancelClicked(); };
    }

    public void OnBuildClicked(int id)
    {
        //print (id); // Close the UI Menu
        TileManager t = (TileManager)Component.FindObjectOfType(typeof(TileManager));
        t.OnBuildClicked(listedBuildings[id]);
        listedBuildings = new List<Building>();
        gameObject.SetActive(false);
    }

    public void OnCancelClicked()
    {
        listedBuildings = new List<Building>();
        gameObject.SetActive(false);
    }

    public DialogueTextArray GetBuildingDialogueInfo(Building bldg)
    {
        string name = bldg.itemName;
        string desc = bldg.description;
        int size = bldg.size;
        int pwr = bldg.power;
        int jobs = bldg.workers;
        string details = "Size: " + size + "x" + size + "  Pwr: " + pwr + "  Jobs: " + jobs;
        string yTime = "Yield time: " + bldg.cycleTime;
        string[] yTypes = bldg.yieldItems.ToArray();
        int[] yNums = bldg.yieldAmounts.ToArray();
        string yieldString = "Yields: ";
        for (int i = 0; i < yTypes.Length; i++)
        {
            yieldString += yTypes[i] + ": " + yNums[i] + " ";
        }
        string[] costTypes = bldg.costItems.ToArray();
        int[] costNums = bldg.costAmounts.ToArray();
        string costString = "Requires: ";
        for (int i = 0; i < costTypes.Length; i++)
        {
            costString += "(" + costNums[i] + " " + costTypes[i] + ") ";
        }
        return new DialogueTextArray(name, desc, details, yTime, yieldString, costString, "Have: (99 somthing)");
    }
}
