using UnityEditor;
using UnityEngine;

    [System.Serializable]
    public class Commodity : GameItem
    {
        public CommodityCategory category { get; set; }
        public float price { get; set; }
        public float priceMin { get; set; }
        public float priceMax { get; set; }

        public override string catName { get { return category.ToString(); } }

        public Commodity(ItemType type, string name, string ID)
        {
            this.itemType = type;
            this.itemName = name;
            this.itemID = ID;
            category = CommodityCategory.None;
        }

        public override void OnGUI()
        {
            base.OnGUI();
            category = (CommodityCategory)EditorGUILayout.EnumPopup("Category", category);
            price = EditorGUILayout.FloatField("Value", price);
            priceMin = EditorGUILayout.FloatField("Min Val", priceMin);
            priceMax = EditorGUILayout.FloatField("Max Val", priceMax);
        }

        public override string ToString()
        {
            return string.Format("[Commodity: name={0}, id={1}, category={2}, value={3}]", itemName, itemID, category, price);
        }
    }
