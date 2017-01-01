using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Building))]
public class BuildingInspector : Editor {
    public override void OnInspectorGUI()
    {
        Debug.Log("Running default inspector...");
        base.OnInspectorGUI();
        Debug.Log("Ran default inspector.");
    }
}
