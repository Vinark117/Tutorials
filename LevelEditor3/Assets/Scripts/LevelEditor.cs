using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using VinTools;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor instance;
    public void Awake()
    {
        instance = this;
    }

    [SerializeField] Tilemap defaultTilemap;

    Tilemap currentTilemap
    {
        get
        {
            if (LevelManager.instance.layers.TryGetValue((int)LevelManager.instance.tiles[_selectedTileIndex].tilemap, out Tilemap tilemap))
            {
                return tilemap;
            }
            else
            {
                return defaultTilemap;
            }
        }
    }
    TileBase currentTile
    {
        get
        {
            return LevelManager.instance.tiles[_selectedTileIndex].tile;
        }
    }

    [SerializeField] Camera cam;

    public int _selectedTileIndex;

    private void Update()
    {
        Vector3Int pos = currentTilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));

        if (!Funcs.IsPointerOverGameObject())
        {
            //place tile with left click
            if (Input.GetMouseButton(0)) PlaceTile(pos);
            //delete tile with right click
            if (Input.GetMouseButton(1)) DeleteTile(pos);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        { 
            _selectedTileIndex++;
            if (_selectedTileIndex >= LevelManager.instance.tiles.Count) _selectedTileIndex = 0;
            Debug.Log(LevelManager.instance.tiles[_selectedTileIndex].name);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            _selectedTileIndex--;
            if (_selectedTileIndex < 0) _selectedTileIndex = LevelManager.instance.tiles.Count - 1;
            Debug.Log(LevelManager.instance.tiles[_selectedTileIndex].name);
        }
    }

    /// <summary>
    /// Place down the current tile on the current tilemap at pos
    /// </summary>
    /// <param name="pos"></param>
    void PlaceTile(Vector3Int pos)
    {
        currentTilemap.SetTile(pos, currentTile);
    }

    /// <summary>
    /// Delete the tile on the current tilemap at pos
    /// </summary>
    /// <param name="pos"></param>
    void DeleteTile(Vector3Int pos)
    {
        currentTilemap.SetTile(pos, null);
    }
}
