using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "VinTools/Custom Tiles/Variable Tile")]
public class VariableTile : Tile
{
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        //get these values from the base class
        base.GetTileData(position, tilemap, ref tileData);

        tileData.sprite = GetSprite(position);
    }


    [Header("Tile block")]
    public Vector2Int m_size = Vector2Int.one;
    public Sprite[] m_Sprites;

    public Sprite GetSprite(Vector3Int pos)
    {
        //check if array lenght matches the dimensions
        if (m_Sprites.Length != m_size.x * m_size.y) return sprite;

        //prevents the values to be negative
        while (pos.x < m_size.x) { pos.x += m_size.x; }
        while (pos.y < m_size.y) { pos.y += m_size.y; }

        //get the index on each axis
        int x = pos.x % m_size.x;
        int y = pos.y % m_size.y;

        //get the index in the array
        int index = x + (((m_size.y - 1) * m_size.x) - y * m_size.x);

        //returns the correct sprite
        return m_Sprites[index];
    }
}