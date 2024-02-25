using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaHandler : MonoBehaviour
{
    public enum Behaviour { OnlyExludeCutouts, Symmetric }

    [Header("Settings")]
    [Tooltip("The behaviour of the rect")]
    public Behaviour behaviour;
    //[Tooltip("Extra padding to the rect besides the safe area, given in percentages")]
    //public Vector2 padding;

    RectTransform rectTransform;
    Rect safeArea;
    Vector2 minAnchor;
    Vector2 maxAnchor;

    private void Awake()
    {
        //set values
        rectTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;

        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        //set it to symmetric
        if (behaviour == Behaviour.Symmetric)
        {
            float padding_left = minAnchor.x;
            float padding_right = 1 - maxAnchor.x;
            float padding_bottom = minAnchor.y;
            float padding_top = 1 - maxAnchor.y;

            if (padding_left < padding_right) minAnchor.x = padding_right;
            else if (padding_left > padding_right) maxAnchor.x = 1 - padding_left;

            if (padding_top > padding_bottom) minAnchor.y = padding_top;
            else if (padding_top < padding_bottom) maxAnchor.y = 1 - padding_bottom;
        }

        //apply changes
        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }
}
