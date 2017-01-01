using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System;

public class DatabaseManager
{

	private static BinaryFormatter formatter = new BinaryFormatter();
	private static string path = "Assets/Game Data Manager/Resources/Database.ocsw";
	private static string pathFolder = "Assets/Game Data Manager/Resources";
    private static string parentfolder = "Game Data Manager/";
    private static string resourcesFolder = "Resources";

    private static GameDatabase _Database;

	public static GameDatabase Database {
		get {
			if (_Database == null) {
				LoadDatabase();
			}
			return _Database;
		}
	}

	private static void Init()
	{
		if (_Database == null) {
			LoadDatabase();
			Debug.Log("Database was null");
		}
		List <ItemType> databaseTypes = new List<ItemType>();
		foreach (var list in _Database) {
			databaseTypes.Add(list.itemType);
		}
		foreach (var itemType in GameUtility.ItemEnums) {
			if (!databaseTypes.Contains(itemType)) {
				_Database.Add(new GameItemList(itemType));
				Debug.Log("Database was missing " + itemType.ToString());
			}
		}
	}

	static void CreateDatabase()
	{
		_Database = new GameDatabase();
		Debug.Log("Created new GameDatabase");
		SaveDatabase();
	}

    static void CreateDirectory()
    {
            AssetDatabase.CreateFolder(parentfolder, resourcesFolder);
            Debug.Log(string.Format("Created {0} folder in {1}", resourcesFolder, parentfolder));
//        CreateDatabase();
    }

    public static void SaveDatabase()
	{
		if (_Database != null) {
			FileStream stream = new FileStream(path, FileMode.Create);
			formatter.Serialize(stream, _Database);
			stream.Close();
			Debug.Log("Saved database"); 	
		} else {
			throw new ArgumentNullException("_Database", "GameDatabase is null, cannot save");
		}

	}

	public static void LoadDatabase()
	{
		Directory.CreateDirectory(pathFolder);
		if (!File.Exists(path)) {
			CreateDatabase();
		}
		FileStream stream = new FileStream(path, FileMode.Open);
		_Database = formatter.Deserialize(stream) as GameDatabase;
		Debug.Log("Loaded database from disk: " + _Database.ToString());
		stream.Close();
	}

	public static void ResetDatabase()
	{
		Debug.Log("Database reset.");
		_Database = null;
	}
}
