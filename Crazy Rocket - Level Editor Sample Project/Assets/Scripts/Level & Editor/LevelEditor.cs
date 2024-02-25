using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.IO;
using System.Linq;
using VinTools;
using VinTools.Tweening;

/// <summary>
/// Handles the modification of level data
/// </summary>
[RequireComponent(typeof(LevelManager))]
public class LevelEditor : MonoBehaviour
{
    public static LevelEditor instance;

    public enum DrawMode { Draw, Area, Copy, Paste, Fill, GameObject }
    public enum GameObjectMode { None, Select, Move, Rotate, Scale };

    [Header("Settings")]
    public static Memory.GameValues.GameMode editorMode;
    public DrawMode drawMode;
    public GameObjectMode gameObjectMode;
    public bool swipe;
    public bool delete;
    [Space]
    public int selectedTile;
    public bool pasteEmptySpaces = true;
    public bool onlyCopySelectedTileType = true;
    public bool onlyFillSelectedLayer = true;
    public bool prewievFill = false;

    [Header("References")]
    [SerializeField] Camera cam;
    [SerializeField] LineRenderer selectionOutline;
    [SerializeField] LineRenderer copyOutline;
    [SerializeField] SpriteRenderer selectionCircle;
    [SerializeField] LineRenderer pasteOutline;
    [SerializeField] ToggleButton[] drawModeButtons;
    [SerializeField] ToggleButton[] gameobjectModeButtons;
    [SerializeField] Tilemap prewievMap;
    [SerializeField] TileBase deleteTile;
    [SerializeField] ToggleButton undoButton;
    [SerializeField] ToggleButton redoButton;
    [SerializeField] RectTransform toolsHolder;
    [SerializeField] RectTransform gameObjectToolsHolder;

    Vector3 toolsShownPos;
    Vector3 toolsHiddenPos;

    [Header("Grid")]
    [SerializeField] LineRenderer gridRenderer;
    [SerializeField] int gridSize;
    
    [Header("Endless Editor")]
    [SerializeField] GameObject endlessGuides;
    [SerializeField] CustomTile backgroundTile;
    [SerializeField] CustomTile floorTile;

    [Header("Editor")]
    public LayerMask gameObjectMask;

    [Header("Scenes")]
    public Color fadeColor = Color.black;
    public int levelBuildIndex = 1;

    //private variables
    LevelManager manager;
    LevelClipboard clipboard;
    EditorActionLog editorActionLog = new EditorActionLog();
    EditorActionLog.EditorAction tempActionlog = new EditorActionLog.EditorAction();

    void Awake()
    {
        manager = GetComponent<LevelManager>();

        //set instance
        if (instance == null) instance = this;
        else Destroy(this);

        OrganizeTiles();

        //Memory.GameValues.CurrentLevelEditorMode = Memory.GameValues.GameMode.Level;
        editorMode = Memory.GameValues.CurrentLevelEditorMode;

        //
        Memory.GameValues.returnToTitleAfterTesting = false;
        Memory.GameValues.levelEditorTesting = false;
    }

    void RefreshToolButtons()
    {
        for (int i = 0; i < drawModeButtons.Length; i++)
        {
            if ((int)drawMode == i)
            {
                //Debug.Log("updated button to " + drawMode);
                drawModeButtons[i].On = true;
                drawModeButtons[i].button.onClick.Invoke();
            }
            else drawModeButtons[i].On = false;
        }
    }

    void RefreshGameobjectToolButtons()
    {
        for (int i = 0; i < gameobjectModeButtons.Length; i++)
        {
            if ((int)gameObjectMode == i)
            {
                gameobjectModeButtons[i].On = true;
                gameobjectModeButtons[i].button.onClick.Invoke();
            }
            else gameobjectModeButtons[i].On = false;
        }
    }

    private void Start()
    {
        SetupKeyboardShortcuts();
        
        toolsShownPos = toolsHolder.localPosition;
        toolsHiddenPos = gameObjectToolsHolder.localPosition;

        RefreshToolButtons();
        RefreshDoButtons();

        editorActionLog = new EditorActionLog();

        //setup grid
        List<Vector3> poses = new List<Vector3>();

        for (int x = -gridSize; x <= gridSize; x += 2)
        {
            poses.Add(new Vector3(x, -gridSize, 0));
            poses.Add(new Vector3(x, gridSize, 0));
            poses.Add(new Vector3(x + 1, gridSize, 0));
            poses.Add(new Vector3(x + 1, -gridSize, 0));
        }

        for (int y = -gridSize; y <= gridSize; y += 2)
        {
            poses.Add(new Vector3(gridSize, y, 0));
            poses.Add(new Vector3(-gridSize, y, 0));
            poses.Add(new Vector3(-gridSize, y + 1, 0));
            poses.Add(new Vector3(gridSize, y + 1, 0));
        }

        gridRenderer.positionCount = poses.Count;
        gridRenderer.SetPositions(poses.ToArray());

        //setup endless editor
        endlessGuides.SetActive(editorMode == Memory.GameValues.GameMode.Endless);

        if (editorMode == Memory.GameValues.GameMode.Endless)
        {
            //get floor tile
            selectedTile = LevelManager.instance.customtiles.IndexOf(floorTile);

            //place floors
            for (int x = 0; x < 25; x++)
            {
                for (int y = 11; y < 19; y++)
                {
                    PlaceTile(new Vector3Int(x, y, 0));
                }

                for (int y = -8; y < 0; y++)
                {
                    PlaceTile(new Vector3Int(x, y, 0));
                }
            }

            //get bg tile
            selectedTile = LevelManager.instance.customtiles.IndexOf(backgroundTile);

            //place bg
            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 12; y++)
                {
                    PlaceTile(new Vector3Int(x, y, 0));
                }
            }
        }
    }

    void Update()
    {
        //TileSelection();
        Editor();
        RefreshActionlog();
    }

    /*void TileSelection()
    {
        //change selected tile
        //if (Keyboard.current[Key.PageUp].wasPressedThisFrame) selectedTile += 1;
        //if (Keyboard.current[Key.PageDown].wasPressedThisFrame) selectedTile -= 1;

        //cycle at ends
        if (selectedTile < 0) selectedTile = LevelManager.instance.customtiles.Count - 1;
        if (selectedTile >= LevelManager.instance.customtiles.Count) selectedTile = 0;
    }*/

    void Editor()
    {
        //get tile position at cursor
        var mousepos = cam.ScreenToWorldPoint(InputManager.mousePos);
        var pos = manager.tilemaps[0].WorldToCell(mousepos);

        if (!InputManager.mouseOverUI)
        {
            switch (drawMode)
            {
                case DrawMode.Draw:
                    if (manager.customtiles[selectedTile].tilemode == CustomTile.TileMode.Tile) Editor_Draw(pos);
                    if (manager.customtiles[selectedTile].tilemode == CustomTile.TileMode.Gameobject) Editor_GameobjectPlacement(mousepos);
                    break;
                case DrawMode.Area:
                    if (manager.customtiles[selectedTile].tilemode == CustomTile.TileMode.Tile) Editor_Area(pos);
                    break;
                case DrawMode.Copy:
                    Editor_Copy(pos);
                    break;
                case DrawMode.Paste:
                    Editor_Paste(pos);
                    break;
                case DrawMode.Fill:
                    if (manager.customtiles[selectedTile].tilemode == CustomTile.TileMode.Tile) Editor_Fill(pos);
                    break;
                case DrawMode.GameObject:
                    Editor_GameObject(mousepos);
                    break;
                default:
                    break;
            }
        }

        //show line renderer for area selection
        var draw = (InputManager.mouseHold && !InputManager.mouseOverUI) && drawMode == DrawMode.Area;
        var col = delete ? Color.red : Color.white;
        DrawSelection(selectionOutline, draw, col, pos, dragStartPos);

        //show line renderer for copy & paste
        DrawSelection(copyOutline, drawMode == DrawMode.Copy, Color.blue, copyAreaStart, copyAreaEnd);
        DrawSelection(pasteOutline, drawMode == DrawMode.Paste && clipboard != null, Color.green, pasteAreaStart, pasteAreaStart + pasteAreaSize);

        //tile drawer
        if (isDrawerOpened)
        {
            if (!InputManager.mouseOverUI && (InputManager.mouseDown || InputManager.mouseDownRight)) TileSelection_Close();
        }

        //gameobject selection
        Color cols = selectionCircle.color;
        cols.a = selectedGameObject == null ? 0 : .5f;
        selectionCircle.color = cols;
        if (selectedGameObject != null) selectionCircle.transform.position = selectedGameObject.transform.position;
    }

    #region Placement modes
    //hold down place 
    void Editor_Draw(Vector3Int pos)
    {
        if (swipe)
        {
            //placing tiles
            if (InputManager.mouseHold && !delete)
            {
                PlaceTile(pos);
            }

            //removing tiles
            if (InputManager.mouseHold && delete)
            {
                RemoveTile(pos);
            }
        }
        else
        {
            if (!InputManager.wasMouseMoved)
            {
                //placing tiles
                if (InputManager.mouseUp && !delete)
                {
                    PlaceTile(pos);
                }

                //removing tiles
                if (InputManager.mouseUp && delete)
                {
                    RemoveTile(pos);
                }
            }
        }
    }

    //select a box area to fill
    void Editor_Area(Vector3Int pos)
    {
        Funcs.GetMinMax(pos, dragStartPos, out Vector3Int start, out Vector3Int end);

        if (!delete)
        {
            //placing tiles
            if (InputManager.mouseDown)
            {
                dragStartPos = pos;
            }
            if (InputManager.mouseUp)
            {
                for (int x = start.x; x <= end.x; x++)
                {
                    for (int y = start.y; y <= end.y; y++)
                    {
                        PlaceTile(new Vector3Int(x, y, pos.z));
                    }
                }
            }
        }
        else
        {
            //removing tiles
            if (InputManager.mouseDown)
            {
                dragStartPos = pos;
            }
            if (InputManager.mouseUp)
            {
                for (int x = start.x; x <= end.x; x++)
                {
                    for (int y = start.y; y <= end.y; y++)
                    {
                        RemoveTile(new Vector3Int(x, y, pos.z));
                    }
                }
            }
        }
    }
    Vector3Int dragStartPos;

    //copy an area
    void Editor_Copy(Vector3Int pos)
    {
        //setting area boundaries
        if (InputManager.mouseDown)
        {
            copyAreaStart = pos;
        }
        if (InputManager.mouseHold)
        {
            copyAreaEnd = pos;
        }
    }
    Vector3Int copyAreaStart;
    Vector3Int copyAreaEnd;

    //paste a the currently copied area
    void Editor_Paste(Vector3Int pos)
    {
        //change position of selection
        if (!InputManager.wasMouseMoved && InputManager.mouseHold)
        {
            //set start
            pasteAreaStart = pos;
        }

        //set end based on the copied area size
        Funcs.GetMinMax(copyAreaEnd, copyAreaStart, out var cmin, out var cmax);
        if (clipboard == null || clipboard.tilemaps.Length < 1) pasteAreaSize = cmax - cmin;
        else pasteAreaSize = clipboard.tilemaps[0].bounds.size - new Vector3Int(1, 1, 0);
    }
    Vector3Int pasteAreaStart;
    Vector3Int pasteAreaSize;

    void Editor_Fill(Vector3Int pos)
    {
        //refresh the area if moved cursor to a different place
        if ((lastpos != pos && prewievFill) || InputManager.mouseDown)
        {
            //refresh the fill area
            lastpos = pos;
            Editor_RefreshFillArea(pos, GetTileOnPos(pos));

            //show the preview tiles
            prewievMap.ClearAllTiles();
            foreach (var item in placesToFill)
            {
                if (!delete) ShowPreviewTile(item);
                else prewievMap.SetTile(item, deleteTile);
            }
        }

        //use tool
        if (InputManager.mouseUp && !InputManager.wasMouseMoved)
        {
            if (!delete)
            {
                //clear the prewiev
                prewievMap.ClearAllTiles();
                //place tiles
                foreach (var item in placesToFill)
                {
                    PlaceTile(item);
                }
            }
            else
            {
                //clear the prewiev
                prewievMap.ClearAllTiles();
                //remove tiles
                foreach (var item in placesToFill)
                {
                    RemoveTile(item);
                }
            }
        }
    }
    Vector3Int lastpos = Vector3Int.zero * int.MaxValue;
    List<Vector3Int> placesToFill = new List<Vector3Int>();
    List<Vector3Int> placesToCheck = new List<Vector3Int>();

    void Editor_RefreshFillArea(Vector3Int startpos, TileBase tileToFill)
    {
        //clear the previous tiles
        placesToCheck.Clear();
        placesToFill.Clear();

        //add the starting position to the arrays
        placesToCheck.Add(startpos);
        placesToFill.Add(startpos);

        //check every tile in the array while it's not empty
        while (placesToCheck.Count > 0)
        {
            var item = placesToCheck[0];

            if (GetIfSame(item + Vector3Int.right, tileToFill) && !placesToFill.Contains(item + Vector3Int.right))
            {
                placesToCheck.Add(item + Vector3Int.right);
                placesToFill.Add(item + Vector3Int.right);
            }
            if (GetIfSame(item + Vector3Int.down, tileToFill) && !placesToFill.Contains(item + Vector3Int.down))
            {
                placesToCheck.Add(item + Vector3Int.down);
                placesToFill.Add(item + Vector3Int.down);
            }
            if (GetIfSame(item + Vector3Int.left, tileToFill) && !placesToFill.Contains(item + Vector3Int.left))
            {
                placesToCheck.Add(item + Vector3Int.left);
                placesToFill.Add(item + Vector3Int.left);
            }
            if (GetIfSame(item + Vector3Int.up, tileToFill) && !placesToFill.Contains(item + Vector3Int.up))
            {
                placesToCheck.Add(item + Vector3Int.up);
                placesToFill.Add(item + Vector3Int.up);
            }

            placesToCheck.Remove(item);

            //prevent the tool from filling unlimited tiles
            if (placesToFill.Count > 1012) break;
        }
    }

    bool GetIfSame(Vector3Int pos, TileBase tile)
    {
        for (int i = 0; i < manager.tilemaps.Count; i++)
        {
            //skip if the seted layer is not this
            if (onlyFillSelectedLayer && i != (int)LevelManager.instance.customtiles[selectedTile].tilemap) continue;
            //skip iteration if we want to ignore that layer

            //if something is there return true
            if (tile != null)
            {
                if (manager.tilemaps[i].GetTile(pos) == tile) return true;
            }
            else if (manager.tilemaps[i].GetTile(pos) != null) return false;
        }

        //if no obstacles return false
        if (tile != null) return false;
        else return true;
    }

    TileBase GetTileOnPos(Vector3Int pos)
    {
        for (int i = 0; i < manager.tilemaps.Count; i++)
        {
            //skip if the seted layer is not this
            if (onlyFillSelectedLayer && i != (int)LevelManager.instance.customtiles[selectedTile].tilemap) continue;
            //skip iteration if we want to ignore that layer
            if (i == (int)CustomTile.MapLayer.Floorborder) continue;

            var tile = manager.tilemaps[i].GetTile(pos);
            if (tile != null) return tile;
        }

        return null;
    }

    void Editor_GameobjectPlacement(Vector3 mousepos)
    {
        if (!InputManager.wasMouseMoved || swipe)
        {
            //placing tiles
            if (InputManager.mouseUp && !delete)
            {
                PlaceGameobject(mousepos);
            }

            //preview gameobject
            if (!delete && InputManager.mouseHold)
            {
                if (previewGameObject != null) Destroy(previewGameObject);

                previewGameObject = Instantiate(manager.customtiles[selectedTile].editorGameObject, Funcs.AlignToGridPosition(new Vector3(mousepos.x, mousepos.y, 0), 12), Quaternion.Euler(Vector3.zero));
                SpriteRenderer rend = previewGameObject.GetComponent<SpriteRenderer>();
                Color col = rend.color;
                col.a = 0.5f;
                rend.color = col;
            }

            //removing tiles
            if (InputManager.mouseUp && delete)
            {
                DestroyGameObject(mousepos);
            }

            //preview selected gameobject
            if (delete && InputManager.mouseHold)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector3.forward, gameObjectMask);

                if (highlightedGameobject != null) highlightedGameobject.color = new Color(1, 1, 1, 1);
                if (hit.collider != null)
                {
                    highlightedGameobject = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                    highlightedGameobject.color = new Color(1, .5f, .5f, 1);
                }
            }
        }

        if (InputManager.mouseUp)
        {
            if (previewGameObject != null) Destroy(previewGameObject);
            if (highlightedGameobject != null)
            {
                highlightedGameobject.color = new Color(1, 1, 1, 1);
                highlightedGameobject = null;
            }
        }
    }


    void Editor_GameObject(Vector3 mousepos)
    {
        switch (gameObjectMode)
        {
            case GameObjectMode.None:
                return;
            case GameObjectMode.Select:
                EditGameObject_Select(mousepos);
                break;
            case GameObjectMode.Move:
                EditGameObject_Move(mousepos);
                break;
            case GameObjectMode.Rotate:
                EditGameObject_Rotate(mousepos);
                break;
            case GameObjectMode.Scale:
                EditGameObject_Scale(mousepos);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Tile placement
    void ShowPreviewTile(Vector3Int pos)
    {
        //set the selected tile to it's map
        prewievMap.SetTile(pos, LevelManager.instance.customtiles[selectedTile].tile);
    }
    void PlaceTile(Vector3Int pos)
    {
        //set the selected tile to it's map
        int tilemapIndex = (int)LevelManager.instance.customtiles[selectedTile].tilemap;

        tempActionlog.AddAction_TilemapChange(tilemapIndex, manager.tilemaps[tilemapIndex].GetTile(pos), LevelManager.instance.customtiles[selectedTile].tile, pos);

        TileBase t = LevelManager.instance.customtiles[selectedTile].editorTile == null ? LevelManager.instance.customtiles[selectedTile].tile : LevelManager.instance.customtiles[selectedTile].editorTile;
        manager.tilemaps[tilemapIndex].SetTile(pos, t);

        //set border
        if (LevelManager.instance.customtiles[selectedTile].extraTile != null)
        {
            //get the tilemap of the extra tile and set the tile
            tilemapIndex = (int)LevelManager.instance.customtiles[selectedTile].extraTile.tilemap;

            tempActionlog.AddAction_TilemapChange(tilemapIndex, manager.tilemaps[tilemapIndex].GetTile(pos), LevelManager.instance.customtiles[selectedTile].extraTile.tile, pos);

            TileBase te = LevelManager.instance.customtiles[selectedTile].extraTile.editorTile == null ? LevelManager.instance.customtiles[selectedTile].extraTile.tile : LevelManager.instance.customtiles[selectedTile].extraTile.editorTile;
            manager.tilemaps[tilemapIndex].SetTile(pos, te);
        }
    }
    void RemoveTile(Vector3Int pos)
    {
        //delete the tile on the selected tile's tilemap
        int tilemapIndex = (int)LevelManager.instance.customtiles[selectedTile].tilemap;

        tempActionlog.AddAction_TilemapChange(tilemapIndex, manager.tilemaps[tilemapIndex].GetTile(pos), null, pos);

        manager.tilemaps[tilemapIndex].SetTile(pos, null);

        //set border
        if (LevelManager.instance.customtiles[selectedTile].extraTile != null)
        {
            //get the tilemap of the extra tile and delete the tile at the position
            tilemapIndex = (int)LevelManager.instance.customtiles[selectedTile].extraTile.tilemap;

            tempActionlog.AddAction_TilemapChange(tilemapIndex, manager.tilemaps[tilemapIndex].GetTile(pos), null, pos);

            manager.tilemaps[tilemapIndex].SetTile(pos, null);
        }
    }

    void PlaceGameobject(Vector3 pos)
    {
        if (manager.customtiles[selectedTile].editorGameObject == null) return;

        //if only allow one of that gameobject
        if (manager.customtiles[selectedTile].onlyAllowOne)
        {
            foreach (EditorGameObject item in FindObjectsOfType<EditorGameObject>())
            {
                if (item.tileID == selectedTile)
                {
                    tempActionlog.AddAction_DestroyGameObject(item.tileID, item.transform.position, item.instanceID, item.transform.eulerAngles.z, item.transform.localScale);

                    Destroy(item.gameObject);
                }
            }
        }

        //instantiate the gameobject
        GameObject go = Instantiate(manager.customtiles[selectedTile].editorGameObject, Funcs.AlignToGridPosition(new Vector3(pos.x, pos.y, 0), 12), Quaternion.Euler(Vector3.zero));
        EditorGameObject ego = go.GetComponent<EditorGameObject>();
        ego.tileID = selectedTile;
        ego.instanceID = Time.time;
        ego.Instantiate();

        int i = 0;
        foreach (var item in ego.childs)
        {
            item.tileID = selectedTile;
            item.instanceID = Time.time + (float)i / 1000;
            item.transform.parent = null;

            i++;
        }

        tempActionlog.AddAction_PlaceGameObject(ego.tileID, Funcs.AlignToGridPosition(new Vector3(pos.x, pos.y, 0), 12), ego.instanceID);
    }

    void DestroyGameObject(Vector3 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.forward, gameObjectMask);

        if (hit.collider != null)
        {
            Transform t = hit.collider.transform;
            EditorGameObject ego = hit.collider.GetComponent<EditorGameObject>();

            if (ego.isChild)
            {
                Destroy(ego.parent.gameObject);
            }
            if (ego.saveChildTransform)
            {
                foreach (var item in ego.childs)
                {
                    Destroy(item.gameObject);
                }
            }

            //TODO make parented objects compatibble with undo and redo
            if (!ego.saveChildTransform && !ego.isChild) tempActionlog.AddAction_DestroyGameObject(ego.tileID, t.position, ego.instanceID, t.eulerAngles.z, t.localScale);

            Destroy(hit.collider.gameObject);
        }
    }

    public void Copy()
    {
        if (drawMode != DrawMode.Copy) return;

        //get bounds of copy area
        Funcs.GetMinMax(copyAreaEnd, copyAreaStart, out var cmin, out var cmax);
        BoundsInt bounds = new BoundsInt(cmin, cmax - cmin + new Vector3Int(1, 1, 0));

        //create a new level
        LevelClipboard data = new LevelClipboard(manager.tilemaps.Count);

        //for every tilemap layer
        for (int i = 0; i < manager.tilemaps.Count; i++)
        {
            //get the bounds
            data.tilemaps[i].bounds = bounds;
            //get the tiles
            data.tilemaps[i].tiles = new TileBase[bounds.size.x * bounds.size.y];

            for (int t = 0, x = bounds.min.x; x < bounds.max.x; x++)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    data.tilemaps[i].tiles[t] = manager.tilemaps[i].GetTile(new Vector3Int(x, y, bounds.z));
                    t++;
                }
            }
        }

        //set clipboard
        clipboard = data;
    }
    public void Paste()
    {
        if (drawMode != DrawMode.Paste) return;

        //return if clipboard empty
        if (clipboard == null) return;

        //get bounds of paste area
        BoundsInt bounds = new BoundsInt(pasteAreaStart, pasteAreaSize);

        //for every tilemap layer
        for (int i = 0; i < clipboard.tilemaps.Length; i++)
        {
            bool isCurrentLayerSelected = (int)LevelManager.instance.customtiles[selectedTile].tilemap == i || (LevelManager.instance.customtiles[selectedTile].extraTile != null && (int)LevelManager.instance.customtiles[selectedTile].extraTile.tilemap == i);
            bool paste = isCurrentLayerSelected || !onlyCopySelectedTileType;

            /*switch (i)
            {
                case (int)CustomTile.MapLayer.Background:
                    if (paste) PasteByTile(clipboard.tilemaps[i], manager.tilemaps[i], tiles[backgroundTileIndex], bounds, !pasteEmptySpaces);
                    break;
                case (int)CustomTile.MapLayer.Floor:
                    if (paste) PasteByTile(clipboard.tilemaps[i], manager.tilemaps[i], tiles[FloorTileIndex], bounds, !pasteEmptySpaces);
                    break;
                default:
                    if (paste) PasteArea(clipboard.tilemaps[i], manager.tilemaps[i], bounds, !pasteEmptySpaces);
                    break;
            }*/

            if (paste) PasteArea(clipboard.tilemaps[i], manager.tilemaps[i], bounds, !pasteEmptySpaces);
        }
    }
    void PasteArea(LevelClipboard.TilemapLayer clipboard, Tilemap tilemap, BoundsInt bounds, bool skipEmptySpaces)
    {
        //make sure the bounds size is the same
        bounds.size = clipboard.bounds.size;

        for (int t = 0, x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                if (!skipEmptySpaces || clipboard.tiles[t] != null)
                {
                    var pos = new Vector3Int(x, y, bounds.z);

                    tempActionlog.AddAction_TilemapChange(manager.tilemaps.IndexOf(tilemap), tilemap.GetTile(pos), clipboard.tiles[t], pos);
                    tilemap.SetTile(pos, clipboard.tiles[t]);
                }

                t++;
            }
        }
    }

    /*void PasteByTile(LevelClipboard.TilemapLayer clipboard, Tilemap tilemap, CustomTile tile, BoundsInt bounds, bool skipEmptySpaces)
    {
        //make sure the bounds size is the same
        bounds.size = clipboard.bounds.size;

        for (int t = 0, x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                if (clipboard.tiles[t] != null) tilemap.SetTile(new Vector3Int(x, y, bounds.z), tile.tile);
                else if (!skipEmptySpaces) tilemap.SetTile(new Vector3Int(x, y, bounds.z), null);

                t++;
            }
        }
    }*/
    #endregion

    #region GameObject manipulation
    GameObject previewGameObject;
    SpriteRenderer highlightedGameobject;
    EditorGameObject selectedGameObject;
    bool canEditGameObject;
    Vector2 gameobjectStartPos;
    Vector2 mouseEditStartOffset;
    float gameobjectStartRotation;
    Vector2 gameObjectStartScale;

    [Header("GameObject Placement Settings")]
    public bool holdToSelect;
    public bool workOutsideCircle;

    public void EditGameObject_Select(Vector3 pos)
    {
        if (!InputManager.wasMouseMoved || holdToSelect)
        {
            //select gameobject
            if (InputManager.mouseUp)
            {
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.forward, gameObjectMask);

                if (hit.collider != null) selectedGameObject = hit.collider.GetComponent<EditorGameObject>();
            }

            //preview selected gameobject
            if (InputManager.mouseHold)
            {
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.forward, gameObjectMask);

                if (highlightedGameobject != null) highlightedGameobject.color = new Color(1, 1, 1, 1);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.TryGetComponent(out highlightedGameobject))
                        highlightedGameobject.color = new Color(.5f, 1f, .5f, 1);
                }
            }
        }

        if (InputManager.mouseUp || (InputManager.wasMouseMoved && !holdToSelect))
        {
            if (highlightedGameobject != null)
            {
                highlightedGameobject.color = new Color(1, 1, 1, 1);
                highlightedGameobject = null;
            }
        }
    }
    public void EditGameObject_Move(Vector2 pos)
    {
        if (selectedGameObject == null) return;

        if (InputManager.mouseDown)
        {
            gameobjectStartPos = selectedGameObject.transform.position;
            mouseEditStartOffset = gameobjectStartPos - pos;
            canEditGameObject = mouseEditStartOffset.magnitude <= 2 || workOutsideCircle;
        }

        if (canEditGameObject && InputManager.mouseHold)
        {
            selectedGameObject.transform.position = Funcs.AlignToGridPosition(pos + mouseEditStartOffset, 12);
        }
        if (canEditGameObject && InputManager.mouseUp)
        {
            tempActionlog.AddAction_MoveGameObject(gameobjectStartPos, selectedGameObject.transform.position, selectedGameObject.instanceID);
        }
    }
    public void EditGameObject_Rotate(Vector2 pos)
    {
        if (selectedGameObject == null) return;

        if (InputManager.mouseDown)
        {
            gameobjectStartRotation = selectedGameObject.transform.eulerAngles.z;
            mouseEditStartOffset = (Vector2)selectedGameObject.transform.position - pos;
            canEditGameObject = mouseEditStartOffset.magnitude <= 2 || workOutsideCircle;
        }
            
        if (canEditGameObject && InputManager.mouseHold)
        {
            Quaternion diff = Quaternion.Euler(0, 0, Math.Vector2ToDegree(mouseEditStartOffset)) * Quaternion.Inverse(Quaternion.Euler(0, 0, Math.Vector2ToDegree((Vector2)selectedGameObject.transform.position - pos)));
            Quaternion target = Quaternion.Euler(0, 0, gameobjectStartRotation) * Quaternion.Inverse(diff);

            //snap to increments of 15
            selectedGameObject.transform.eulerAngles = new Vector3(0, 0, (int)(target.eulerAngles.z / 15) * 15);
        }
        if (canEditGameObject && InputManager.mouseUp)
        {
            tempActionlog.AddAction_RotateGameObject(gameobjectStartRotation, selectedGameObject.transform.eulerAngles.z, selectedGameObject.instanceID);
        }
    }
    public void EditGameObject_Scale(Vector2 pos)
    {
        if (selectedGameObject == null) return;

        if (InputManager.mouseDown)
        {
            gameObjectStartScale = selectedGameObject.transform.localScale;
            mouseEditStartOffset = (Vector2)selectedGameObject.transform.position - pos;
        }

        if (InputManager.mouseHold)
        {
            Vector2 drag = (Vector2)selectedGameObject.transform.position - pos;
            Vector2 offset = mouseEditStartOffset - drag;

            bool editX = Mathf.Abs(offset.x / offset.y) > 1f;
            bool editY = Mathf.Abs(offset.y / offset.x) > 1f;

            float newXscale = editX ?
                Mathf.Clamp((int)(gameObjectStartScale.x / mouseEditStartOffset.x * drag.x * 10) / 10f, 0.1f, 100) :
                gameObjectStartScale.x;
            float newYscale = editY ?
                Mathf.Clamp((int)(gameObjectStartScale.y / mouseEditStartOffset.y * drag.y * 10) / 10f, 0.1f, 100) :
                gameObjectStartScale.y;

            selectedGameObject.transform.localScale = new Vector3(newXscale, newYscale, 1);
        }
        if (InputManager.mouseUp)
        {
            tempActionlog.AddAction_ScaleGameObject(gameObjectStartScale, selectedGameObject.transform.localScale, selectedGameObject.instanceID);
        }
    }
    #endregion

    #region Selection area display
    void DrawSelection(LineRenderer renderer) => DrawSelection(renderer, false, Color.white, Vector3Int.zero, Vector3Int.zero);
    void DrawSelection(LineRenderer renderer, bool draw, Color color, Vector3Int pos1, Vector3Int pos2)
    {
        var pos1float = manager.tilemaps[0].CellToWorld(pos1);
        var pos2float = manager.tilemaps[0].CellToWorld(pos2);

        Funcs.GetMinMax(pos1float, pos2float, out var min, out var max);
        max += new Vector3(2, 2, 0);

        if (!draw)
        {
            renderer.enabled = false;
            return;
        }
        else
        {
            //set colrs
            renderer.startColor = color;
            renderer.endColor = color;

            //set positions
            renderer.positionCount = 4;
            renderer.SetPosition(0, new Vector3(min.x, min.y, 0));
            renderer.SetPosition(1, new Vector3(max.x, min.y, 0));
            renderer.SetPosition(2, new Vector3(max.x, max.y, 0));
            renderer.SetPosition(3, new Vector3(min.x, max.y, 0));

            //enable
            renderer.enabled = true;
        }
    }
    #endregion

    #region functions
    #endregion

    #region UI Buttons
    [Header("UI Buttons")]
    [SerializeField] RectTransform Button_Delete;
    [SerializeField] RectTransform Button_Swipe;
    [SerializeField] RectTransform Button_FillSelectedLayer;
    [SerializeField] RectTransform Button_PreviewFill;
    [SerializeField] RectTransform Button_Copy;
    [SerializeField] RectTransform Button_SavePreset;
    [SerializeField] RectTransform Button_PasteSelectedLayer;
    [SerializeField] RectTransform Button_PasteAir;
    [SerializeField] RectTransform Button_Paste;
    [Space]
    [SerializeField] RectTransform GOButton_Swipe;
    [SerializeField] RectTransform GOButton_OutsideCircle;
    [SerializeField] RectTransform GOButton_ResetScale;
    [Space]
    [SerializeField] float Button_HiddenPos = 20;
    [SerializeField] float Button_ShownPos = -20;

    public void Toggle_Swipe(bool value) => swipe = value;
    public void Toggle_Delete(bool value) => delete = value;
    public void Toggle_PasteEmpySpaces(bool value) => pasteEmptySpaces = value;
    public void Toggle_LayerPaste(bool value) => onlyCopySelectedTileType = value;
    public void Toggle_LayerFill(bool value) => onlyFillSelectedLayer = value;
    public void Toggle_PrewievFill(bool value) => prewievFill = value;

    public void Toggle_GameObjectSwipe(bool value) => holdToSelect = value;
    public void Toggle_WorkOutsideCircle(bool value) => workOutsideCircle = value;
    public void Toggle_ResetScale()
    {
        if (selectedGameObject != null)
        {
            selectedGameObject.transform.localScale = Vector3.one;
        }
    }

    public void SetDrawMode_Draw()
    {
        drawMode = DrawMode.Draw;
        prewievMap.ClearAllTiles();
        
        Button_Delete.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        Button_Swipe.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        Button_FillSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PreviewFill.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Copy.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_SavePreset.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteAir.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Paste.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
    }
    public void SetDrawMode_Area()
    {
        drawMode = DrawMode.Area;
        prewievMap.ClearAllTiles();

        Button_Delete.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        Button_Swipe.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_FillSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PreviewFill.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Copy.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_SavePreset.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteAir.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Paste.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
    }
    public void SetDrawMode_Fill()
    {
        drawMode = DrawMode.Fill;
        prewievMap.ClearAllTiles();

        Button_Delete.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        Button_Swipe.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_FillSelectedLayer.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        Button_PreviewFill.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        Button_Copy.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_SavePreset.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteAir.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Paste.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
    }
    public void SetDrawMode_Copy()
    {
        drawMode = DrawMode.Copy;
        prewievMap.ClearAllTiles();

        Button_Delete.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Swipe.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_FillSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PreviewFill.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Copy.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        Button_SavePreset.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        Button_PasteSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteAir.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Paste.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
    }
    public void SetDrawMode_Paste()
    {
        drawMode = DrawMode.Paste;
        prewievMap.ClearAllTiles();

        Button_Delete.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Swipe.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_FillSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PreviewFill.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Copy.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_SavePreset.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteSelectedLayer.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic(); 
        Button_PasteAir.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        Button_Paste.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
    }
    public void SetDrawMode_Gameobject()
    {
        drawMode = DrawMode.GameObject;
        prewievMap.ClearAllTiles();

        gameObjectMode = GameObjectMode.Select;
        ShowTools_GameObject();

        Button_Delete.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Swipe.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_FillSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PreviewFill.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Copy.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_SavePreset.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteSelectedLayer.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_PasteAir.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        Button_Paste.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
    }

    //zoom buttons
    public void ZoomIn() => EditorCamera.instance.IncreaseZoom(1);
    public void ZoomOut() => EditorCamera.instance.IncreaseZoom(-1);

    //switch left bar
    public void ShowTools_Default()
    {
        toolsHolder.TweenMoveLocal(toolsShownPos, 0.2f).SetEaseInOutCubic();
        gameObjectToolsHolder.TweenMoveLocal(toolsHiddenPos, 0.2f).SetEaseInOutCubic();

        RefreshToolButtons();
    }
    public void ShowTools_GameObject()
    {
        gameObjectToolsHolder.TweenMoveLocal(toolsShownPos, 0.2f).SetEaseInOutCubic();
        toolsHolder.TweenMoveLocal(toolsHiddenPos, 0.2f).SetEaseInOutCubic();

        RefreshGameobjectToolButtons();
    }

    //set gameobject modes
    public void SetGameObjectMode_None()
    {
        gameObjectMode = GameObjectMode.None;
        selectedGameObject = null;

        drawMode = DrawMode.Draw;
        ShowTools_Default();

        GOButton_Swipe.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        GOButton_OutsideCircle.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        GOButton_ResetScale.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
    }
    public void SetGameObjectMode_Select()
    {
        gameObjectMode = GameObjectMode.Select;

        GOButton_Swipe.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        GOButton_OutsideCircle.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        GOButton_ResetScale.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
    }
    public void SetGameObjectMode_Move()
    {
        if (selectedGameObject == null)
        {
            gameObjectMode = GameObjectMode.Select;
            RefreshGameobjectToolButtons();
        }
        else
        {
            gameObjectMode = GameObjectMode.Move;

            GOButton_Swipe.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
            GOButton_OutsideCircle.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
            GOButton_ResetScale.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        }
    }
    public void SetGameObjectMode_Rotate()
    {
        if (selectedGameObject == null)
        {
            gameObjectMode = GameObjectMode.Select;
            RefreshGameobjectToolButtons();
        }
        else
        {
            gameObjectMode = GameObjectMode.Rotate;

            GOButton_Swipe.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
            GOButton_OutsideCircle.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
            GOButton_ResetScale.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
        }
    }
    public void SetGameObjectMode_Scale()
    {
        if (selectedGameObject == null)
        {
            gameObjectMode = GameObjectMode.Select;
            RefreshGameobjectToolButtons();
        }
        else
        {
            gameObjectMode = GameObjectMode.Scale;

            GOButton_Swipe.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
            GOButton_OutsideCircle.TweenMoveLocalX(Button_HiddenPos, 0.2f).SetEaseInOutCubic();
            GOButton_ResetScale.TweenMoveLocalX(Button_ShownPos, 0.2f).SetEaseInOutCubic();
        }
    }
    #endregion

    #region Keyboard shortcuts
    void SetupKeyboardShortcuts()
    {
        InputManager.Editor_Copy.AddListener(Copy);
        InputManager.Editor_Paste.AddListener(Paste);
        InputManager.Editor_Undo.AddListener(Undo);
        InputManager.Editor_Redo.AddListener(Redo);
        InputManager.Editor_ZoomIn.AddListener(ZoomIn);
        InputManager.Editor_ZoomOut.AddListener(ZoomOut);
        //InputManager.Editor_PlayTest.AddListener();

        InputManager.Editor_SelectDraw.AddListener(Shortcut_Draw);
        InputManager.Editor_SelectArea.AddListener(Shortcut_Box);
        InputManager.Editor_SelectFill.AddListener(Shortcut_Fill);
        InputManager.Editor_SelectCopy.AddListener(Shortcut_Copy);
        InputManager.Editor_SelectPaste.AddListener(Shortcut_Paste);
        InputManager.Editor_SelectGameobjectSelect.AddListener(Shortcut_SelectGO);
        InputManager.Editor_SelectGameobjectMove.AddListener(Shortcut_MoveGO);
        InputManager.Editor_SelectGameobjectRotate.AddListener(Shortcut_RotateGO);
        InputManager.Editor_SelectGameobjectScale.AddListener(Shortcut_ScaleGO);
    }

    public void Shortcut_Draw()
    {
        drawMode = DrawMode.Draw;
        gameObjectMode = GameObjectMode.None;
        RefreshToolButtons();
        RefreshGameobjectToolButtons();
        ShowTools_Default();
    }
    public void Shortcut_Box()
    {
        drawMode = DrawMode.Area;
        gameObjectMode = GameObjectMode.None;
        RefreshToolButtons();
        RefreshGameobjectToolButtons();
        ShowTools_Default();
    }
    public void Shortcut_Fill()
    {
        drawMode = DrawMode.Fill;
        gameObjectMode = GameObjectMode.None;
        RefreshToolButtons();
        RefreshGameobjectToolButtons();
        ShowTools_Default();
    }
    public void Shortcut_Copy()
    {
        drawMode = DrawMode.Copy;
        gameObjectMode = GameObjectMode.None;
        RefreshToolButtons();
        RefreshGameobjectToolButtons();
        ShowTools_Default();
    }
    public void Shortcut_Paste()
    {
        drawMode = DrawMode.Paste;
        gameObjectMode = GameObjectMode.None;
        RefreshToolButtons();
        RefreshGameobjectToolButtons();
        ShowTools_Default();
    }
    public void Shortcut_SelectGO()
    {
        drawMode = DrawMode.GameObject;
        gameObjectMode = GameObjectMode.Select;
        RefreshToolButtons();
        RefreshGameobjectToolButtons();
        ShowTools_GameObject();
    }
    public void Shortcut_MoveGO()
    {
        drawMode = DrawMode.GameObject;
        gameObjectMode = GameObjectMode.Move;
        RefreshToolButtons();
        RefreshGameobjectToolButtons();
        ShowTools_GameObject();
    }
    public void Shortcut_RotateGO()
    {
        drawMode = DrawMode.GameObject;
        gameObjectMode = GameObjectMode.Rotate;
        RefreshToolButtons();
        RefreshGameobjectToolButtons();
        ShowTools_GameObject();
    }
    public void Shortcut_ScaleGO()
    {
        drawMode = DrawMode.GameObject;
        gameObjectMode = GameObjectMode.Scale;
        RefreshToolButtons();
        RefreshGameobjectToolButtons();
        ShowTools_GameObject();
    }

    #endregion

    #region Tile selection UI
    [Header("Tile selection UI")]
    public RectTransform TileDrawer;
    public int tileDrawerOpenHeight = -179;
    public int tileDrawerClosedHeight = -253;
    bool isDrawerOpened;

    [Space]
    public Text drawerTitle;
    public string[] drawerTitles;
    public Image[] drawerSlots;
    public ToggleButton leftArrow;
    public ToggleButton rightArrow;
    int selectedDrawerPage = 0;
    int selectedDrawer = 0;

    CustomTile[] currentDisplayedTiles;
    List<CustomTile>[] organizedTiles;

    void OrganizeTiles()
    {
        //set up lists
        organizedTiles = new List<CustomTile>[8];
        for (int i = 0; i < organizedTiles.Length; i++)
        {
            organizedTiles[i] = new List<CustomTile>();
        }

        //
        currentDisplayedTiles = new CustomTile[drawerSlots.Length];

        //sort tiles
        foreach (var tile in LevelManager.instance.customtiles)
        {
            switch (tile.tilemap)
            {
                case CustomTile.MapLayer.Background:
                    organizedTiles[0].Add(tile);
                    break;
                case CustomTile.MapLayer.Floor:
                    organizedTiles[1].Add(tile);
                    break;
                case CustomTile.MapLayer.Windows:
                    organizedTiles[2].Add(tile);
                    break;
                case CustomTile.MapLayer.Obstacles:
                    organizedTiles[3].Add(tile);
                    break;
                case CustomTile.MapLayer.Shrinklaser:
                    organizedTiles[4].Add(tile);
                    break;
                case CustomTile.MapLayer.Growthlaser:
                    organizedTiles[4].Add(tile);
                    break;
                case CustomTile.MapLayer.Deathlaser:
                    organizedTiles[4].Add(tile);
                    break;
                case CustomTile.MapLayer.Collectible:
                    organizedTiles[5].Add(tile);
                    break;
                case CustomTile.MapLayer.Mechanical:
                    organizedTiles[7].Add(tile);
                    break;
                case CustomTile.MapLayer.Essentials:
                    organizedTiles[6].Add(tile);
                    break;
                case CustomTile.MapLayer.Gates:
                    organizedTiles[7].Add(tile);
                    break;
                case CustomTile.MapLayer.Switches:
                    organizedTiles[7].Add(tile);
                    break;
                default:
                    break;
            }
        }
    }

    public void TileSelection_Open(int index)
    {
        //animation
        TileDrawer.TweenMoveLocalY(tileDrawerOpenHeight, 0.2f).SetEaseInOutCubic();
        isDrawerOpened = true;

        //set title
        drawerTitle.text = drawerTitles[index];

        //set tiles
        selectedDrawer = index;
        selectedDrawerPage = 0;
        ChangePage(0);
    }
    public void ChangePage(int difference)
    {
        //change the selected page
        selectedDrawerPage += difference;

        //
        var listoftiles = organizedTiles[selectedDrawer];

        //set arrow buttons
        bool leftstate = selectedDrawerPage > 0;
        leftArrow.On = leftstate;
        leftArrow.interactable = leftstate;
        bool rightstate = listoftiles.Count > (selectedDrawerPage + 1) * drawerSlots.Length;
        rightArrow.On = rightstate;
        rightArrow.interactable = rightstate;

        //set display
        for (int i = 0; i < drawerSlots.Length; i++)
        {
            //get tile index
            int index = i + selectedDrawerPage * drawerSlots.Length;

            if (index  < listoftiles.Count)
            {
                drawerSlots[i].sprite = listoftiles[index].previewImage;
                currentDisplayedTiles[i] = listoftiles[index];
            }
            else
            {
                drawerSlots[i].sprite = null;
                currentDisplayedTiles[i] = null;
            }
        }
    }
    public void SelectTile(int index)
    {
        if (currentDisplayedTiles[index] != null)
        {
            selectedTile = LevelManager.instance.customtiles.IndexOf(currentDisplayedTiles[index]);

            //close drawer
            TileSelection_Close();
        }
    }
    public void TileSelection_Close()
    {
        TileDrawer.TweenMoveLocalY(tileDrawerClosedHeight, 0.2f).SetEaseInOutCubic();
        isDrawerOpened = false;
    }

    #endregion

    #region Screenshot
    [Header("Screenshot")]
    public GameObject UI;

    public void TakeScreenshot()
    {
        StartCoroutine(LoadScreenshot());
    }

    public IEnumerator LoadScreenshot()
    {
        string folder = "CustomLevels";
        string fileName = "ScreenCapture";
        string directoryPath = $"{Application.persistentDataPath}{(folder == null ? "" : $"/{folder}")}";

        //take screenshot
        UI.SetActive(false);
#if UNITY_EDITOR
        ScreenCapture.CaptureScreenshot($"Assets/{folder}/{fileName}.png");
#else
        ScreenCapture.CaptureScreenshot($"{folder}/{fileName}.png");
#endif

        //create folder if not existing
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        //yield return new WaitForEndOfFrame();

        //wait for a second to save the screenshot
        yield return new WaitForSecondsRealtime(.5f);

        Debug.Log("//load screenshot");
        byte[] bytes = File.ReadAllBytes($"{directoryPath}/{fileName}.png");

        Debug.Log("//load the bytes into a texture2D");
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.LoadImage(bytes, false);

        Debug.Log("//resize the texture //set the width to 233");
        float scaleFactor = 233f / tex.width;
        TextureScale.Bilinear(tex, 233, (int)(tex.height * scaleFactor));

        Debug.Log("//encode back to png");
        bytes = tex.EncodeToPNG();

        Debug.Log("//save screenshot");
        LevelEditorSaveUI.instance.screenshot = bytes;
        LevelEditorSaveUI.instance.screenshotResolution = new Vector2Int(tex.width, tex.height);

        Debug.Log("//delete file");
        File.Delete($"{directoryPath}/{fileName}.png");

        UI.SetActive(true);
    }

#endregion

    #region undo & redo
    void RefreshActionlog()
    {
        if (tempActionlog.moved)    
        {
            editorActionLog.log.Add(tempActionlog);
            tempActionlog = new EditorActionLog.EditorAction();

            editorActionLog.undidlog = new List<EditorActionLog.EditorAction>();

            RefreshDoButtons();
        }
    }

    public void ClearActionlog() => editorActionLog = new EditorActionLog();

    void RefreshDoButtons()
    {
        undoButton.On = editorActionLog.log.Count > 0;
        redoButton.On = editorActionLog.undidlog.Count > 0;
    }

    public void Undo()
    {
        if (editorActionLog.log.Count <= 0) return;

        EditorActionLog.EditorAction actions = editorActionLog.log.Last();
        editorActionLog.log.Remove(actions);
        editorActionLog.undidlog.Add(actions);

        //undo tilemaps
        for (int i = 0; i < actions.to.Count; i++)
        {
            manager.tilemaps[actions.tilemap[i]].SetTile(actions.at[i], actions.from[i]);
        }

        //undo gameobject placement
        for (int i = 0; i < actions.gameObjetct_ids.Count; i++)
        {
            foreach (var item in FindObjectsOfType<EditorGameObject>())
            {
                if (item.instanceID == actions.gameObject_instances[i])
                {
                    if (item.saveChildTransform)
                    {
                        foreach (var ad in item.childs)
                        {
                            Destroy(ad.gameObject);
                        }
                    }

                    Destroy(item.gameObject);
                }
            }
        }

        //undo gameobject deletion
        for (int i = 0; i < actions.delGameObjetct_ids.Count; i++)
        {
            GameObject go = Instantiate(manager.customtiles[actions.delGameObjetct_ids[i]].editorGameObject, Funcs.AlignToGridPosition(actions.delGameObject_poses[i], 12), Quaternion.Euler(new Vector3(0, 0, actions.delGameObject_rotations[i])));
            go.transform.localScale = new Vector3(actions.delGameObject_scales[i].x, actions.delGameObject_scales[i].y, 1);
            EditorGameObject ego = go.GetComponent<EditorGameObject>();
            ego.tileID = actions.delGameObjetct_ids[i];
            ego.instanceID = actions.delGameObject_instances[i];
        }

        for (int i = 0; i < actions.moveGameObject_from.Count; i++)
        {
            foreach (var item in FindObjectsOfType<EditorGameObject>())
            {
                if (item.instanceID == actions.moveGameObject_instance[i]) item.transform.position = actions.moveGameObject_from[i];
            }
        }

        for (int i = 0; i < actions.rotateGameObject_from.Count; i++)
        {
            foreach (var item in FindObjectsOfType<EditorGameObject>())
            {
                if (item.instanceID == actions.rotateGameObject_instance[i]) item.transform.eulerAngles = new Vector3(0, 0, actions.rotateGameObject_from[i]);
            }
        }

        for (int i = 0; i < actions.scaleGameObject_from.Count; i++)
        {
            foreach (var item in FindObjectsOfType<EditorGameObject>())
            {
                if (item.instanceID == actions.scaleGameObject_instance[i]) item.transform.localScale = new Vector3(actions.scaleGameObject_from[i].x, actions.scaleGameObject_from[i].y, 1);
            }
        }

        RefreshDoButtons();
    }

    public void Redo()
    {
        if (editorActionLog.undidlog.Count <= 0) return;

        EditorActionLog.EditorAction actions = editorActionLog.undidlog.Last();
        editorActionLog.undidlog.Remove(actions);
        editorActionLog.log.Add(actions);

        //redo tilemaps
        for (int i = 0; i < actions.to.Count; i++)
        {
            manager.tilemaps[actions.tilemap[i]].SetTile(actions.at[i], actions.to[i]);
        }

        //redo placing objects
        for (int i = 0; i < actions.gameObjetct_ids.Count; i++)
        {
            GameObject go = Instantiate(manager.customtiles[actions.gameObjetct_ids[i]].editorGameObject, Funcs.AlignToGridPosition(actions.gameObject_poses[i], 12), Quaternion.Euler(Vector3.one));
            EditorGameObject ego = go.GetComponent<EditorGameObject>();
            ego.tileID = actions.gameObjetct_ids[i];
            ego.instanceID = actions.gameObject_instances[i];
        }

        //Redo deleting objects
        for (int i = 0; i < actions.delGameObjetct_ids.Count; i++)
        {
            foreach (var item in FindObjectsOfType<EditorGameObject>())
            {
                if (item.instanceID == actions.delGameObject_instances[i]) Destroy(item.gameObject);
            }
        }

        for (int i = 0; i < actions.moveGameObject_from.Count; i++)
        {
            foreach (var item in FindObjectsOfType<EditorGameObject>())
            {
                if (item.instanceID == actions.moveGameObject_instance[i]) item.transform.position = actions.moveGameObject_to[i];
            }
        }

        for (int i = 0; i < actions.rotateGameObject_from.Count; i++)
        {
            foreach (var item in FindObjectsOfType<EditorGameObject>())
            {
                if (item.instanceID == actions.rotateGameObject_instance[i]) item.transform.eulerAngles = new Vector3(0, 0, actions.rotateGameObject_to[i]);
            }
        }

        for (int i = 0; i < actions.scaleGameObject_from.Count; i++)
        {
            foreach (var item in FindObjectsOfType<EditorGameObject>())
            {
                if (item.instanceID == actions.scaleGameObject_instance[i]) item.transform.localScale = new Vector3(actions.scaleGameObject_to[i].x, actions.scaleGameObject_to[i].y, 1);
            }
        }

        RefreshDoButtons();
    }
    #endregion

    #region level testing
    public void TestLevel()
    {
        Memory.GameValues.levelEditorTesting = true;
        Memory.GameValues.CurrentLevelMode = Memory.GameValues.LevelMode.Custom;

        if (editorMode == Memory.GameValues.GameMode.Level)
        {
            Memory.GameValues.CurrentGameMode = Memory.GameValues.GameMode.Level;
        }
        else if (editorMode == Memory.GameValues.GameMode.Endless)
        {
            Memory.GameValues.CurrentGameMode = Memory.GameValues.GameMode.Endless;
            Memory.GameValues.CurrentEndlessMode = Memory.GameValues.EndlessMode.RocketLabs;
        }

        LevelEditorSaveUI.instance.AutoSave(true);

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //VinTools.SceneManagement.SceneTransition.FadeAndLoadScene(fadeColor, 1, EaseType.cubicInOut, levelBuildIndex);
    }
    #endregion

    [System.Serializable]
    public class LevelClipboard
    {
        public TilemapLayer[] tilemaps;

        [System.Serializable]
        public class TilemapLayer
        {
            public BoundsInt bounds;
            public TileBase[] tiles;
        }

        public LevelClipboard(int numberOfTilemaps)
        {
            this.tilemaps = new TilemapLayer[numberOfTilemaps];

            for (int i = 0; i < this.tilemaps.Length; i++)
            {
                this.tilemaps[i] = new TilemapLayer();
            }
        }
    }

    [System.Serializable]
    public class EditorActionLog
    {
        public List<EditorAction> log = new List<EditorAction>();
        public List<EditorAction> undidlog = new List<EditorAction>();

        public EditorActionLog()
        {
            log = new List<EditorAction>();
            undidlog = new List<EditorAction>();
        }

        [System.Serializable]
        public class EditorAction
        {
            public bool moved;
            
            //ActionType.tilemapChange
            public List<int> tilemap = new List<int>();
            public List<TileBase> from = new List<TileBase>();
            public List<TileBase> to = new List<TileBase>();
            public List<Vector3Int> at = new List<Vector3Int>();

            //ActionType.placegameObject
            public List<int> gameObjetct_ids = new List<int>();
            public List<Vector3> gameObject_poses = new List<Vector3>();
            public List<float> gameObject_instances = new List<float>();

            //ActionType.deleteGameObject
            public List<int> delGameObjetct_ids = new List<int>();
            public List<Vector3> delGameObject_poses = new List<Vector3>();
            public List<float> delGameObject_instances = new List<float>();
            public List<float> delGameObject_rotations = new List<float>();
            public List<Vector2> delGameObject_scales = new List<Vector2>();

            //move
            public List<Vector2> moveGameObject_from = new List<Vector2>();
            public List<float> moveGameObject_instance = new List<float>();
            public List<Vector2> moveGameObject_to = new List<Vector2>();

            //rotate
            public List<float> rotateGameObject_from = new List<float>();
            public List<float> rotateGameObject_instance = new List<float>();
            public List<float> rotateGameObject_to = new List<float>();

            //rotate
            public List<Vector2> scaleGameObject_from = new List<Vector2>();
            public List<float> scaleGameObject_instance = new List<float>();
            public List<Vector2> scaleGameObject_to = new List<Vector2>();

            public void AddAction_TilemapChange(int tilemap, TileBase from, TileBase to, Vector3Int at)
            {
                if (from == to) return;
                
                this.tilemap.Add(tilemap);
                this.from.Add(from);
                this.to.Add(to);
                this.at.Add(at);

                this.moved = true;
            }

            public void AddAction_PlaceGameObject(int objID, Vector3 pos, float instanceID)
            {
                this.gameObjetct_ids.Add(objID);
                this.gameObject_poses.Add(pos);
                this.gameObject_instances.Add(instanceID);

                this.moved = true;
            }

            public void AddAction_DestroyGameObject(int objID, Vector3 pos, float instanceID, float rotation, Vector2 scale)
            {
                this.delGameObjetct_ids.Add(objID);
                this.delGameObject_poses.Add(pos);
                this.delGameObject_instances.Add(instanceID);

                this.delGameObject_scales.Add(scale);
                this.delGameObject_rotations.Add(rotation);

                this.moved = true;
            }

            public void AddAction_MoveGameObject(Vector2 from, Vector2 to, float instance)
            {
                this.moveGameObject_from.Add(from);
                this.moveGameObject_to.Add(to);
                this.moveGameObject_instance.Add(instance);

                this.moved = true;
            }

            public void AddAction_RotateGameObject(float from, float to, float instance)
            {
                this.rotateGameObject_from.Add(from);
                this.rotateGameObject_to.Add(to);
                this.rotateGameObject_instance.Add(instance);

                this.moved = true;
            }

            public void AddAction_ScaleGameObject(Vector2 from, Vector2 to, float instance)
            {
                this.scaleGameObject_from.Add(from);
                this.scaleGameObject_to.Add(to);
                this.scaleGameObject_instance.Add(instance);

                this.moved = true;
            }
        }
    }
}

