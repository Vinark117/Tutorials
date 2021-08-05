using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "VinTools/Custom Tiles/Counting Tile")]
public class CountingTile : Tile
{
    [Header("Counting tile")]
    public Sprite[] sprites;
    public TileBase[] tilesToCheck;

    //determines which Tiles in the vicinity are updated when this Tile is added to the Tilemap
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        for (int xd = -1; xd <= 1; xd++)
        {
            for (int yd = -1; yd <= 1; yd++)
            {
                tilemap.RefreshTile(location + new Vector3Int(xd, yd, 0));
            }
        }
    }

    //determines what the Tile looks like on the Tilemap
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        //get these values from the base class
        base.GetTileData(location, tilemap, ref tileData);

        //set sprite based on how many neighbors the tile has
        tileData.sprite = sprites[GetNeighborCount(location, tilemap)];
    }

    int GetNeighborCount(Vector3Int location, ITilemap tilemap)
    {
        int count = 0;

        for (int xd = -1; xd <= 1; xd++)
        {
            for (int yd = -1; yd <= 1; yd++)
            {
                //don't count this tile
                if (yd == 0 && xd == 0) continue;

                //get tile on the location
                TileBase tile = tilemap.GetTile(location + new Vector3Int(xd, yd, 0));

                //check if tile is in the array
                if (tilesToCheck.Contains(tile)) count++; //.Contains requires using System.Linq;
            }
        }

        //return count
        return count;
    }
    /*
#if UNITY_EDITOR
    [MenuItem("Assets/Create/VinTools/Custom Tiles/Counting Tile")]
    public static void CreateCountingTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Counting Tile", "New Counting Tile", "Asset", "Save Counting Tile", "Assets");
        if (path == "") return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<CountingTile>(), path);
    }
#endif*/
}
