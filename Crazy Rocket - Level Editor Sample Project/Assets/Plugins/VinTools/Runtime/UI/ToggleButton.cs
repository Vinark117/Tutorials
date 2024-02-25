using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ToggleButton : MonoBehaviour
{
    [Header("Settings")]
    public Behaviour behaviour;
    [SerializeField] bool on = true;

    public enum Behaviour { none, togglable, groupselect }
    public bool On
    {
        get
        {
            return on;
        }
        set
        {
            if (on != value)
            {
                on = value;
                ChangeSprites();
            }
        }
    }
    public bool interactable
    {
        get
        {
            return button.interactable;
        }
        set
        {
            button.interactable = value;
        }
    }

    [Header("On")]
    public Sprite On_Default;
    public Sprite On_Highlighted;
    public Sprite On_Pressed;
    [Header("Off")]
    public Sprite Off_Default;
    public Sprite Off_Highlighted;
    public Sprite Off_Pressed;

    [Space]
    public Button.ButtonClickedEvent OnSelect;
    public Button.ButtonClickedEvent OnDeselect;


    Image img;
    [HideInInspector] public Button button;
    ToggleButton[] group;

    private void Awake()
    {
        //get components
        img = GetComponent<Image>();
        button = GetComponent<Button>();
        group = transform.parent.GetComponentsInChildren<ToggleButton>();

        //setup button
        button.transition = Selectable.Transition.SpriteSwap;
        ChangeSprites();

        switch (behaviour)
        {
            case Behaviour.none:
                button.onClick.AddListener(PressButton);
                break;
            case Behaviour.togglable:
                button.onClick.AddListener(PressTogglable);
                break;
            case Behaviour.groupselect:
                button.onClick.AddListener(SelectFromGroup);
                break;
            default:
                break;
        }
    }

    void ChangeSprites()
    {
        SpriteState state = button.spriteState;
        switch (on)
        {
            case true:
                img.sprite = On_Default;

                state.highlightedSprite = On_Highlighted;
                state.pressedSprite = On_Pressed;
                state.selectedSprite = On_Highlighted;
                state.disabledSprite = Off_Default != null ? Off_Default : On_Default;
                button.spriteState = state;
                break;
            case false:
                img.sprite = Off_Default;

                state.highlightedSprite = Off_Highlighted;
                state.pressedSprite = Off_Pressed;
                state.selectedSprite = Off_Highlighted;
                state.disabledSprite = Off_Default != null ? Off_Default : On_Default;
                button.spriteState = state;
                break;
        }
    }
    void PressButton()
    {
        OnSelect.Invoke();
    }
    void PressTogglable()
    {
        on = !on;
        ChangeSprites();

        if (on) OnSelect.Invoke();
        else OnDeselect.Invoke();
    }
    void SelectFromGroup()
    {
        foreach (var item in group)
        {
            if (item != this)
            {
                item.on = false;
                item.ChangeSprites();

                if (item.on) item.OnSelect.Invoke();
                else item.OnDeselect.Invoke();
            }
        }

        on = true;
        ChangeSprites();

        if (on) OnSelect.Invoke();
        else OnDeselect.Invoke();
    }
}
