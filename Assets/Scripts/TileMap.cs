using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StrategyGame;

public class TileMap : MonoBehaviour {

    private int playTilesX;
    private int playTilesZ;
    private int numTilesX;
    private int numTilesZ;
    private float mapDim;
    private int tileResolution;
    private float tileSide;
    private float tileDiag;
    private float tileRadius;
    private Color[] colorsDefault;
    private Color[] colorsCrater;
    private Color[] colorsBorder;
    private WorldController worldController;

    public Texture2D textureAtlas;
    public Texture2D textureGravel;
    public Tile[,] tiles { get; protected set; }
    //public Tile[,] tiles;

    //public TileSet[] tileSet;

    void Start () {
        playTilesX = GameResources.PlayTilesX;
        playTilesZ = GameResources.PlayTilesZ;
        numTilesX = GameResources.NumTilesX;
        numTilesZ = GameResources.NumTilesZ;
        mapDim = GameResources.MapDim;
        tileResolution = GameResources.TileResolution;
        tileSide = GameResources.TileSide;
        tileDiag = GameResources.TileDiag;
        tileRadius = GameResources.TileRadius;
        GenerateTileColors ();
        GenerateMap ();
        worldController = GetComponentInParent<WorldController> ();
        //print("TileMap Start() tileSet Length: " + tileSet.Length);
        //worldController.tileSet = tileSet;
    }

    public void Click (Vector3 clickPoint) {
        Tile clickedTile = PointToTile (clickPoint);
        //        print (clickedTile.worldCoords.ToString ());
        //print("name:" + gameObject.name);
        if (clickedTile.terrainType != TileTerrainType.Border) {
            worldController.tileManager.SelectTile (clickedTile);
        }
    }

    Tile PointToTile (Vector3 point) {
        int x = (int) Mathf.Floor(point.x / tileDiag) * 2;
        int y = (int) Mathf.Floor (point.z / tileDiag);
//        print ("Region: (" + x + "," + y + ")");
//        print ("x % diag: " + point.x % tileDiag + "," + "z % diag: " + point.z % tileDiag);
        float mx = point.x % tileDiag;
        float mz = point.z % tileDiag;
        if (mz < (tileRadius - mx)) { // Bottom left
//            print ("bottom left");
            x--;
            y--;
        } else if (mz > (tileRadius + mx)) { // Top left
//            print ("top left");
            x--;
        } else if (mz < (mx - tileRadius)) { // Bottom right
//            print ("bottom right");
            x++;
            y--;
        } else if (mz > ((3 * tileRadius) - mx)) { // Top right
//            print ("top right");
            x++;
        } else {
//            print ("center");
        }
        if (x == -1) {
            x += numTilesX; // border tile coordinates out of bounds, wrap around
        }
        if (y == -1) {
            y += numTilesZ; // border tile coordinates out of bounds, wrap around
        }
        //        print ("Tile: (" + x + "," + y + ")");
        return tiles[x, y];
        //return tileSet[y][x];
    }

    Color[] getTileColors(Tile tile) {
        switch (tile.terrainType) {
        case TileTerrainType.Border:
            return this.colorsBorder;
        case TileTerrainType.Crater:
            return this.colorsCrater;
        default:
            return this.colorsDefault;
        }
    }

    void GenerateTileColors () {
        this.colorsDefault = textureAtlas.GetPixels(0 * tileResolution, 0, tileResolution,tileResolution);
        this.colorsCrater = textureAtlas.GetPixels(1 * tileResolution, 0 ,tileResolution,tileResolution);
        this.colorsBorder = textureAtlas.GetPixels(2 * tileResolution, 0 ,tileResolution,tileResolution);
    }

    void GenerateMap () {
        GenerateMesh ();
        GenerateTiles ();
        GenerateTexture ();
    }

    void GenerateMesh(){
        int numMeshTilesX = 1;
        int numMeshTilesZ = 1;
        int numVertsX = (numMeshTilesX * 2);
        int numVertsZ = (numMeshTilesZ * 2);
        int numVerts = numVertsX * numVertsZ;
        int numTris = (numMeshTilesX * numMeshTilesZ) * 2;

        Vector3[] verticies = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];
        int[] triangles = new int[numTris * 3];

        int i = 0;
        for (int z = 0; z < numVertsZ; z++) {
            for (int x = 0; x < numVertsX; x++) {
                verticies [i].Set (x * mapDim, 0f, z * mapDim);
                normals [i] = Vector3.up;
                uv [i].Set ((float) x / (float) 1, (float) z / (float) 1);
                i++;
            }
        }

        i = 0;
        for (int z = 0; z < numMeshTilesZ; z++) {
            for (int x = 0; x < numMeshTilesX; x++) {
                triangles [i + 0] = x + (z * numVertsX);
                triangles [i + 1] = x + (z * numVertsX) + numVertsX + 1 ;
                triangles [i + 2] = x + (z * numVertsX) + 1;
                triangles [i + 3] = x + (z * numVertsX);
                triangles [i + 4] = x + (z * numVertsX) + numVertsX;
                triangles [i + 5] = x + (z * numVertsX) + numVertsX + 1;
                i += 6;
            }
        }

        Mesh mesh = new Mesh ();
        mesh.vertices = verticies;
        mesh.normals = normals;
        mesh.uv = uv;
        mesh.name = "TileMap";

        mesh.subMeshCount = 2;
        mesh.SetTriangles (triangles, 0);
        mesh.SetTriangles (triangles, 1);

        MeshFilter meshFilter = (MeshFilter) GetComponent<MeshFilter>();
        MeshCollider meshCollider = (MeshCollider) GetComponent<MeshCollider>();

        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    void GenerateTiles () {
        tiles = new Tile[numTilesX, numTilesZ];
        //tileSet = new TileSet[numTilesZ];
        for (int z = 0; z < numTilesZ; z++) {
            //tileSet[z] = new TileSet(numTilesX);
            for (int x = 0; x < numTilesX; x+=2) {
                //tileSet[z][x] = new Tile(new Vector2(x, z), new Vector3((float)(x + 1) * tileRadius, 0f,
                //    (float)(z * tileDiag) + (x % 2 * tileRadius) + tileRadius));
                tiles[x, z] = new Tile(new Vector2(x, z), new Vector3((float)(x + 1) * tileRadius, 0f,
                    (float)(z * tileDiag) + (x % 2 * tileRadius) + tileRadius));

                //tileSet[z][x] = tiles[x, z];
            }
        }
        for (int z = 0; z < numTilesZ; z++) {
            for (int x = 1; x < numTilesX; x+=2) {
                //tileSet[z][x] = new Tile(new Vector2(x, z), new Vector3((float)(x + 1) * tileRadius, 0f,
                //    (float)(z * tileDiag) + (x % 2 * tileRadius) + tileRadius));
                tiles[x, z] = new Tile(new Vector2(x, z), new Vector3((float)(x + 1) * tileRadius, 0f,
                    (float)(z * tileDiag) + (x % 2 * tileRadius) + tileRadius));
                    //tileSet[z][x] = tiles[x, z];

                if (x == numTilesX - 1 || z == (numTilesZ - 1)) {
                    tiles[x, z].terrainType = TileTerrainType.Border;
                    //tileSet[z][x].terrainType = TileTerrainType.Border;

                    //tileSet[z][x] = tiles[x, z];
                }
            }
        }
//        int addNumCraters = 2;
//        while (addNumCraters > 0) {
//            Vector2 randCoords = new Vector2 (Random.Range (0, numTilesX), Random.Range (0, numTilesZ));
//            Tile selectedTile = tiles [(int) randCoords.x, (int) randCoords.y];
//            if (selectedTile.terrainType == TileTerrainType.Default) {
//                selectedTile.terrainType = TileTerrainType.Crater;
//                addNumCraters --;
//            }
//        }
    }

    void GenerateTexture () {
        Texture2D mapTexture = new Texture2D(playTilesX * 32, playTilesZ * 32);
        for (int z = 0; z < playTilesZ; z++) {
            for (int x = 0; x < playTilesX; x++) {
                mapTexture.SetPixels(x * 32, z * 32, 32, 32, getTileColors(tiles[x * 2, z]));
                //mapTexture.SetPixels(x * 32, z * 32, 32, 32, getTileColors(tileSet[z][x * 2]));
            }
        }
        Texture2D mapTexOffset = new Texture2D(playTilesX * 32, playTilesZ * 32);
        for (int z = 0; z < playTilesZ; z++) {
            for (int x = 0; x < playTilesX; x++) {
                mapTexOffset.SetPixels(x * 32, z * 32, 32, 32, getTileColors(tiles[x * 2 + 1, z]));
                //mapTexOffset.SetPixels(x * 32, z * 32, 32, 32, getTileColors(tileSet[z][x * 2 + 1]));
            }
        }
        MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
        meshRenderer.materials [0].mainTexture = mapTexture;
        meshRenderer.materials [1].mainTexture = mapTexOffset;
        mapTexture.Apply ();
        mapTexOffset.Apply ();
    }

    public override string ToString()
    {
        return base.ToString() + "Tiles: " + tiles.Length;
    }
}
