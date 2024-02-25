using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EditorGameObject : MonoBehaviour
{
    [HideInInspector] public int tileID;
    [HideInInspector] public float instanceID;

    [Header("Settings")]
    public bool saveChildTransform = false;
    public bool isChild = false;

    [HideInInspector] public List<EditorGameObject> childs;
    [HideInInspector] public EditorGameObject parent;

    private void Awake()
    {
        Instantiate();
    }

    public void Instantiate()
    {
        if (isChild) parent = GetComponentInParent<EditorGameObject>();
        if (saveChildTransform)
        {
            childs = GetComponentsInChildren<EditorGameObject>().ToList();
            childs.Remove(this);
        }
    }
}
