using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

public delegate GameItem ItemConstructor(ItemType type, string name, string ID);

[System.Serializable]
public class GameItemList {


	public ItemConstructor newItemConstructor;

	public ItemType itemType = ItemType.None;
    //[SerializeField]
    //private List<GameItem> _gameItems = new List<GameItem>();
    //[SerializeField]
    //private GameItem[] _gameItems = new GameItem[1024];
    [SerializeField]
    private List<GameItem> _gameItemsList = new List<GameItem>();

    public List<GameItem> gameItems
    {
        get
        {
            if (_gameItemsList == null)
            {
                _gameItemsList = new List<GameItem>();
            }
            return _gameItemsList;
        }
    }
    public List<string> myIDs = new List<string>();

	public void Add (GameItem item) {
		gameItems.Add(item);
		myIDs.Add(item.itemID);
	}

    public GameItem[] ToArray()
    {
        return gameItems.ToArray();
    }

    /// <summary>
    /// Gets the number of <see cref="GameItem"/>actually contained in the <see cref="GameItemList"/>
    /// </summary>
    public int Count { get { return gameItems.Count; } }

	public GameItemList (ItemType itemType) {
		this.itemType = itemType;
		newItemConstructor = GameUtility.GetConstructor(itemType);
//		Debug.Log("Creating new " + this.ToString());
	}

	/// <summary>
	/// Returns an enumerator that iterates through the <see cref="GameItemList"/>
	/// </summary>
	public List<GameItem>.Enumerator GetEnumerator () {
		return gameItems.GetEnumerator();
	}

	/// <summary>
	/// Gets the <see cref="GameItem"/>at the specified index
	/// </summary>
	public GameItem this [int index] {
		get {
			return gameItems[index];
		}
	}

	/// <summary>
	/// Searches for a <see cref="GameItem"/>with the specified ID and returns the first occurance within the entire <see cref="GameItemList"/>
	/// </summary>
	public GameItem this [string itemId] {
		get {
			return gameItems.Find(x => x.itemID.Equals(itemId));
		}
	}

	/// <summary>
	/// Removes the first occurance of a specific <see cref="GameItem"/>from the <see cref="GameItemList"/>
	/// </summary>
	public void Remove (GameItem item) {
		myIDs.Remove(item.itemID);
		gameItems.Remove(item);
	}

	/// <summary>
	/// Searches for a <see cref="GameItem"/>with the specified ID and removes the first occurance from within the entire <see cref="GameItemList"/>
	/// </summary>
	/// 
	public void Remove (string itemId) {
		myIDs.Remove(itemId);
		gameItems.Remove(this[itemId]);
	}

	/// <summary>
	/// Returns a nicely formatted string that represents the current <see cref="GameItemList"/>
	/// </summary>
	public override string ToString()
	{
		return string.Format("[GameItemList: Type={0}, Count={1}]", itemType, Count);
	}

	private string[] _objectNames;
	public string[] objectNames {
		get {
			_objectNames = new string[gameItems.Count];
			for (int i = 0; i < gameItems.Count; i++) {
				_objectNames[i] = gameItems[i].itemName;
			}				
			return _objectNames;
		}
	}

	private string[] _objectIDs;
	public string[] objectIDs {
		get {
			_objectIDs = new string[gameItems.Count];
			for (int i = 0; i < gameItems.Count; i++) {
				_objectIDs[i] = gameItems[i].itemID;
			}				
			return _objectIDs;
		}
	}

	private string[] _objectEnums;
	public string[] objectEnums {
		get {
			_objectEnums = new string[gameItems.Count];
			for (int i = 0; i < gameItems.Count; i++) {
				_objectEnums[i] = gameItems[i].catName;
			}				
			return _objectEnums;
		}
	}

    //[OnSerializing]
    //internal void OnSerializingMethod(StreamingContext context)
    //{
    //    Debug.Log("GameItemList " + itemType + " Serializing");
    //    //_gameItems = _gameItemsList.ToArray();
    //}

    //[OnSerialized]
    //internal void OnSerializedMethod(StreamingContext context)
    //{
    //    Debug.Log("GameItemList " + itemType + " Serialized");
    //}

    //[OnDeserializing]
    //internal void OnDeserializingMethod(StreamingContext context)
    //{
    //    Debug.Log("GameItemList " + itemType + " Deserializing");
    //}

    //[OnDeserialized]
    //internal void OnDeserializedMethod(StreamingContext context)
    //{

    //    //_gameItemsList = new List<GameItem>(_gameItems);
    //    Debug.Log("GameItemList " + itemType + " Deserialized");
    //}
}
