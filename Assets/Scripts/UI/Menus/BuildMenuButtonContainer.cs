using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using StrategyGame;

public class BuildMenuButtonContainer : MonoBehaviour {

    public GameObject menuButtonContainer;
    public ModalMenuButton BuildMenuItem;
    public BuildMenu buildMenu;

    private static BuildMenuButtonContainer menuButtons;
    private BuildingCategory[] categories = GameResources.BuildingCategories;
    private bool initialized = false;

    public static BuildMenuButtonContainer Instance () {
        if (!menuButtons) {
            menuButtons = FindObjectOfType(typeof (BuildMenuButtonContainer)) as BuildMenuButtonContainer;
            if (!menuButtons)
                Debug.LogError ("There needs to be one active BuildMenuButtonContainer script on a GameObject in your scene.");
        }

        return menuButtons;
    }

    UnityAction GetMenuAction (int menu) {
        return delegate{OnMenuClicked(menu);};
    }

    public void OnMenuClicked(int id){
        buildMenu.PopulateMenu (id);
    }

    void initialize () {
        for (int i = 0; i < categories.Length; i++) {
            ModalMenuButton modalMenuButton = Instantiate<ModalMenuButton> (BuildMenuItem);  
            modalMenuButton.menuButtonText.text = categories [i].ToString ();
            modalMenuButton.transform.SetParent (menuButtonContainer.transform);
            modalMenuButton.BuildMenus (categories[i].ToString (), GetMenuAction (i));
        }
        initialized = true;
    }

    public void Activate () {
        if (!initialized) {
            initialize ();
        }
        menuButtonContainer.SetActive (true);
    }
}
