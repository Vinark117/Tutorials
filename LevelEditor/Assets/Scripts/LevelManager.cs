using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        //set up the instance
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public List<CustomTile> tiles = new List<CustomTile>();
    public Tilemap tilemap;

    private void Update()
    {
        //save level when pressing Ctrl + A
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A)) Savelevel();
        //load level when pressing Ctrl + L
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L)) LoadLevel();
    }

    void Savelevel()
    {
        //get the bounds of the tilemap
        BoundsInt bounds = tilemap.cellBounds;

        //create a new leveldata
        LevelData levelData = new LevelData();

        //loop trougth the bounds of the tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                //get the tile on the position
                TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
                //find the temp tile in the custom tiles list
                CustomTile temptile = tiles.Find(t => t.tile == temp);

                //if there's a customtile associated with the tile
                if (temptile != null)
                {
                    //add the values to the leveldata
                    levelData.tiles.Add(temptile.id);
                    levelData.poses_x.Add(x);
                    levelData.poses_y.Add(y);
                }
            }
        }

        //save the data as a json
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.dataPath + "/testLevel.json", json);

        //debug
        Debug.Log("Level was saved");
    }

    void LoadLevel()
    {
        //load the json file to a leveldata
        string json = File.ReadAllText(Application.dataPath + "/testLevel.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        //clear the tilemap
        tilemap.ClearAllTiles();

        //place the tiles
        for (int i = 0; i < data.tiles.Count; i++)
        {
            tilemap.SetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0), tiles.Find(t => t.name == data.tiles[i]).tile);
        }

        //debug
        Debug.Log("Level was loaded");
    }
}

public class LevelData
{
    public List<string> tiles = new List<string>();
    public List<int> poses_x = new List<int>();
    public List<int> poses_y = new List<int>();
}