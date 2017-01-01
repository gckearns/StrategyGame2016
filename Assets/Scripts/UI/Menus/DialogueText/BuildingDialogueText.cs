using UnityEngine;
using System.Collections;

public class BuildingDialogueText : MonoBehaviour {

    public string titleText;
    public string subtitleText;
    public string textOne;
    public string textTwo;
    public string textThree;
    public string smallTextOne;
    public string smallTextTwo;

    public BuildingDialogueText(string buildingName, string buildingDescription, string powerAndWorkers,
        string yieldTimes, string yieldItems, string buildingState, string buildingMessage)
    {
        this.titleText = buildingName;
        this.subtitleText = buildingDescription;
        this.textOne = powerAndWorkers;
        this.textTwo = yieldTimes;
        this.textThree = yieldItems;
        this.smallTextOne = buildingState;
        this.smallTextTwo = buildingMessage;
    }

    public BuildingDialogueText(BuildingSaveState building)
    {
        this.titleText = building.gameItemType.itemName;
        this.subtitleText = building.gameItemType.description;
        this.textOne = "";
        this.textTwo = GetYieldTimeString(building);
        this.textThree = GetYieldItemsString(building.gameItemType);
        this.smallTextOne = building.bldgState.ToString();
        this.smallTextTwo = "";
    }

    public string GetYieldTimeString(BuildingSaveState building)
    {
        string s = "Cycle time: " + building.gameItemType.cycleTime +
            " seconds    Time remaining: " + building.cycleProgress;
        return s;
    }

    public string GetYieldItemsString(Building building)
    {
        string[] yTypes = building.yieldItems.ToArray();
        int[] yNums = building.yieldAmounts.ToArray();
        string yieldString = "Yields: ";
        for (int i = 0; i < yTypes.Length; i++)
        {
            yieldString += yTypes[i] + ": " + yNums[i] + " ";
        }
        return yieldString;
    }

    public string[] GetStrings()
    {
        return new string[] {
            this.titleText,
            this.subtitleText,
            this.textOne,
            this.textTwo,
            this.textThree,
            this.smallTextOne,
            this.smallTextTwo
        };
    }
}
