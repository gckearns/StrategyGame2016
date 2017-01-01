using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingModalPanel : MonoBehaviour {

    public Image iconImage;
    public Text titleText;
    public Text subtitleText;
    public Text textOne;
    public Text textTwo;
    public Text textThree;
    public Text smallTextOne;
    public Text smallTextTwo;
    public Button cancelButton;
    public Button buttonOne;
    public Button buttonTwo;
    public Button buttonThree;

    public void BuildDialogue(string[] dialogueStrings, Sprite icon, UnityAction cancelEvent, UnityAction eventOne, UnityAction eventTwo, UnityAction eventThree)
    {
        this.iconImage.sprite = icon;

        this.titleText.text = dialogueStrings[0];
        this.subtitleText.text = dialogueStrings[1];

        this.textOne.text = dialogueStrings[2];
        this.textTwo.text = dialogueStrings[3];
        this.textThree.text = dialogueStrings[4];

        this.smallTextOne.text = dialogueStrings[5];
        this.smallTextTwo.text = dialogueStrings[6];

        buttonOne.onClick.RemoveAllListeners();
        buttonOne.onClick.AddListener(eventOne);
        buttonOne.onClick.AddListener(ClosePanel);

        buttonTwo.onClick.RemoveAllListeners();
        buttonTwo.onClick.AddListener(eventTwo);
        buttonTwo.onClick.AddListener(ClosePanel);

        buttonThree.onClick.RemoveAllListeners();
        buttonThree.onClick.AddListener(eventThree);
        buttonThree.onClick.AddListener(ClosePanel);

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