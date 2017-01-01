using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class Ship : GameItem
    {
        public int size { get; set; }
        public override string catName { get { return (size + "x" + size); } }
        public int constructionTime { get; set; }
        public List<string> costItems = new List<string>();
        public List<int> costAmounts = new List<int>();
        public List<string> yieldItems = new List<string>();
        public List<int> yieldAmounts = new List<int>();
        public List<string> storageTypes = new List<string>();
        public List<int> storageLimits = new List<int>();
        public string prefabPath = "";

        public Ship(ItemType type, string name, string ID)
        {
            this.itemType = type;
            this.itemName = name;
            this.itemID = ID;
        }

        public override void OnGUI()
        {
            base.OnGUI();
            EditorGUILayout.BeginHorizontal();
            PrefabGUI();
            EditorGUILayout.EndHorizontal();
            size = EditorGUILayout.IntSlider("Size", size, 1, 3);
            constructionTime = EditorGUILayout.IntSlider("Construct Time", constructionTime,0,86400);
        }

        private void PrefabGUI()
        {
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            EditorGUI.BeginChangeCheck();
            obj = (GameObject)EditorGUILayout.ObjectField("Prefab", obj, typeof(GameObject), false, GUILayout.MinWidth(256));
            bool prefabChanged = EditorGUI.EndChangeCheck();
            if (prefabChanged)
            {
                if (obj == null)
                {
                    prefabPath = "";
                }
                else
                {
                    prefabPath = AssetDatabase.GetAssetPath(obj);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("[Ship: name={0}, id={1}, size={2}]", itemName, itemID, size);
        }
    }