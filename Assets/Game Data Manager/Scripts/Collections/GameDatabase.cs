using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

[System.Serializable]
public class GameDatabase {

    [SerializeField]
    private List<GameItemList> database = new List<GameItemList>();

	public void Add (GameItemList list) {
		if (!database.Exists(x => x.itemType == list.itemType)) {
			database.Add(list);
		} else {
			throw new ArgumentException(string.Format("Database already contains a GameItemList with ItemType {0}", list.itemType));
		}
	}

	public void Add (ItemType itemType) {
		if (!database.Exists(x => x.itemType == itemType)) {
			database.Add(new GameItemList(itemType));
		} else {
			throw new ArgumentException(string.Format("Database already contains a GameItemList with ItemType {0}", itemType));
		}
	}

	public void Clear () {
		database.Clear();
	}

	public bool Contains (ItemType itemType){
		return database.Exists(x => x.itemType.Equals(itemType));
	}

	public bool Contains (GameItemList item){
		return database.Contains(item);
	}

	public int Count { get { return database.Count; } }

	public GameItemList this [int index] {
		get {
			return database[index];
		}
		set {
			database[index] = value;
		}
	}

	public GameItemList this [ItemType itemType] {
		get {
			if (!database.Exists(x => x.itemType.Equals(itemType))) {
				Add (itemType);
                Debug.Log("Added GameItemList of type: " + itemType.ToString());
			}
			return database.Find(x => x.itemType.Equals(itemType));
		}
		set {
			database[database.FindIndex(x => x.itemType.Equals(itemType))] = value;
		}
	}

	public List<GameItemList>.Enumerator GetEnumerator () {
		return database.GetEnumerator();
	}

	public int IndexOfType(ItemType itemType) {
		List <ItemType> databaseTypes = new List<ItemType>();
		foreach (var list in this) {
			databaseTypes.Add(list.itemType);
		}
		return databaseTypes.IndexOf(itemType);
	}

	public override string ToString ()
	{
		return string.Format("[GameDatabase: Count={0}]", Count);
	}

	public void TrimExcess () {
		database.TrimExcess();
	}

    //[OnSerializing]
    //internal void OnSerializingMethod(StreamingContext context)
    //{
    //    Debug.Log("GameDatabase Serializing");
    //    //_gameItems = _gameItemsList.ToArray();
    //}

    //[OnSerialized]
    //internal void OnSerializedMethod(StreamingContext context)
    //{
    //    Debug.Log("GameDatabase Serialized");
    //}

    //[OnDeserializing]
    //internal void OnDeserializingMethod(StreamingContext context)
    //{
    //    Debug.Log("GameDatabase Deserializing");
    //}

    //[OnDeserialized]
    //internal void OnDeserializedMethod(StreamingContext context)
    //{

    //    //_gameItemsList = new List<GameItem>(_gameItems);
    //    Debug.Log("GameDatabase Deserialized");
    //}
}
