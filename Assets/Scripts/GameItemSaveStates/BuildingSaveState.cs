using UnityEngine;
using System.Collections;

public class BuildingSaveState : GameItemSaveState {
    public new Building gameItemType;
    public BuildingCategory category;
    public BuildingState bldgState;
    public bool powered;
    public float stateSwitchProgress;
    public float cycleProgress;
}
