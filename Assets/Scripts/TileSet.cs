using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileSet{

    public Tile[] tileSet;

    public TileSet(int size)
    {
        tileSet = new Tile[size];
    }

    public Tile this[int i]
    {
        get
        {
            return tileSet[i];
        }
        set
        {
            tileSet[i] = value;
        }
    }
}
