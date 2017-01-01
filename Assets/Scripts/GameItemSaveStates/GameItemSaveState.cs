using UnityEngine;
using System.Collections;

public class GameItemSaveState : MonoBehaviour {

    public ItemType itemType { get; set; }
    public GameItem gameItemType { get; set; }
    public int itemID { get; set; }
    public Tile tile { get; set; }
}
