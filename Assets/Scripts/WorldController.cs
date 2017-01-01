using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

    public TileMap terrainMap;
    public GameObject Highlight;
    public TileManager tileManager;
    public TileMap currentMap;

    public Tile tile;
    public TileSet[] tileSet;


    private static GameObject tileHighlight;
	// Use this for initialization
	void Start () {
        tileHighlight = Highlight;
        NewMap ();
	}

    void NewMap () {
        currentMap = Instantiate (terrainMap);
        currentMap.transform.SetParent (transform);
        tileManager = tileManager.Initialize (currentMap, currentMap.tiles);
        ClearTileHighlight ();
    }

    public static void TileHighlight (Tile tile) {
        tileHighlight.transform.position = new Vector3 (tile.worldCoords.x, 0.01f, tile.worldCoords.z);
        tileHighlight.SetActive (true);
    }

    public static void ClearTileHighlight (){
        tileHighlight.SetActive (false);
    }
}
