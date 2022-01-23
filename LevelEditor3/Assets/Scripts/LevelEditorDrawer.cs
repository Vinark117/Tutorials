using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VinTools.Tweening;

public class LevelEditorDrawer : MonoBehaviour
{
    RectTransform drawer;
    [SerializeField] Text drawerTitle;
    [SerializeField] Image[] drawerSlots;
    [SerializeField] Button leftArrow;
    [SerializeField] Button rightArrow;
    [Space]
    [SerializeField] int tileDrawerOpenPos;
    [SerializeField] int tileDrawerClosedPos;

    bool isDrawerOpened;
    int selectedCategory;
    int selectedDrawerPage;

    CustomTile[] currentDisplayedTiles;
    List<CustomTile>[] organizedTiles;

    private void Start()
    {
        drawer = GetComponent<RectTransform>();

        SetUpDrawers();
    }

    void SetUpDrawers()
    {
        string[] categoryNames = System.Enum.GetNames(typeof(LevelManager.Categories));
        var categories = System.Enum.GetValues(typeof(LevelManager.Categories));

        organizedTiles = new List<CustomTile>[categories.Length];
        for (int i = 0; i < organizedTiles.Length; i++)
        {
            organizedTiles[i] = new List<CustomTile>();
        }

        currentDisplayedTiles = new CustomTile[drawerSlots.Length];

        foreach (var item in LevelManager.instance.tiles)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                if (item.drawerCategory == (LevelManager.Categories)categories.GetValue(i)) organizedTiles[i].Add(item);
            }
        }
    }

    public void Open(int index)
    {
        if (isDrawerOpened) return;

        drawer.localPosition = new Vector3(drawer.localPosition.x, tileDrawerOpenPos, drawer.position.z);
        //drawer.TweenMoveLocalY(tileDrawerOpenPos, .2f).SetEaseInOutCubic();
        isDrawerOpened = true;

        drawerTitle.text = System.Enum.GetNames(typeof(LevelManager.Categories))[index];

        selectedCategory = index;
        selectedDrawerPage = 0;
        SetPage(0);
    }

    public void SetPage(int difference)
    {
        selectedDrawerPage += difference;

        List<CustomTile> listOfTiles = organizedTiles[selectedCategory];

        bool firstPage = selectedDrawerPage <= 0;
        bool lastPage = listOfTiles.Count <= (selectedDrawerPage + 1) * drawerSlots.Length;

        leftArrow.gameObject.SetActive(!firstPage);
        leftArrow.interactable = !firstPage;

        rightArrow.gameObject.SetActive(!lastPage);
        rightArrow.interactable = !lastPage;

        for (int i = 0; i < drawerSlots.Length; i++)
        {
            int index = i + selectedDrawerPage * drawerSlots.Length;

            if (index < listOfTiles.Count)
            {
                drawerSlots[i].sprite = listOfTiles[index].image;
                currentDisplayedTiles[i] = listOfTiles[index];
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
        if (currentDisplayedTiles[index] != null) LevelEditor.instance._selectedTileIndex = LevelManager.instance.tiles.IndexOf(currentDisplayedTiles[index]);

        Close();
    }

    public void Close()
    {
        if (!isDrawerOpened) return;

        drawer.localPosition = new Vector3(drawer.localPosition.x, tileDrawerClosedPos, drawer.position.z);
        //drawer.TweenMoveLocalY(tileDrawerClosedPos, .2f).SetEaseInOutCubic();
        isDrawerOpened = false;
    }
}
