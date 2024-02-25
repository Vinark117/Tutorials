using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile", menuName = "Scriptable Objects/Tile")]
public class CustomTile : ScriptableObject
{
    [Header("Tilemap")]
    public MapLayer tilemap;
    public TileMode tilemode;

    public enum MapLayer 
    { 
        Background, 
        Floor, 
        Windows, 
        Obstacles, 
        Shrinklaser, 
        Growthlaser, 
        Deathlaser, 
        Collectible, 
        Mechanical, 
        Essentials, 
        Floorborder, 
        Windowmask, 
        Gates, 
        Switches 
    }
    public enum TileMode { Tile, Gameobject}

    [Header("Tile")]
    [Tooltip("If set this tile will be placed down insted of the tile in the level editor")]
    public TileBase editorTile;
    [Tooltip("The tile which will be placed in both the editor and the level")]
    public TileBase tile;
    [Tooltip("A child tile of this tile, will be placed down and deleted with this tile")]
    public CustomTile extraTile;

    [Header("Gameobject")]
    [Tooltip("The gameobject which will be placed down in the level editor")]
    public GameObject editorGameObject;
    [Tooltip("The gameobject which will be placed down in the level")]
    public GameObject properGameObject;
    [Tooltip("Enable this if you only want one instance of this gameobject")]
    public bool onlyAllowOne;

    [Header("Display")]
    public string tileName;
    public Sprite previewImage;
}
