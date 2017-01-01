using UnityEngine;
using System.Collections;

public class ShipSaveState : GameItemSaveState {
    public new Ship gameItemType { get; set; }
    public ShipState shipState { get; set; }
    public float stateSwitchProgress { get; set; }
}
