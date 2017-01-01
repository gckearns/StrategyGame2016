using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModalItemPanel : MonoBehaviour {
    public Image icon;
    public Text itemText;
    public Text itemSubText;
    public string itemID;

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void BuildDialogue(string[] dialogueStrings, Sprite icon, string itemID)
    {
        itemText.text = dialogueStrings[0];
        itemSubText.text = dialogueStrings[1];
        this.icon.sprite = icon;
        this.itemID = itemID;
        gameObject.SetActive(true);
    }

    public void UpdateDialogueSubtext(string subtext)
    {
        itemSubText.text = subtext;
    }
}
