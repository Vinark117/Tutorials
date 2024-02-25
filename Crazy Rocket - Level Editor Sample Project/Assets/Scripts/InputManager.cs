using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VinTools;

public class InputManager : MonoBehaviour
{
    #region static variables & setup
    public static InputManager instance;
    //private InputMap input;
    public InputScene scene;

    public enum InputScene { RocketControls, LevelEditor }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        //DontDestroyOnLoad(gameObject);

        //input = new InputMap();
    }

    /*private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }*/
    #endregion

    #region Level editor
    Vector2 mouseStartPos;

    const float clickDelay = .1f;
    static float lastRightClickTime;
    static float lastLeftClickTime;

    public static Vector2 mousePos
    {
        get
        {
            return Input.mousePosition;
        }
    }
    public static Vector2 mouseScroll
    {
        get
        {
            return Input.mouseScrollDelta;
        }
    }
    public static bool mouseDown 
    {
        get
        {
            return Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1);
        }
    }
    public static bool mouseHold
    {
        get
        {
            lastLeftClickTime = Time.time;
            return Input.GetMouseButton(0) && !Input.GetMouseButton(1);
        }
    }
    public static bool mouseUp
    {
        get
        {
            return Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1);
        }
    }
    public static bool mouseDownRight
    {
        get
        {
            return Input.GetMouseButtonDown(1);
        }
    }
    public static bool mouseHoldRight
    {
        get
        {
            lastRightClickTime = Time.time;
            return Input.GetMouseButton(1);
        }
    }
    public static bool mouseUpRight
    {
        get
        {
            return Input.GetMouseButtonUp(1);
        }
    }
    public static bool wasMouseMoved;
    
    public static bool mouseOverUI;

    public static UnityEvent Editor_SelectDraw = new UnityEvent();
    public static UnityEvent Editor_SelectArea = new UnityEvent();
    public static UnityEvent Editor_SelectFill = new UnityEvent();
    public static UnityEvent Editor_SelectCopy = new UnityEvent();
    public static UnityEvent Editor_SelectPaste = new UnityEvent();
    public static UnityEvent Editor_SelectGameobjectSelect = new UnityEvent();
    public static UnityEvent Editor_SelectGameobjectMove = new UnityEvent();
    public static UnityEvent Editor_SelectGameobjectRotate = new UnityEvent();
    public static UnityEvent Editor_SelectGameobjectScale = new UnityEvent();

    public static UnityEvent Editor_Copy = new UnityEvent();
    public static UnityEvent Editor_Paste = new UnityEvent();
    public static UnityEvent Editor_Undo = new UnityEvent();
    public static UnityEvent Editor_Redo = new UnityEvent();
    public static UnityEvent Editor_Save = new UnityEvent();
    public static UnityEvent Editor_Open = new UnityEvent();
    public static UnityEvent Editor_ZoomIn = new UnityEvent();
    public static UnityEvent Editor_ZoomOut = new UnityEvent();
    public static UnityEvent Editor_PlayTest = new UnityEvent();
    #endregion

    public bool Escape
    {
        get
        {
            //return input.Generic.Escape.triggered;
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }

    private void Update()
    {
        switch (scene)
        {
            case InputScene.LevelEditor:
                LevelEditor();
                KeyboardShortcuts();
                break;
            default:
                break;
        }
    }

    void LevelEditor()
    {
        //left mouse
        /*mouseUp = input.LevelEditor.MouseLeftButton.phase == UnityEngine.InputSystem.InputActionPhase.Waiting && mouseHold;
        mouseHold = input.LevelEditor.MouseLeftButton.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        mouseDown = input.LevelEditor.MouseLeftButton.triggered;

        //right mouse
        mouseUpRight = input.LevelEditor.MouseRightButton.phase == UnityEngine.InputSystem.InputActionPhase.Waiting && mouseHoldRight;
        mouseHoldRight = input.LevelEditor.MouseRightButton.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        mouseDownRight = input.LevelEditor.MouseRightButton.triggered;

        //mouse pos
        mousePos = input.LevelEditor.MousePos.ReadValue<Vector2>();
        //mouse scroll
        mouseScroll = input.LevelEditor.MouseScroll.ReadValue<Vector2>();*/

        //check if mouse was moved
        if (mouseDown) mouseStartPos = mousePos;
        wasMouseMoved = mousePos != mouseStartPos;

        //find if mouse over UI gameobjects
        mouseOverUI = IsPointerOverGameObject();
    }

    static bool IsPointerOverGameObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    /*static bool IsPointerOverGameObject()
    {
        //check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }

        return false;
    }*/

    void KeyboardShortcuts()
    {
        //return if over UI
        if (mouseOverUI) return;

        //ctrl key is not held down
        //if (input.Generic.ModifierKey.phase != UnityEngine.InputSystem.InputActionPhase.Started)
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            //if (input.EditorShortcuts.Draw.triggered) 
            if (Input.GetKeyDown(KeyCode.P))
                Editor_SelectDraw.Invoke();

            //if (input.EditorShortcuts.Box.triggered) 
            if (Input.GetKeyDown(KeyCode.B))
                Editor_SelectArea.Invoke();

            //if (input.EditorShortcuts.Fill.triggered) 
            if (Input.GetKeyDown(KeyCode.F))
                Editor_SelectFill.Invoke();

            //if (input.EditorShortcuts.Copy.triggered) 
            if (Input.GetKeyDown(KeyCode.C))
                Editor_SelectCopy.Invoke();

            //if (input.EditorShortcuts.Paste.triggered) 
            if (Input.GetKeyDown(KeyCode.V))
                Editor_SelectPaste.Invoke();

            //if (input.EditorShortcuts.SelectObject.triggered) 
            if (Input.GetKeyDown(KeyCode.E))
                Editor_SelectGameobjectSelect.Invoke();

            //if (input.EditorShortcuts.Move.triggered) 
            if (Input.GetKeyDown(KeyCode.G))
                Editor_SelectGameobjectMove.Invoke();

            //if (input.EditorShortcuts.Rotate.triggered) 
            if (Input.GetKeyDown(KeyCode.R))
                Editor_SelectGameobjectRotate.Invoke();

            //if (input.EditorShortcuts.Scale.triggered) 
            if (Input.GetKeyDown(KeyCode.S))
                Editor_SelectGameobjectScale.Invoke();

            //if (input.EditorShortcuts.ZoomIn.triggered)
            if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.PageUp))
                Editor_ZoomIn.Invoke();

            //if (input.EditorShortcuts.ZoomOut.triggered)
            if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.PageDown))
                Editor_ZoomOut.Invoke();
        }

        //ctrl key is held down
        //if (input.Generic.ModifierKey.phase == UnityEngine.InputSystem.InputActionPhase.Started)
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //if (input.EditorShortcuts.Undo.triggered) 
            if (Input.GetKeyDown(KeyCode.Z))
                Editor_Undo.Invoke();

            //if (input.EditorShortcuts.Redo.triggered) 
            if (Input.GetKeyDown(KeyCode.Y))
                Editor_Redo.Invoke();

            //if (input.EditorShortcuts.SaveLevel.triggered) 
            if (Input.GetKeyDown(KeyCode.S))
                Editor_Save.Invoke();

            //if (input.EditorShortcuts.OpenLevel.triggered)
            if (Input.GetKeyDown(KeyCode.O))
                Editor_Open.Invoke();

            //if (input.EditorShortcuts.Copy.triggered) 
            if (Input.GetKeyDown(KeyCode.C))
                Editor_Copy.Invoke();

            //if (input.EditorShortcuts.Paste.triggered) 
            if (Input.GetKeyDown(KeyCode.V))
                Editor_Paste.Invoke();

            //if (input.EditorShortcuts.Playtest.triggered) 
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Return))
                Editor_PlayTest.Invoke();
        }
    }
}
