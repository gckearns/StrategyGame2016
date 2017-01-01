using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEditor;

public class MainMenu : UIMenu {

    public GameObject mainMenu;
    public MainModalPanel menuPanel;
    public MainMenuItem[] menuItems;

    public void PopulateMenu()
    {
        ClearMenu();
        for (int i=0; i < menuItems.Length; i++)
        {
            MainModalPanel modalPanel = Instantiate(menuPanel);
            modalPanel.transform.SetParent(content);
            modalPanel.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -100 * i, 0);
            modalPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 100);
            modalPanel.BuildDialogue(GetDialogueInfo(menuItems[i]),
                menuItems[i].icon, GetMenuAction(i), GetCancelAction());
            content.sizeDelta = new Vector2(0, 100 * i + 1);
            gameObject.SetActive(true);
        }
    }

    UnityAction GetMenuAction(int menuItem)
    {
        return delegate { OnMenuClicked(menuItem); };
    }

    UnityAction GetCancelAction()
    {
        return delegate { OnCancelClicked(); };
    }

    public void OnMenuClicked(int id)
    {
        switch (id)
        {
            case 3:
                menuItems[id].menu.Activate();
                break;
            default:
                break;
        }
        print(id); // Close the UI Menu
        gameObject.SetActive(false);
    }

    public void OnCancelClicked()
    {
        gameObject.SetActive(false);
    }

    public string[] GetDialogueInfo(MainMenuItem item)
    {
        return new string[] {item.title,item.subTitle };
    }
}
