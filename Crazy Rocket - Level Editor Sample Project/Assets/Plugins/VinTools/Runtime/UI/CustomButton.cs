using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    public enum ButtonState { Default, Highlighted, Pressed, Selected}
    public ButtonState buttonState;

    [Header("Child Animation")]
    public bool changeChildPosition = true;
    [Space]
    public Vector2 ChildPos_Default = new Vector2(0, 0);
    public Vector2 ChildPos_Highlighted = new Vector2(0, -1);
    public Vector2 ChildPos_Pressed = new Vector2(0, -2);

    //Vector3[] defaultChildPoses;
    Vector3[] defaultAnchoredPoses;
    RectTransform[] childs;

    protected override void Awake()
    {
        base.Awake();

        //get all of the childs and positions
        //defaultChildPoses = new Vector3[transform.childCount];
        defaultAnchoredPoses = new Vector3[transform.childCount];
        childs = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i).GetComponent<RectTransform>();
            //defaultChildPoses[i] = childs[i].localPosition;
            defaultAnchoredPoses[i] = childs[i].anchoredPosition;
        }
    }

    public void OnButtonStateChange()
    {
        if (transition == Transition.SpriteSwap && changeChildPosition)
        {
            switch (buttonState)
            {
                case ButtonState.Default:
                    SetChildPoses(ChildPos_Default);
                    break;
                case ButtonState.Highlighted:
                    SetChildPoses(ChildPos_Highlighted);
                    break;
                case ButtonState.Pressed:
                    SetChildPoses(ChildPos_Pressed);
                    break;
                case ButtonState.Selected:
                    SetChildPoses(ChildPos_Highlighted);
                    break;
                default:
                    break;
            }
        }
    }

    void SetChildPoses(Vector2 offset)
    {
        for (int i = 0; i < childs.Length; i++)
        {
            //childs[i].localPosition = defaultChildPoses[i] + (Vector3)offset;
            childs[i].anchoredPosition = defaultAnchoredPoses[i] + (Vector3)offset;
        }
    }

    #region overrides
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);

        buttonState = ButtonState.Default;
        OnButtonStateChange();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        buttonState = ButtonState.Selected;
        OnButtonStateChange();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        buttonState = ButtonState.Pressed;
        OnButtonStateChange();

    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (buttonState != ButtonState.Selected && buttonState != ButtonState.Pressed)
        {
            buttonState = ButtonState.Highlighted;
            OnButtonStateChange();
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (buttonState != ButtonState.Selected && buttonState != ButtonState.Pressed)
        {
            buttonState = ButtonState.Default;
            OnButtonStateChange();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (buttonState != ButtonState.Selected)
        {
            buttonState = ButtonState.Highlighted;
            OnButtonStateChange();
        }
    }
    #endregion
}
