using System.Collections.Generic;
using System;

public class GameUtility {
	public static List<ItemType> ItemEnums = new List<ItemType> (new ItemType[]
		{
			ItemType.Building,
			ItemType.Commodity,
            ItemType.Ship
		}
	);

    public static List<BuildingCategory> BuildingEnums = new List<BuildingCategory>(new BuildingCategory[]
    {
            BuildingCategory.Housing,
            BuildingCategory.Industry,
            BuildingCategory.Services,
    }
);

    private static string[] _ItemTypeStrings;
	public static string[] ItemTypeStrings {
		get {
			if (_ItemTypeStrings == null) {
				_ItemTypeStrings = new string[ItemEnums.Count];
				for (int i = 0; i < ItemEnums.Count; i++) {
					_ItemTypeStrings[i] = ItemEnums[i].ToString();
				}
			}
			return _ItemTypeStrings;
		}
	}

	public static ItemConstructor GetConstructor(ItemType type) {
		switch (type) {
			case ItemType.Building:
				return new ItemConstructor(NewBuilding);
//				break;
			case ItemType.Commodity:
				return new ItemConstructor(NewCommodity);
            //				break;
            case ItemType.Ship:
                return new ItemConstructor(NewShip);
            default:
				return null;
		}
	}

	public static Building NewBuilding(ItemType type, string name, string ID){
		Building item = new Building(type, name, ID);
		return item;
	}

	public static Commodity NewCommodity(ItemType type, string name, string ID){
		Commodity item = new Commodity(type, name, ID);
		return item;
	}

    public static Ship NewShip(ItemType type, string name, string ID)
    {
        Ship item = new Ship(type, name, ID);
        return item;
    }
}
