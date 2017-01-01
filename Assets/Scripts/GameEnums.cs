[System.Serializable]
public enum ItemType
{
	None = 0,
	Building,
	Commodity,
    Ship
}
public enum EditorCategory
{
    None = 0,
    Cost,
    Yield,
    Storage
}
[System.Serializable]
public enum BuildingCategory
{
	None = 0,
	Housing,
	Industry,
	Services
}
[System.Serializable]
public enum BuildingState
{
    None = 0,
    Constructing,
    Operating,
    Inactive,
    Recycling,
    Recycled,
    Destroyed,
    Transit
}
[System.Serializable]
public enum CommodityCategory
{
	None = 0,
	Solid,
	Liquid,
	Gas
}
[System.Serializable]
public enum ShipState
{
    None = 0,
    Ready,
    Launching,
    Flight,
    Landing,
    Constructing,
    Recycling,
    Recycled,
    Destroyed,
    Transit
}