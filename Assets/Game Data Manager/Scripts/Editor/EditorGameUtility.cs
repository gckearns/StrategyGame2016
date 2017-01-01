using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class EditorGameUtility : MonoBehaviour {

    public static Building PrefabGUI(Building item)
    {
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(item.prefabPath);
        EditorGUI.BeginChangeCheck();
        //obj = (GameObject)EditorUtility.InstanceIDToObject(-94490);
        obj = (GameObject)EditorGUILayout.ObjectField("Prefab", obj, typeof(GameObject), false, GUILayout.MinWidth(256));
        //if (obj != null) Debug.Log(obj.GetInstanceID());
        //Debug.Log(AssetDatabase.GetAssetPath(obj.GetInstanceID()));
        bool prefabChanged = EditorGUI.EndChangeCheck();
        if (prefabChanged)
        {
            if (obj == null)
            {
                item.prefabPath = "";
            }
            else
            {
                item.prefabPath = AssetDatabase.GetAssetPath(obj);
            }
        }
        return item;
    }

    public static Ship PrefabGUI(Ship item)
    {
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(item.prefabPath);
        EditorGUI.BeginChangeCheck();
        //obj = (GameObject)EditorUtility.InstanceIDToObject(-94490);
        obj = (GameObject)EditorGUILayout.ObjectField("Prefab", obj, typeof(GameObject), false, GUILayout.MinWidth(256));
        //if (obj != null) Debug.Log(obj.GetInstanceID());
        //Debug.Log(AssetDatabase.GetAssetPath(obj.GetInstanceID()));
        bool prefabChanged = EditorGUI.EndChangeCheck();
        if (prefabChanged)
        {
            if (obj == null)
            {
                item.prefabPath = "";
            }
            else
            {
                item.prefabPath = AssetDatabase.GetAssetPath(obj);
            }
        }
        return item;
    }

    public static Building CostGUI(Building item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cost Items");
        EditorGUILayout.LabelField("Cost Amounts");
        if (GUILayout.Button("+"))
        {
            if (!GameItemSelectWindow.isOpen)
            {
                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                w.modifiedItem = item;
                w.modifiedCategory = EditorCategory.Cost;
                w.ShowUtility();
            }
        }
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < item.costItems.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.costItems[i]].itemName;
            if (GUILayout.Button(itemLabel))
            {
                if (!GameItemSelectWindow.isOpen)
                {
                    GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                    w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                    w.modifiedItem = item;
                    w.modifiedID = item.costItems[i];
                    w.modifiedCategory = EditorCategory.Cost;
                    w.ShowUtility();
                }
            }
            item.costAmounts[i] = EditorGUILayout.IntField(item.costAmounts[i]);
            if (GUILayout.Button("-"))
            {
                item.costItems.RemoveAt(i);
                item.costAmounts.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        return item;
    }

    public static Ship CostGUI(Ship item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Cost Items");
        EditorGUILayout.LabelField("Cost Amounts");
        if (GUILayout.Button("+"))
        {
            if (!GameItemSelectWindow.isOpen)
            {
                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                w.modifiedItem = item;
                w.modifiedCategory = EditorCategory.Cost;
                w.ShowUtility();
            }
        }
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < item.costItems.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.costItems[i]].itemName;
            if (GUILayout.Button(itemLabel))
            {
                if (!GameItemSelectWindow.isOpen)
                {
                    GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                    w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                    w.modifiedItem = item;
                    w.modifiedID = item.costItems[i];
                    w.modifiedCategory = EditorCategory.Cost;
                    w.ShowUtility();
                }
            }
            item.costAmounts[i] = EditorGUILayout.IntField(item.costAmounts[i]);
            if (GUILayout.Button("-"))
            {
                item.costItems.RemoveAt(i);
                item.costAmounts.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        return item;
    }

    public static Building YieldGUI(Building item)
    {
        if (item.yieldItems == null) item.yieldItems = new List<string>();
        if (item.yieldAmounts == null) item.yieldAmounts = new List<int>();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Yield Items");
        EditorGUILayout.LabelField("Yield Amounts");
        if (GUILayout.Button("+"))
        {
            if (!GameItemSelectWindow.isOpen)
            {
                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                w.modifiedItem = item;
                w.modifiedCategory = EditorCategory.Yield;
                w.ShowUtility();
            }
        }
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < item.yieldItems.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.yieldItems[i]].itemName;
            if (GUILayout.Button(itemLabel))
            {
                if (!GameItemSelectWindow.isOpen)
                {
                    GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                    w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                    w.modifiedItem = item;
                    w.modifiedID = item.yieldItems[i];
                    w.modifiedCategory = EditorCategory.Yield;
                    w.ShowUtility();
                }
            }
            item.yieldAmounts[i] = EditorGUILayout.IntField(item.yieldAmounts[i]);
            if (GUILayout.Button("-"))
            {
                item.yieldItems.RemoveAt(i);
                item.yieldAmounts.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        return item;
    }

    public static Ship YieldGUI(Ship item)
    {
        if (item.yieldItems == null) item.yieldItems = new List<string>();
        if (item.yieldAmounts == null) item.yieldAmounts = new List<int>();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Yield Items");
        EditorGUILayout.LabelField("Yield Amounts");
        if (GUILayout.Button("+"))
        {
            if (!GameItemSelectWindow.isOpen)
            {
                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                w.modifiedItem = item;
                w.modifiedCategory = EditorCategory.Yield;
                w.ShowUtility();
            }
        }
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < item.yieldItems.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.yieldItems[i]].itemName;
            if (GUILayout.Button(itemLabel))
            {
                if (!GameItemSelectWindow.isOpen)
                {
                    GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                    w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                    w.modifiedItem = item;
                    w.modifiedID = item.yieldItems[i];
                    w.modifiedCategory = EditorCategory.Yield;
                    w.ShowUtility();
                }
            }
            item.yieldAmounts[i] = EditorGUILayout.IntField(item.yieldAmounts[i]);
            if (GUILayout.Button("-"))
            {
                item.yieldItems.RemoveAt(i);
                item.yieldAmounts.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        return item;
    }

    public static Building StorageGUI(Building item)
    {
        if (item.storageTypes == null) item.storageTypes = new List<string>();
        if (item.storageLimits == null) item.storageLimits = new List<int>();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Storage Types");
        EditorGUILayout.LabelField("Storage Amounts");
        if (GUILayout.Button("+"))
        {
            if (!GameItemSelectWindow.isOpen)
            {
                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                w.modifiedItem = item;
                w.modifiedCategory = EditorCategory.Storage;
                w.ShowUtility();
            }
        }
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < item.storageTypes.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.storageTypes[i]].itemName;
            if (GUILayout.Button(itemLabel))
            {
                if (!GameItemSelectWindow.isOpen)
                {
                    GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                    w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                    w.modifiedItem = item;
                    w.modifiedID = item.yieldItems[i];
                    w.modifiedCategory = EditorCategory.Storage;
                    w.ShowUtility();
                }
            }
            item.storageLimits[i] = EditorGUILayout.IntField(item.storageLimits[i]);
            if (GUILayout.Button("-"))
            {
                item.storageTypes.RemoveAt(i);
                item.storageLimits.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        return item;
    }

    public static Ship StorageGUI(Ship item)
    {
        if (item.storageTypes == null) item.storageTypes = new List<string>();
        if (item.storageLimits == null) item.storageLimits = new List<int>();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Storage Types");
        EditorGUILayout.LabelField("Storage Amounts");
        if (GUILayout.Button("+"))
        {
            if (!GameItemSelectWindow.isOpen)
            {
                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                w.modifiedItem = item;
                w.modifiedCategory = EditorCategory.Storage;
                w.ShowUtility();
            }
        }
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < item.storageTypes.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.storageTypes[i]].itemName;
            if (GUILayout.Button(itemLabel))
            {
                if (!GameItemSelectWindow.isOpen)
                {
                    GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
                    w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
                    w.modifiedItem = item;
                    w.modifiedID = item.yieldItems[i];
                    w.modifiedCategory = EditorCategory.Storage;
                    w.ShowUtility();
                }
            }
            item.storageLimits[i] = EditorGUILayout.IntField(item.storageLimits[i]);
            if (GUILayout.Button("-"))
            {
                item.storageTypes.RemoveAt(i);
                item.storageLimits.RemoveAt(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        return item;
    }

    //public static ItemGUI GetItemGUI(ItemType type)
    //{
    //    switch (type)
    //    {
    //        case ItemType.Building:
    //            return new ItemGUI(BuildingGUI);
    //        //				break;
    //        case ItemType.Commodity:
    //            return new ItemGUI(CommodityGUI);
    //        //				break;
    //        case ItemType.Ship:
    //            return new ItemGUI(ShipGUI);
    //        default:
    //            return null;
    //    }
    //}

    //public static Building BuildingGUI(GameItem building)
    //{
    //    Building item = (Building) building;
    //    item.OnGUI();
    //    EditorGUILayout.BeginHorizontal();
    //    item = IconGUI(item);
    //    item = PrefabGUI(item);
    //    EditorGUILayout.EndHorizontal();
    //    item.category = (BuildingCategory)EditorGUILayout.EnumPopup("Category", item.category);
    //    item.size = EditorGUILayout.IntSlider("Size", item.size, 1, 3);
    //    item = CostGUI(item);
    //    item.cycleTime = EditorGUILayout.IntField("Cycle Time", item.cycleTime);
    //    item = YieldGUI(item);
    //    item = StorageGUI(item);
    //    return item;
    //}

    //public static Commodity CommodityGUI(GameItem commodity)
    //{
    //    Commodity item = (Commodity)commodity;
    //    item.OnGUI();
    //    item = IconGUI(item);
    //    item.category = (CommodityCategory)EditorGUILayout.EnumPopup("Category", item.category);
    //    item.price = EditorGUILayout.FloatField("Value", item.price);
    //    item.priceMin = EditorGUILayout.FloatField("Min Val", item.priceMin);
    //    item.priceMax = EditorGUILayout.FloatField("Max Val", item.priceMax);
    //    return item;
    //}

    //public static Ship ShipGUI(GameItem ship)
    //{
    //    Ship item = (Ship)ship;
    //    EditorGUILayout.BeginHorizontal();
    //    item = IconGUI(item);
    //    item = PrefabGUI(item);
    //    EditorGUILayout.EndHorizontal();
    //    item.size = EditorGUILayout.IntSlider("Size", item.size, 1, 3);
    //    item = CostGUI(item);
    //    item = YieldGUI(item);
    //    item = StorageGUI(item);
    //    return item;
    //}

    //private static Building PrefabGUI(Building item)
    //{
    //    GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(item.prefabPath);
    //    EditorGUI.BeginChangeCheck();
    //    //obj = (GameObject)EditorUtility.InstanceIDToObject(-94490);
    //    obj = (GameObject)EditorGUILayout.ObjectField("Prefab", obj, typeof(GameObject), false, GUILayout.MinWidth(256));
    //    //if (obj != null) Debug.Log(obj.GetInstanceID());
    //    //Debug.Log(AssetDatabase.GetAssetPath(obj.GetInstanceID()));
    //    bool prefabChanged = EditorGUI.EndChangeCheck();
    //    if (prefabChanged)
    //    {
    //        if (obj == null)
    //        {
    //            item.prefabPath = "";
    //        }
    //        else
    //        {
    //            item.prefabPath = AssetDatabase.GetAssetPath(obj);
    //        }
    //    }
    //    return item;
    //}

    //private static Ship PrefabGUI(Ship item)
    //{
    //    GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(item.prefabPath);
    //    EditorGUI.BeginChangeCheck();
    //    //obj = (GameObject)EditorUtility.InstanceIDToObject(-94490);
    //    obj = (GameObject)EditorGUILayout.ObjectField("Prefab", obj, typeof(GameObject), false, GUILayout.MinWidth(256));
    //    //if (obj != null) Debug.Log(obj.GetInstanceID());
    //    //Debug.Log(AssetDatabase.GetAssetPath(obj.GetInstanceID()));
    //    bool prefabChanged = EditorGUI.EndChangeCheck();
    //    if (prefabChanged)
    //    {
    //        if (obj == null)
    //        {
    //            item.prefabPath = "";
    //        }
    //        else
    //        {
    //            item.prefabPath = AssetDatabase.GetAssetPath(obj);
    //        }
    //    }
    //    return item;
    //}

    //private static Building CostGUI(Building item)
    //{
    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.LabelField("Cost Items");
    //    EditorGUILayout.LabelField("Cost Amounts");
    //    if (GUILayout.Button("+"))
    //    {
    //        if (!GameItemSelectWindow.isOpen)
    //        {
    //            GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //            w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //            w.modifiedItem = item;
    //            w.modifiedCategory = EditorCategory.Cost;
    //            w.ShowUtility();
    //        }
    //    }
    //    EditorGUILayout.EndHorizontal();
    //    for (int i = 0; i < item.costItems.Count; i++)
    //    {
    //        EditorGUILayout.BeginHorizontal();
    //        string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.costItems[i]].itemName;
    //        if (GUILayout.Button(itemLabel))
    //        {
    //            if (!GameItemSelectWindow.isOpen)
    //            {
    //                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //                w.modifiedItem = item;
    //                w.modifiedID = item.costItems[i];
    //                w.modifiedCategory = EditorCategory.Cost;
    //                w.ShowUtility();
    //            }
    //        }
    //        item.costAmounts[i] = EditorGUILayout.IntField(item.costAmounts[i]);
    //        if (GUILayout.Button("-"))
    //        {
    //            item.costItems.RemoveAt(i);
    //            item.costAmounts.RemoveAt(i);
    //        }
    //        EditorGUILayout.EndHorizontal();
    //    }
    //    return item;
    //}

    //private static Ship CostGUI(Ship item)
    //{
    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.LabelField("Cost Items");
    //    EditorGUILayout.LabelField("Cost Amounts");
    //    if (GUILayout.Button("+"))
    //    {
    //        if (!GameItemSelectWindow.isOpen)
    //        {
    //            GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //            w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //            w.modifiedItem = item;
    //            w.modifiedCategory = EditorCategory.Cost;
    //            w.ShowUtility();
    //        }
    //    }
    //    EditorGUILayout.EndHorizontal();
    //    for (int i = 0; i < item.costItems.Count; i++)
    //    {
    //        EditorGUILayout.BeginHorizontal();
    //        string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.costItems[i]].itemName;
    //        if (GUILayout.Button(itemLabel))
    //        {
    //            if (!GameItemSelectWindow.isOpen)
    //            {
    //                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //                w.modifiedItem = item;
    //                w.modifiedID = item.costItems[i];
    //                w.modifiedCategory = EditorCategory.Cost;
    //                w.ShowUtility();
    //            }
    //        }
    //        item.costAmounts[i] = EditorGUILayout.IntField(item.costAmounts[i]);
    //        if (GUILayout.Button("-"))
    //        {
    //            item.costItems.RemoveAt(i);
    //            item.costAmounts.RemoveAt(i);
    //        }
    //        EditorGUILayout.EndHorizontal();
    //    }
    //    return item;
    //}

    //private static Building YieldGUI(Building item)
    //{
    //    if (item.yieldItems == null) item.yieldItems = new List<string>();
    //    if (item.yieldAmounts == null) item.yieldAmounts = new List<int>();

    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.LabelField("Yield Items");
    //    EditorGUILayout.LabelField("Yield Amounts");
    //    if (GUILayout.Button("+"))
    //    {
    //        if (!GameItemSelectWindow.isOpen)
    //        {
    //            GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //            w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //            w.modifiedItem = item;
    //            w.modifiedCategory = EditorCategory.Yield;
    //            w.ShowUtility();
    //        }
    //    }
    //    EditorGUILayout.EndHorizontal();
    //    for (int i = 0; i < item.yieldItems.Count; i++)
    //    {
    //        EditorGUILayout.BeginHorizontal();
    //        string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.yieldItems[i]].itemName;
    //        if (GUILayout.Button(itemLabel))
    //        {
    //            if (!GameItemSelectWindow.isOpen)
    //            {
    //                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //                w.modifiedItem = item;
    //                w.modifiedID = item.yieldItems[i];
    //                w.modifiedCategory = EditorCategory.Yield;
    //                w.ShowUtility();
    //            }
    //        }
    //        item.yieldAmounts[i] = EditorGUILayout.IntField(item.yieldAmounts[i]);
    //        if (GUILayout.Button("-"))
    //        {
    //            item.yieldItems.RemoveAt(i);
    //            item.yieldAmounts.RemoveAt(i);
    //        }
    //        EditorGUILayout.EndHorizontal();
    //    }
    //    return item;
    //}

    //private static Ship YieldGUI(Ship item)
    //{
    //    if (item.yieldItems == null) item.yieldItems = new List<string>();
    //    if (item.yieldAmounts == null) item.yieldAmounts = new List<int>();

    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.LabelField("Yield Items");
    //    EditorGUILayout.LabelField("Yield Amounts");
    //    if (GUILayout.Button("+"))
    //    {
    //        if (!GameItemSelectWindow.isOpen)
    //        {
    //            GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //            w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //            w.modifiedItem = item;
    //            w.modifiedCategory = EditorCategory.Yield;
    //            w.ShowUtility();
    //        }
    //    }
    //    EditorGUILayout.EndHorizontal();
    //    for (int i = 0; i < item.yieldItems.Count; i++)
    //    {
    //        EditorGUILayout.BeginHorizontal();
    //        string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.yieldItems[i]].itemName;
    //        if (GUILayout.Button(itemLabel))
    //        {
    //            if (!GameItemSelectWindow.isOpen)
    //            {
    //                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //                w.modifiedItem = item;
    //                w.modifiedID = item.yieldItems[i];
    //                w.modifiedCategory = EditorCategory.Yield;
    //                w.ShowUtility();
    //            }
    //        }
    //        item.yieldAmounts[i] = EditorGUILayout.IntField(item.yieldAmounts[i]);
    //        if (GUILayout.Button("-"))
    //        {
    //            item.yieldItems.RemoveAt(i);
    //            item.yieldAmounts.RemoveAt(i);
    //        }
    //        EditorGUILayout.EndHorizontal();
    //    }
    //    return item;
    //}

    //private static Building StorageGUI(Building item)
    //{
    //    if (item.storageTypes == null) item.storageTypes = new List<string>();
    //    if (item.storageLimits == null) item.storageLimits = new List<int>();
    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.LabelField("Storage Types");
    //    EditorGUILayout.LabelField("Storage Amounts");
    //    if (GUILayout.Button("+"))
    //    {
    //        if (!GameItemSelectWindow.isOpen)
    //        {
    //            GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //            w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //            w.modifiedItem = item;
    //            w.modifiedCategory = EditorCategory.Storage;
    //            w.ShowUtility();
    //        }
    //    }
    //    EditorGUILayout.EndHorizontal();
    //    for (int i = 0; i < item.storageTypes.Count; i++)
    //    {
    //        EditorGUILayout.BeginHorizontal();
    //        string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.storageTypes[i]].itemName;
    //        if (GUILayout.Button(itemLabel))
    //        {
    //            if (!GameItemSelectWindow.isOpen)
    //            {
    //                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //                w.modifiedItem = item;
    //                w.modifiedID = item.storageTypes[i];
    //                w.modifiedCategory = EditorCategory.Storage;
    //                w.ShowUtility();
    //            }
    //        }
    //        item.storageLimits[i] = EditorGUILayout.IntField(item.storageLimits[i]);
    //        if (GUILayout.Button("-"))
    //        {
    //            item.storageTypes.RemoveAt(i);
    //            item.storageLimits.RemoveAt(i);
    //        }
    //        EditorGUILayout.EndHorizontal();
    //    }
    //    return item;
    //}

    //private static Ship StorageGUI(Ship item)
    //{
    //    if (item.storageTypes == null) item.storageTypes = new List<string>();
    //    if (item.storageLimits == null) item.storageLimits = new List<int>();
    //    EditorGUILayout.BeginHorizontal();
    //    EditorGUILayout.LabelField("Storage Types");
    //    EditorGUILayout.LabelField("Storage Amounts");
    //    if (GUILayout.Button("+"))
    //    {
    //        if (!GameItemSelectWindow.isOpen)
    //        {
    //            GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //            w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //            w.modifiedItem = item;
    //            w.modifiedCategory = EditorCategory.Storage;
    //            w.ShowUtility();
    //        }
    //    }
    //    EditorGUILayout.EndHorizontal();
    //    for (int i = 0; i < item.storageTypes.Count; i++)
    //    {
    //        EditorGUILayout.BeginHorizontal();
    //        string itemLabel = DatabaseManager.Database[ItemType.Commodity][item.storageTypes[i]].itemName;
    //        if (GUILayout.Button(itemLabel))
    //        {
    //            if (!GameItemSelectWindow.isOpen)
    //            {
    //                GameItemSelectWindow w = ScriptableObject.CreateInstance<GameItemSelectWindow>();
    //                w.parentWindow = EditorWindow.GetWindow<ManagerWindow>();
    //                w.modifiedItem = item;
    //                w.modifiedID = item.storageTypes[i];
    //                w.modifiedCategory = EditorCategory.Storage;
    //                w.ShowUtility();
    //            }
    //        }
    //        item.storageLimits[i] = EditorGUILayout.IntField(item.storageLimits[i]);
    //        if (GUILayout.Button("-"))
    //        {
    //            item.storageTypes.RemoveAt(i);
    //            item.storageLimits.RemoveAt(i);
    //        }
    //        EditorGUILayout.EndHorizontal();
    //    }
    //    return item;
    //}
}
