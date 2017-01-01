using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using StrategyGame;

public class TileManager : MonoBehaviour{

    private Tile[,] _tiles;
    private TileMap tileMap;
    private Tile selectedTile;

    public Tile[,] tiles
    {
        get
        {
            if (_tiles == null) _tiles = tileMap.tiles;
            return _tiles;
        }
    }

    public TileManager Initialize(TileMap tileMap, Tile[,] tiles){
        this.tileMap = tileMap;
        return this;
    }

    public void SelectTile (Tile tile) {
        WorldController.ClearTileHighlight ();
        if (tile != selectedTile)
        {
            if (selectedTile != null && selectedTile.building != null) UnHighlightBuilding(selectedTile.building.gameObject);
            WorldController.TileHighlight(tile);
            if (tile.building != null)
            {
                HighlightBuilding(tile.building.gameObject);
                SelectBuilding(tile.building);
            }
            selectedTile = tile;
            ShowBuildMenuButtons();
        }
    }

    void ShowBuildMenuButtons()
    {
        BuildMenuButtonContainer menuButtons = BuildMenuButtonContainer.Instance();
        menuButtons.Activate();
    }

    public void OnBuildClicked(Building bldgType) {
        if (HasRoom(bldgType.size))
        {
            GameObject bldgObject = (GameObject)GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(bldgType.prefabPath, typeof(GameObject)));
            float x = bldgObject.transform.position.x;
            float y = bldgObject.transform.position.y;
            float z = bldgObject.transform.position.z;
            bldgObject.transform.position = new Vector3(selectedTile.worldCoords.x + x, y, selectedTile.worldCoords.z + z);
            bldgObject.transform.SetParent(transform);
            bldgObject.GetComponent<BuildingSaveState>().gameItemType = bldgType;
            foreach (var item in GetTilesSquare(selectedTile, bldgType.size))
            {
                item.building = bldgObject.GetComponent<BuildingSaveState>();
            }
            HighlightBuilding(bldgObject);
        }
    }

    public void HighlightBuilding(GameObject building)
    {
        if (building != null) building.transform.Translate(0,1,0);
    }

    public void UnHighlightBuilding(GameObject building)
    {
        if (building != null) building.transform.Translate(0, -1, 0);
    }

    public void SelectBuilding(BuildingSaveState building)
    {
        BuildingModalPanel panel = GameObject.FindObjectOfType<BuildingModalPanel>();
        //panel.BuildDialogue(new BuildingDialogueText(building).GetStrings(),);
    }

    public void Test()
    {
        HasRoom(2);
    }

    public bool HasRoom(int size)
    {
        foreach (var item in GetTilesSquare(selectedTile,size))
        {
            if (!IsBuildableTile(item))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsBuildableTile(Tile tile)
    {
        if (tile != null)
        {
            if (tile.building == null)
            {
                if (tile.terrainType == TileTerrainType.Default){
                    return true;
                }
            }
        }
        return false;
    }

    public Tile GetAdjacentTile(Tile tile, TileDirection direction)
    {
        int mod = ((int)tile.mapCoords.x % 2 == 0) ? 0 : 1;
        try
        {
            switch (direction)
            {
                case TileDirection.Up:
                    return tiles[(int)tile.mapCoords.x, (int)(tile.mapCoords.y + 1)];
                case TileDirection.Down:
                    return tiles[(int)tile.mapCoords.x, (int)(tile.mapCoords.y - 1)];
                case TileDirection.Left:
                    return tiles[(int)(tile.mapCoords.x - 2), (int)tile.mapCoords.y];
                case TileDirection.Right:
                    return tiles[(int)(tile.mapCoords.x + 2), (int)tile.mapCoords.y];
                case TileDirection.Northeast:
                    return tiles[(int)(tile.mapCoords.x + 1), (int)(tile.mapCoords.y + mod)];
                case TileDirection.Southeast:
                    return tiles[(int)(tile.mapCoords.x + 1), (int)(tile.mapCoords.y - 1 + mod)];
                case TileDirection.Southwest:
                    return tiles[(int)(tile.mapCoords.x - 1), (int)(tile.mapCoords.y - 1 + mod)];
                case TileDirection.Northwest:
                    return tiles[(int)(tile.mapCoords.x - 1), (int)(tile.mapCoords.y + mod)];
                default:
                    break;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }

    public Tile GetRelativeTile(Tile tile, TileDirection direction, int distance)
    {
        if (distance == 0) return tile;
        int mod = ((int)tile.mapCoords.x % 2 == 0) ? 0 : 1;
        try
        {
            switch (direction)
            {
                case TileDirection.Up:
                    return tiles[(int)tile.mapCoords.x, (int)(tile.mapCoords.y + distance)];
                case TileDirection.Down:
                    return tiles[(int)tile.mapCoords.x, (int)(tile.mapCoords.y - distance)];
                case TileDirection.Left:
                    return tiles[(int)(tile.mapCoords.x - (2 * distance)), (int)tile.mapCoords.y];
                case TileDirection.Right:
                    return tiles[(int)(tile.mapCoords.x + (2 * distance)), (int)tile.mapCoords.y];
                case TileDirection.Northeast:
                    return tiles[(int)(tile.mapCoords.x + distance), (int)(tile.mapCoords.y + mod)];
                case TileDirection.Southeast:
                    return tiles[(int)(tile.mapCoords.x + distance), (int)(tile.mapCoords.y - distance + mod)];
                case TileDirection.Southwest:
                    return tiles[(int)(tile.mapCoords.x - distance), (int)(tile.mapCoords.y - distance + mod)];
                case TileDirection.Northwest:
                    return tiles[(int)(tile.mapCoords.x - distance), (int)(tile.mapCoords.y + mod)];
                default:
                    break;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }

    public Tile[] GetTilesSquare(Tile tile, int size)
    {
        Tile[] tiles;
        tiles = new Tile[size * size];
        int i = 0;
        for (int x = 0; x < size; x++)
        {
            Tile xTile = GetRelativeTile(tile, TileDirection.Northeast, x);
            for (int z = 0; z < size; z++)
            {
                tiles[i] = GetRelativeTile(xTile, TileDirection.Northwest, z);
                //print(i+": " + tiles[i].ToString());
                i++;
            }
        }
        return tiles;
    }
}
