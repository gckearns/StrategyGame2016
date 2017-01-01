using UnityEngine;
using System.Collections;
using UnityEditor;
using MyNamespace;
using System;
using System.Collections.Generic;

public class GameItemSelectWindow : EditorWindow
{
	//[MenuItem("Manager/Selection Window")]
	public static void Init()
	{
		GameItemSelectWindow window = GetWindow<GameItemSelectWindow>();
		window.Show();
	}

	GUISkin gSkin;

	private int toolSelected;
	private int itemSelected;
	private GameDatabase myDatabase = DatabaseManager.Database;
	private GameItemList selectedDatabase;
	private GameItem selectedGameData;
    public static bool isOpen = false;

    public EditorWindow parentWindow;
    public GameItem modifiedItem;
    public EditorCategory modifiedCategory;
    public string modifiedID = "";

    void OnEnable()
	{
		Debug.Log("ManagerWindow script OnEnable");
		minSize = new Vector2(544, 256);
		gSkin = Resources.Load("gskin") as GUISkin;
	}

	void OnDisable()
	{
		DatabaseManager.SaveDatabase();
        isOpen = false;
    }

	private void LoadDatabase()
	{
		selectedDatabase = myDatabase[GameUtility.ItemEnums[selectedTool]];
		Debug.Log("Loaded selected database: " + selectedDatabase.ToString());
		EditorGUIUtility.keyboardControl = 0;
	}

    void Validate()
    {
        if (modifiedItem.itemType == ItemType.Building)
        {
            Building bldg = (Building)modifiedItem;
            List<string> modifiedItems = null;
            List<int> modifiedAmounts = null;

            if (modifiedCategory == EditorCategory.Cost)
            {
                modifiedItems = bldg.costItems;
                modifiedAmounts = bldg.costAmounts;
            } else if (modifiedCategory == EditorCategory.Yield)
            {
                modifiedItems = bldg.yieldItems;
                modifiedAmounts = bldg.yieldAmounts;
            } else if (modifiedCategory == EditorCategory.Storage)
            {
                modifiedItems = bldg.storageTypes;
                modifiedAmounts = bldg.storageLimits;
            }

            if (!modifiedItems.Contains(selectedGameData.itemID))
            {
                modifiedItems.Add(selectedGameData.itemID);
                modifiedAmounts.Add(5);

                parentWindow.Focus();
                Close();
            }
            else
            {
                RemoveNotification();
                ShowNotification(new GUIContent("Duplicate item!"));
            }
        }
        if (modifiedItem.itemType == ItemType.Ship)
        {
            Ship ship = (Ship)modifiedItem;
            List<string> modifiedItems = null;
            List<int> modifiedAmounts = null;

            if (modifiedCategory == EditorCategory.Cost)
            {
                modifiedItems = ship.costItems;
                modifiedAmounts = ship.costAmounts;
            } else if (modifiedCategory == EditorCategory.Yield)
            {
                modifiedItems = ship.yieldItems;
                modifiedAmounts = ship.yieldAmounts;
            } else if (modifiedCategory == EditorCategory.Storage)
            {
                modifiedItems = ship.storageTypes;
                modifiedAmounts = ship.storageLimits;
            }

            if (!modifiedItems.Contains(selectedGameData.itemID))
            {
                modifiedItems.Add(selectedGameData.itemID);
                modifiedAmounts.Add(5);

                parentWindow.Focus();
                Close();
            }
            else
            {
                RemoveNotification();
                ShowNotification(new GUIContent("Duplicate item!"));
            }
        }
    }

    void OnGUI()
	{
        isOpen = true;
        GUI.skin = gSkin;
		MyTabs();
		MyDataview();
		MyToolbar();
	}

	void MyToolbar()
	{
		EditorGUILayout.BeginHorizontal(GUILayout.Height(32));
		if (GUILayout.Button("OK", GUILayout.Height(32))) {
            Validate();
        }
		if (GUILayout.Button("Cancel", GUILayout.Height(32))) {
            Close ();
		}
		EditorGUILayout.EndHorizontal();
	}

	private int selectedTool = -1;

	void MyTabs()
	{
		EditorGUILayout.BeginHorizontal(GUILayout.Height(32), GUILayout.Width(192));
		EditorGUI.BeginChangeCheck();
		selectedTool = MyGUILayout.ToggleBar(selectedTool, GameUtility.ItemTypeStrings);
		if (EditorGUI.EndChangeCheck())
			LoadDatabase();
		EditorGUILayout.EndHorizontal();
	}

	private string leftSearch;
	private string rightSearch;
	private Vector2 leftScroll;
	private Vector2 rightScroll;
	private int mySelection;

	void MyDataview()
	{
		GUIStyle myButton = new GUIStyle(GUI.skin.button);
		myButton.normal.background = null;
		GUIStyle leftVerticalStyle = new GUIStyle(GUI.skin.scrollView);
		leftVerticalStyle.margin = new RectOffset(0, 4, 0, 2);
		GUIStyle rightVerticalStyle = new GUIStyle(GUI.skin.scrollView);
		rightVerticalStyle.margin = new RectOffset(4, 0, 0, 2);
		GUIStyle scrollStyle = new GUIStyle(GUI.skin.scrollView);
		scrollStyle.normal.background = Resources.Load<Texture2D>("ColdSteelTexture");
		selectedGameData = null;
		Rect rect = EditorGUILayout.BeginVertical(leftVerticalStyle, GUILayout.ExpandWidth(true));
		{ // Begin Left Pane
			EditorGUI.DrawRect(rect, Color.white);
			leftSearch = EditorGUILayout.TextField("Search", leftSearch, "SearchField");
			rect = EditorGUILayout.BeginHorizontal(GUILayout.Height(16));
			{
				GUILayout.Button("Name", myButton, GUILayout.MinWidth(96), GUILayout.Height(16));
				GUILayout.Button("ID", myButton, GUILayout.MinWidth(48), GUILayout.Height(16));
				GUILayout.Button("Category", myButton, GUILayout.MinWidth(64), GUILayout.Height(16));
			}
			EditorGUILayout.EndHorizontal();
			leftScroll = EditorGUILayout.BeginScrollView(leftScroll, scrollStyle);
			{
				EditorGUILayout.BeginHorizontal(GUILayout.Height(16));
				{
					if (selectedDatabase != null) {
						if (selectedDatabase.Count > 0) {
                            if (modifiedID != "")
                            {
                                mySelection = selectedDatabase.myIDs.IndexOf(modifiedID);
                                modifiedID = "";
                            }
                            if (mySelection >= selectedDatabase.Count) {
								mySelection = (selectedDatabase.Count - 1);
							}
							if (mySelection < 0) {
								mySelection = 0;
							}
                            EditorGUI.BeginChangeCheck();
							mySelection = GUILayout.SelectionGrid(mySelection, selectedDatabase.objectNames, 1, "SelectionButton", GUILayout.MinWidth(96));
							mySelection = GUILayout.SelectionGrid(mySelection, selectedDatabase.objectIDs, 1, "SelectionButton", GUILayout.MinWidth(64));
							mySelection = GUILayout.SelectionGrid(mySelection, selectedDatabase.objectEnums, 1, "SelectionButton", GUILayout.MinWidth(64));
							if (EditorGUI.EndChangeCheck()) {
								EditorGUIUtility.keyboardControl = 0;
							}
							selectedGameData = selectedDatabase[mySelection];
						}
					}
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndScrollView();
		} // End Left Pane
		EditorGUILayout.EndVertical(); 
	}
}
