using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class LevelEditor : MonoBehaviour
{

    [SerializeField] Tilemap currentTilemap;
    [SerializeField] TileBase currentTile;

    [SerializeField] Camera cam;

    private void Update()
    {
        Vector3Int pos = currentTilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition));

        //place tile with left click
        if (Input.GetMouseButton(0)) PlaceTile(pos);
        //delete tile with right click
        if (Input.GetMouseButton(1)) DeleteTile(pos);
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
