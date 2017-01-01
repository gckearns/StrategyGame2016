using UnityEngine;
using System.Collections;
using StrategyGame;

[System.Serializable]
public class Tile {

    //public TileMap map;
    public Vector2 mapCoords;
    public Vector3 worldCoords;
    public TileTerrainType terrainType;
    public BuildingSaveState building = null;
  
    public Tile (Vector2 mapCoords, Vector3 worldCoords) {
        //this.map = map;
        this.mapCoords = mapCoords;
        this.worldCoords = worldCoords;
        this.terrainType = TileTerrainType.Default;
    }

    public override string ToString ()
    {
        return string.Format ("[Tile: {0}, WorldCoords={1}, Type={2}]", this.mapCoords.ToString (), this.worldCoords.ToString (), terrainType);
    }
}
