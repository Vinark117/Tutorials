using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

/// <summary>
/// Handles the storing, loading and saving of level data and the building of levels
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool levelEditor = false;
    
    [Header("Tilemaps")]
    //tilemap serialization
    public List<Tilemap> tilemaps;
    public List<CustomTile> customtiles = new List<CustomTile>();
    List<TileBase> tiles = new List<TileBase>();
    List<TileBase> editorTiles = new List<TileBase>();

    private void Awake()
    {
        //set instance
        if (instance == null) instance = this;
        else Destroy(this);

        //set the tiles array
        foreach (var item in customtiles)
        {
            tiles.Add(item.tile);
            editorTiles.Add(item.editorTile == null ? item.tile : item.editorTile);

            if (item.extraTile != null)
            {
                tiles.Add(item.extraTile.tile);
                editorTiles.Add(item.extraTile.editorTile == null ? item.extraTile.tile : item.extraTile.editorTile);
            }
        }
    }

    #region saving & loading
    public LevelData SaveLevel(string Title, string Author, string Description, byte[] previewImg, Vector2Int previewImgRes)
    {
        //create a new level
        LevelData level = new LevelData(Title, Author, Description, previewImg, previewImgRes, tilemaps.Count);

        //for every tilemap layer
        for (int i = 0; i < tilemaps.Count; i++)
        {
            //get the bounds
            BoundsInt bounds = tilemaps[i].cellBounds;

            for (int t = 0, x = bounds.min.x; x < bounds.max.x; x++)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    //get tile on the position
                    var pos = new Vector3Int(x, y, bounds.z);
                    var tile = tilemaps[i].GetTile(pos);

                    //if the tile is not empty save it
                    if (tile != null)
                    {
                        level.tilemaps[i].AddTile(editorTiles.IndexOf(tile), pos.x, pos.y);
                    }
                    t++;
                }
            }
        }

        //get every gameobject
        foreach (var item in FindObjectsOfType<EditorGameObject>())
        {
            if (!item.isChild) level.gameobjects.AddGameObject(item.tileID, item.transform.position, item.transform.eulerAngles.z, item.transform.localScale);

            //save child
            if (item.saveChildTransform)
            {
                List<EditorGameObject> childs = item.childs;

                for (int i = 0; i < childs.Count; i++)
                {
                    level.gameobjects.AddChildTransform(childs[i].transform.localPosition, childs[i].transform.eulerAngles.z, childs[i].transform.localScale);
                }
            }
        }

        //return it
        return level;
    }

    public void LoadLevel(LevelData level) => LoadLevel(level, 0, true);
    public void LoadLevel(LevelData level, int x_offset) => LoadLevel(level, x_offset, false);
    public void LoadLevel(LevelData level, int x_offset, bool clear)
    {
        //for every tilemap layer
        for (int i = 0; i < level.tilemaps.Length; i++)
        {
            //clear the tilemap
            if (clear) tilemaps[i].ClearAllTiles();

            //set tiles
            for (int t = 0; t < level.tilemaps[i].tiles.Count; t++)
            {
                tilemaps[i].SetTile(new Vector3Int(level.tilemaps[i].poses_x[t] + x_offset, level.tilemaps[i].poses_y[t], 0), levelEditor ? editorTiles[level.tilemaps[i].tiles[t]] : tiles[level.tilemaps[i].tiles[t]]);
                tilemaps[i].RefreshTile(new Vector3Int(level.tilemaps[i].poses_x[t] + x_offset, level.tilemaps[i].poses_y[t], 0));
            }
        }
        
        if (clear)
        {
            //clear the already existing gameobjects
            foreach (var item in FindObjectsOfType<EditorGameObject>())
            {
                Destroy(item.gameObject);
            }
        }

        Transform previousParent = null;
        int childIndex = 0;

        //for every gameobject
        for (int i = 0; i < level.gameobjects.ids.Count; i++)
        {
            int goid = level.gameobjects.ids[i];
            Vector3 pos = new Vector3(level.gameobjects.poses_x[i] + x_offset, level.gameobjects.poses_y[i], 0);
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, level.gameobjects.rotations[i]));

            //center level editor camera on rocket
            /*if (levelEditor && goid == 0 && EditorCamera.instance != null)
            {
                EditorCamera.newPosition = new Vector3(level.gameobjects.poses_x[i] + x_offset, level.gameobjects.poses_y[i], -10);
            }*/

            //if object is parent
            if (goid >= 0)
            {
                GameObject go = Instantiate(levelEditor ? customtiles[goid].editorGameObject : customtiles[goid].properGameObject, pos, rot);
                previousParent = go.transform;
                childIndex = 0;

                if (levelEditor)
                {
                    var ego = go.GetComponent<EditorGameObject>();
                    ego.tileID = goid;
                    ego.instanceID = -i;
                    ego.Instantiate();
                }

                go.transform.localScale = new Vector3(level.gameobjects.scales_x[i], level.gameobjects.scales_y[i], 1);
            }
            //if the gameobject is a child of the previous parent
            else if (goid == -1)
            {
                if (levelEditor)
                {
                    EditorGameObject[] childs = previousParent.GetComponent<EditorGameObject>().childs.ToArray();

                    var ego = childs[childIndex].GetComponent<EditorGameObject>();
                    ego.tileID = goid;
                    ego.instanceID = -i;

                    childs[childIndex].transform.localScale = new Vector3(level.gameobjects.scales_x[i], level.gameobjects.scales_y[i], 1);
                    childs[childIndex].transform.position = new Vector3(level.gameobjects.poses_x[i], level.gameobjects.poses_y[i], 1);
                    childs[childIndex].transform.rotation = Quaternion.Euler(new Vector3(0, 0, level.gameobjects.rotations[i]));

                    childs[childIndex].transform.parent = null;
                }
                else
                {
                    List<Transform> childs = new List<Transform>();

                    for (int c = 0; c < previousParent.childCount; c++)
                    {
                        childs.Add(previousParent.GetChild(c));
                    }

                    foreach (var item in childs)
                    {
                        item.parent = null;
                    }

                    childs[childIndex].localScale = new Vector3(level.gameobjects.scales_x[i], level.gameobjects.scales_y[i], 1);
                    childs[childIndex].position = new Vector3(level.gameobjects.poses_x[i], level.gameobjects.poses_y[i], 1);
                    childs[childIndex].rotation = Quaternion.Euler(new Vector3(0, 0, level.gameobjects.rotations[i]));
                }

                childIndex++;
            }
        }
    }

    public void LoadEndlessSectionHardness(int hardness)
    {
        tilemaps[1].SetTile(new Vector3Int(hardness, -10, 0), tiles[1]);
    }

    public EndlessSection SaveEndlessSection(string Title, string Author, string Description, byte[] previewImg, Vector2Int previewImgRes)
    { 
        //create a new level
        LevelData level = SaveLevel(Title, Author, Description, previewImg, previewImgRes);
        EndlessSection section = new EndlessSection(level);

        //get the hardness of the level at y:-10
        GetHardnessOnTilemap(1);
        GetHardnessOnTilemap(3);

        void GetHardnessOnTilemap(int index)
        {
            for (int i = 0; i < level.tilemaps[index].tiles.Count; i++)
            {
                if (level.tilemaps[index].poses_y[i] != -10 || level.tilemaps[index].poses_x[i] < 0 || level.tilemaps[index].poses_x[i] > 9) continue;
                section.hardness = level.tilemaps[index].poses_x[i] > section.hardness ? level.tilemaps[index].poses_x[i] : section.hardness;
            }
        }

        //limit the bounds of the level
        foreach (var item in level.tilemaps)
        {
            int i = 0;
            while (i < item.tiles.Count)
            {
                //remove tile
                if (item.poses_x[i] < 0 || item.poses_x[i] > 24 || item.poses_y[i] < -8 || item.poses_y[i] > 18)
                {
                    item.poses_x.RemoveAt(i);
                    item.poses_y.RemoveAt(i);
                    item.tiles.RemoveAt(i);
                }
                //keep tile
                else
                {
                    i++;
                }
            }
        }

        //setting up some private variables
        Vector2[] entrance_High_coords = { new Vector2(0, 8), new Vector2(0, 9), new Vector2(0, 10), new Vector2(1, 8), new Vector2(1, 9), new Vector2(1, 10) };
        Vector2[] entrance_Mid_coords = { new Vector2(0, 4), new Vector2(0, 5), new Vector2(0, 6), new Vector2(1, 4), new Vector2(1, 5), new Vector2(1, 6) };
        Vector2[] entrance_Low_coords = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 2) };
        Vector2[] exit_High_coords = { new Vector2(23, 8), new Vector2(23, 9), new Vector2(23, 10), new Vector2(24, 8), new Vector2(24, 9), new Vector2(24, 10) };
        Vector2[] exit_Mid_coords = { new Vector2(23, 4), new Vector2(23, 5), new Vector2(23, 6), new Vector2(24, 4), new Vector2(24, 5), new Vector2(24, 6) };
        Vector2[] exit_Low_coords = { new Vector2(23, 0), new Vector2(23, 1), new Vector2(23, 2), new Vector2(24, 0), new Vector2(24, 1), new Vector2(24, 2) };

        //set the entrances and exits
        GetEntrancesOnTilemap(1);
        GetEntrancesOnTilemap(3);

        void GetEntrancesOnTilemap(int index)
        {
            for (int i = 0; i < level.tilemaps[index].tiles.Count; i++)
            {
                Vector2 pos = new Vector2(level.tilemaps[index].poses_x[i], level.tilemaps[index].poses_y[i]);

                if (entrance_High_coords.Contains(pos)) section.entrance_High = false;
                if (entrance_Mid_coords.Contains(pos)) section.entrance_Mid = false;
                if (entrance_Low_coords.Contains(pos)) section.entrance_Low = false;
                if (exit_High_coords.Contains(pos)) section.exit_High = false;
                if (exit_Mid_coords.Contains(pos)) section.exit_Mid = false;
                if (exit_Low_coords.Contains(pos)) section.exit_Low = false;
            }
        }

        //return section to for saving
        return section;
    }
    #endregion
}

[System.Serializable]
public class LevelData
{
    public string saveName;
    public string author;
    public string description;

    public byte[] previewImage;
    public string[] dialogue;
    public Vector2Int previewImageResolution;

    public TilemapLayer[] tilemaps;
    public SavedGameObjects gameobjects;

    [System.Serializable]
    public class TilemapLayer
    {
        public List<int> tiles = new List<int>();
        public List<int> poses_x = new List<int>();
        public List<int> poses_y = new List<int>();

        public void AddTile(int tile, int pos_x, int pos_y)
        {
            tiles.Add(tile);
            poses_x.Add(pos_x);
            poses_y.Add(pos_y);
        }
    }
    
    [System.Serializable]
    public class SavedGameObjects
    {
        public List<int> ids = new List<int>();
        public List<float> poses_x = new List<float>();
        public List<float> poses_y = new List<float>();
        public List<float> rotations = new List<float>();
        public List<float> scales_x = new List<float>();
        public List<float> scales_y = new List<float>();

        public void AddGameObject(int id, Vector2 pos, float rotation, Vector2 scale)
        {
            ids.Add(id);
            poses_x.Add(pos.x);
            poses_y.Add(pos.y);
            rotations.Add(rotation);
            scales_x.Add(scale.x);
            scales_y.Add(scale.y);
        }

        public void AddChildTransform(Vector2 pos, float rotation, Vector2 scale)
        {
            ids.Add(-1);
            poses_x.Add(pos.x);
            poses_y.Add(pos.y);
            rotations.Add(rotation);
            scales_x.Add(scale.x);
            scales_y.Add(scale.y);
        }
    }

    public LevelData(string saveName, string author, string description, int numberOfTilemaps)
    {
        this.saveName = saveName;
        this.author = author;
        this.description = description;
        this.gameobjects = new SavedGameObjects();

        this.tilemaps = new TilemapLayer[numberOfTilemaps];

        for (int i = 0; i < this.tilemaps.Length; i++)
        {
            this.tilemaps[i] = new TilemapLayer();
        }
    }
    public LevelData(string saveName, string author, string description, byte[] previewImage, Vector2Int previewImageRes, int numberOfTilemaps)
    {
        this.saveName = saveName;
        this.author = author;
        this.description = description;
        this.previewImage = previewImage;
        this.previewImageResolution = previewImageRes;
        this.gameobjects = new SavedGameObjects();

        this.tilemaps = new TilemapLayer[numberOfTilemaps];

        for (int i = 0; i < this.tilemaps.Length; i++)
        {
            this.tilemaps[i] = new TilemapLayer();
        }
    }
    public LevelData(int numberOfTilemaps)
    {
        this.tilemaps = new TilemapLayer[numberOfTilemaps];
        this.gameobjects = new SavedGameObjects();

        for (int i = 0; i < this.tilemaps.Length; i++)
        {
            this.tilemaps[i] = new TilemapLayer();
        }
    }

    public LevelData()
    {

    }
}

public class EndlessSection
{
    public LevelData levelData;

    public bool entrance_High = true;
    public bool entrance_Mid = true;
    public bool entrance_Low = true;

    public bool exit_High = true;
    public bool exit_Mid = true;
    public bool exit_Low = true;

    //How hard is this section, 0 is the easiest
    public int hardness = 0;

    public EndlessSection(LevelData data)
    {
        this.levelData = data;

        entrance_High = true;
        entrance_Mid = true;
        entrance_Low = true;

        exit_High = true;
        exit_Mid = true;
        exit_Low = true;

        hardness = 0;
    }
}
