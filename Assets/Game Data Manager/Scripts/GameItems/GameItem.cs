using UnityEditor;
using UnityEngine;

[System.Serializable]
public abstract class GameItem {

	public ItemType itemType { get; set; }
	public string itemName { get; set; }
	public string itemID { get; set; }
	public abstract string catName { get;}
	public string description { get; set; }
    public string iconPath = "";

    public virtual void OnGUI() {
		itemName = EditorGUILayout.TextField("Name",itemName);
		EditorGUILayout.LabelField("ID", itemID);
        IconGUI();
    }

    private void IconGUI()
    {
        Sprite obj = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
        EditorGUI.BeginChangeCheck();
        obj = (Sprite)EditorGUILayout.ObjectField("Icon", obj, typeof(Sprite), false, GUILayout.MaxWidth(208));
        bool iconChanged = EditorGUI.EndChangeCheck();
        if (iconChanged)
        {
            if (obj == null)
            {
                iconPath = "";
            }
            else
            {
                iconPath = AssetDatabase.GetAssetPath(obj);
            }
        }
    }
}
