using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System;

public class AddObjectWindow : EditorWindow {

    public string objectName = "New Object";
    public string objectID = "newobject";
    public static bool isOpen = false;
	public GameItemList database;
    public EditorWindow parentWindow;

    void OnDisable () {
        isOpen = false;
    }

    void OnGUI ()
    {
        isOpen = true;
        EditorGUILayout.BeginVertical (GUILayout.ExpandHeight(true));
        objectName = EditorGUILayout.TextField ("Name", objectName);
        EditorGUILayout.BeginHorizontal ();
        objectID = EditorGUILayout.TextField ("ID", objectID);
        if (GUILayout.Button ("Suggest")) {
			EditorGUIUtility.keyboardControl = 0;
            SuggestID ();
        }
        EditorGUILayout.EndHorizontal ();
        EditorGUILayout.EndVertical ();
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button ("OK")) {
            ValidateID ();
        }
        if (GUILayout.Button ("Cancel")) {
            Close ();
        }
        EditorGUILayout.EndHorizontal ();
        Event e = Event.current;
        //        Debug.Log ("Event: " + e.type.ToString());
        if (e.type == EventType.MouseUp) {
            EditorGUIUtility.keyboardControl = 0;
        }
    } 

    void ValidateID () {
        if (IsValidID(objectID)) {
			database.Add(database.newItemConstructor(database.itemType, objectName, objectID));
            parentWindow.Focus ();
            Close ();
            //            FocusWindowIfItsOpen<DataManagerWindow> ();
        } else {
            RemoveNotification ();
            ShowNotification (new GUIContent("ID already exists!"));
        }
    }

    void SuggestID() {
        StringBuilder sb = new StringBuilder ();
        List<char> charList = new List<char> (objectName.ToCharArray ()); //convert the name field to a char array
        foreach (char c in charList) { //iterate through each char in the char array
            if (!char.IsWhiteSpace(c)) { //check that the char is not whitespace
				sb.Append (char.ToLower(c)); //convert the char to lowercase and add it to the stringbuilder
            }
        }
        string tryID = sb.ToString (); //convert the formatted string to a string
        int appendInt = 2; //start the potential appended number with 2
        bool appended = false;
        while (!IsValidID(tryID)) { //validate string as a unique id
            if (appended) { //if this was looped it will have a number appended
                sb.Remove (sb.Length - 1, 1); //remove the old appended number from the end of the string builder
            }
            tryID = sb.Append(appendInt).ToString(); //add the append number to the id string
			appendInt ++;
			appended = true;
        }
        objectID = tryID;
    }

    bool IsValidID (string checkID){
		if (database == null) {
			throw new NullReferenceException("Database not selected");
		}
		return !database.myIDs.Contains(checkID);
    }
}
