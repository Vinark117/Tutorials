using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VinTools;

public class EditorCamera : MonoBehaviour
{
    public static EditorCamera instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static bool isEnabled = true;

    [Header("Settings")]
    public float maxZoom;
    public float minZoom;
    [Space]
    public float touchZoomMultiplier;
    public float mouseZoomMultiplier;
    public float keyboardMoveSpeed;

    Camera cam;

    public static Vector3 newPosition;
    public static float newOrtographicSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
        newPosition = transform.position;
        newOrtographicSize = cam.orthographicSize;
    }

    Vector3 ZTS_offset;
    float ZTS_camSize;
    bool ZTS_started;

    private void Update()
    {
        if (!isEnabled) return;

        MouseMove();
        //if (!InputManager.mouseOverUI) CameraFuncs.AccurateMouseZoom(cam, Input.mousePosition, Input.mouseScrollDelta, mouseZoomMultiplier, minZoom, maxZoom);
        MouseZoom();
        TouchZoom();

        //limit zoom
        newOrtographicSize = Mathf.Clamp(newOrtographicSize, minZoom, maxZoom);
        cam.orthographicSize = newOrtographicSize;

        //set position
        transform.position = newPosition;

        //set grid
        SetGridPosition();

    }

    bool canMove = true;
    Vector3 dragStartPos;

    void MouseMove()
    {
        if (!canMove) return;
        if (InputManager.mouseOverUI) return;

        Vector3 mpos = cam.ScreenToWorldPoint(InputManager.mousePos);

        if (((LevelEditor.instance.drawMode == LevelEditor.DrawMode.Draw) && !LevelEditor.instance.swipe) || LevelEditor.instance.drawMode == LevelEditor.DrawMode.Fill || LevelEditor.instance.drawMode == LevelEditor.DrawMode.Paste)
        {
            if (!InputManager.mouseHoldRight)
            {
                if (InputManager.mouseDown) dragStartPos = mpos;
                if (InputManager.mouseHold) newPosition += dragStartPos - mpos;
                //if (InputManager.mouseHold) transform.position += dragStartPos - mpos;
            }
        }

        if (InputManager.mouseDownRight) dragStartPos = mpos;
        if (InputManager.mouseHoldRight) newPosition += dragStartPos - mpos;
        //if (InputManager.mouseHoldRight) transform.position += dragStartPos - mpos;
    }
    void MouseZoom() 
    {
        if (InputManager.mouseOverUI) return;

        //zoom
        if (InputManager.mouseScroll.y != 0)
        {
            Vector2 mousepos = Vector2.zero;

            mousepos = cam.ScreenToWorldPoint(InputManager.mousePos) - transform.position;
            mousepos.x /= ortographicWidth;
            mousepos.y /= newOrtographicSize;

            newOrtographicSize += InputManager.mouseScroll.y * -mouseZoomMultiplier * Time.deltaTime;
            newOrtographicSize = Mathf.Clamp(newOrtographicSize, minZoom, maxZoom);

            newPosition = cam.ScreenToWorldPoint(InputManager.mousePos) - new Vector3(mousepos.x * ortographicWidth, mousepos.y * newOrtographicSize, 0);
        }
    }
    void TouchZoom()
    {
        if (Input.touchCount == 2)
        {
            if (!ZTS_started)
            {
                ZTS_offset = Input.GetTouch(0).position - Input.GetTouch(1).position;
                ZTS_camSize = cam.orthographicSize;

                ZTS_started = true;
            }

            newOrtographicSize = (ZTS_offset.magnitude / (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude) * ZTS_camSize;
        }
        else
        {
            ZTS_started = false;
        }
    }

    public void IncreaseZoom(int by)
    {

        //newOrtographicSize = (step - by) * 4;
        //newOrtographicSize = Mathf.Clamp(newOrtographicSize, 2, maxZoom);

        int step = (int)cam.orthographicSize / 4;
        float OrtographicSize = (step - by) * 4;
        newOrtographicSize = Mathf.Clamp(OrtographicSize, 2, maxZoom);
    }

    float ortographicWidth
    {
        get
        {
            return newOrtographicSize / Screen.height * Screen.width;
        }
        set
        {
            newOrtographicSize = value / Screen.width * Screen.height;
        }
    }

    [Header("Grid")]
    public Transform grid;
    public float gridSize = 0.5f;
    public Vector2 offset;

    private void SetGridPosition()
    {
        grid.gameObject.SetActive(cam.orthographicSize <= 40);

        //Vector3 p = (Vector2)transform.position;
        //grid.position = new Vector3(ToGridPos(p.x) + GetOffset(offset.x), ToGridPos(p.y) + GetOffset(offset.y), ToGridPos(p.z) + GetOffset(offset.z));
        grid.position = Funcs.AlignToGridPosition((Vector2)transform.position, gridSize, offset);
    }

    /*float ToGridPos(float pos)
    {
        return Mathf.Round(pos * gridSize) / gridSize;
    }
    float GetOffset(float offset)
    {
        return (1f / gridSize * offset);
    }*/
}
