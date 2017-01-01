using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class UIMenu : MonoBehaviour {

    public RectTransform content;

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void ClearMenu()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            GameObject g = content.GetChild(i).gameObject;
            g.SetActive(false);
        }
    }
}
