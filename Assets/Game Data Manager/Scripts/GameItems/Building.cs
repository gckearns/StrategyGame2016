using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building : GameItem {
	public BuildingCategory category { get; set; }
	public int size { get; set; }
    public int power { get; set; }
    public int workers { get; set; }
	public override string catName { get { return category.ToString(); } }
    public int constructionTime { get; set; }
	public List<string> costItems = new List<string>();
	public List<int> costAmounts = new List<int>();
    public List<string> yieldItems = new List<string>();
    public List<int> yieldAmounts = new List<int>();
    public int cycleTime = 0;
    public List<string> storageTypes = new List<string>();
    public List<int> storageLimits = new List<int>();
    public string prefabPath = "";
    
    public Building (ItemType type, string name, string ID) {
		this.itemType = type;
		this.itemName = name;
		this.itemID = ID;
		category = BuildingCategory.None;
    }

    public override void OnGUI()
	{
		base.OnGUI();
        PrefabGUI();
        category = (BuildingCategory) EditorGUILayout.EnumPopup("Category", category);
		size = EditorGUILayout.IntSlider("Size", size, 1, 3);
        power = EditorGUILayout.IntField("Power", power);
        workers = EditorGUILayout.IntField("Workers", workers);
        constructionTime = EditorGUILayout.IntSlider("Construct Time", constructionTime, 0, 3600);
        cycleTime = EditorGUILayout.IntField("Cycle Time", cycleTime);
    }

    private void PrefabGUI()
    {
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        EditorGUI.BeginChangeCheck();
        obj = (GameObject)EditorGUILayout.ObjectField("Prefab",obj, typeof(GameObject), false, GUILayout.MinWidth(256));
        bool prefabChanged = EditorGUI.EndChangeCheck();
        if (prefabChanged) {
            if (obj == null)
            {
                prefabPath = "";
            } else
            {
                prefabPath = AssetDatabase.GetAssetPath(obj);
            }
        }
    }

    public override string ToString()
	{
		return string.Format("[Building: name={0}, id={1}, category={2}, size={3}]", itemName, itemID, category, size);
	}
}
