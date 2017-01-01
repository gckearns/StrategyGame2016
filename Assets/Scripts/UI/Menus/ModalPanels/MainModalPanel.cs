using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainModalPanel : MonoBehaviour {

    public Text menuItemTitle;
    public Image iconImage;
    public Text menuItemSubtitle;
    public Button yesButton;
    public Button cancelButton;

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void BuildDialogue(string[] dialogueStrings, Sprite icon, UnityAction yesEvent, UnityAction cancelEvent)
    {
        menuItemTitle.text = dialogueStrings[0];
        menuItemSubtitle.text = dialogueStrings[1];
        iconImage.sprite = icon;

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(cancelEvent);
        cancelButton.onClick.AddListener(ClosePanel);

        gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}