using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelEditorSaveUI : MonoBehaviour
{
    public static LevelEditorSaveUI instance;

    private void Awake()
    {
        //set instance
        if (instance == null) instance = this;
        else Destroy(this);
    }

    string currentLevelTitle;
    string currentLevelAuthor;
    string currentLevelDescription;
    [HideInInspector] public byte[] screenshot;
    [HideInInspector] public Vector2Int screenshotResolution;
    LevelData levelDataDisplayed;
    EndlessSection sectionDataDisplayed;
    string levelPathDisplayed;

    enum mode { Save, Load}
    mode currentMode;

    public float transitionTime = 0.3f;

    List<FileInfo> levelFiles = new List<FileInfo>();
    List<GameObject> displayedLevels = new List<GameObject>();

    [Header("Objs")]
    public Transform openPos;
    public Transform closedPos;
    public Transform UI_Transform;
    public CanvasGroup backgroundPanel;

    [Header("UI Elements")]
    public InputField inputField_Title;
    public InputField inputField_Author;
    public InputField inputField_Description;
    public RawImage image_Preview;
    public Button button_Save;
    public Button button_Load;
    public Button button_Delete;
    public Text text_ImagePreview;

    [Header("Level Displays")]
    public RectTransform viewportContent;
    public GameObject levelPreviewTemplate;

    private void Start()
    {
        //set UI to starting position
        backgroundPanel.blocksRaycasts = true;
        backgroundPanel.alpha = 0;
        backgroundPanel.gameObject.SetActive(false);
        UI_Transform.localPosition = closedPos.localPosition;
        UI_Transform.gameObject.SetActive(false);

        //set shortcuts
        InputManager.Editor_Save.AddListener(OpenSaveUI);
        InputManager.Editor_Open.AddListener(OpenLoadUI);

        //load back level after testing
        AutoLoadTest();
    }

    #region Change inputfields
    public void Filterlevels(string text)
    {
        foreach (var lvl in displayedLevels)
        {
            lvl.SetActive(lvl.name.ToLower().Contains(text.ToLower()));
        }
    }
    public void Edit_Title(string text)
    {
        button_Save.GetComponentInChildren<Text>().text = currentLevelTitle != text ? "Save as" : "Save";
    }
    public void Edit_Author(string text)
    {

    }
    public void Edit_Description(string text)
    {

    }
    #endregion

    #region Open & Close UI
    void OpenUI()
    {
        //animation
        backgroundPanel.gameObject.SetActive(true);
        UI_Transform.gameObject.SetActive(true);
        backgroundPanel.blocksRaycasts = true;
        backgroundPanel.LeanAlpha(1, transitionTime);
        UI_Transform.LeanMoveLocal(openPos.localPosition, transitionTime).setEaseInOutCubic();

        button_Delete.gameObject.SetActive(false);

        //display saved levels
        GetSavedLevels(levelFiles, LevelEditor.editorMode);
        DisplaySavedlevels();
    }

    public void OpenLoadUI()
    {
        OpenUI();

        //set current mode
        currentMode = mode.Load;

        //set buttons
        button_Save.gameObject.SetActive(false);
        button_Load.gameObject.SetActive(true);

        //set interactable
        inputField_Author.interactable = false;
        inputField_Description.interactable = false;
        inputField_Title.interactable = false;

        //set texts
        inputField_Title.text = "";
        inputField_Author.text = "";
        inputField_Description.text = "";
        text_ImagePreview.text = "No preview.";

    }
    public void OpenSaveUI()
    {
        OpenUI();

        //set current mode
        currentMode = mode.Save;

        //set buttons
        button_Save.gameObject.SetActive(true);
        button_Load.gameObject.SetActive(false);

        //set interactable
        inputField_Author.interactable = true;
        inputField_Description.interactable = true;
        inputField_Title.interactable = true;

        //set save button text
        button_Save.GetComponentInChildren<Text>().text = "Save";
        text_ImagePreview.text = "No image found.\nTake a screenshot to\nmake a preview image.";

        //set texts
        inputField_Title.text = currentLevelTitle;
        inputField_Author.text = currentLevelAuthor;
        inputField_Description.text = currentLevelDescription;

        //set preview Image
        SetPreviewImage(image_Preview, screenshot, screenshotResolution.x, screenshotResolution.y);
    }
    public void CloseUI()
    {
        //animation
        UI_Transform.LeanMoveLocal(closedPos.localPosition, transitionTime).setEaseInOutCubic();
        backgroundPanel.LeanAlpha(0, transitionTime).setOnComplete(() =>
        {
            backgroundPanel.blocksRaycasts = true;
            backgroundPanel.gameObject.SetActive(false);
            UI_Transform.gameObject.SetActive(false);
        });

        //reset stuff
        levelDataDisplayed = null;
        sectionDataDisplayed = null;
        levelPathDisplayed = "";
        button_Delete.gameObject.SetActive(false);
    }
    #endregion

    #region Level list
    public static void GetSavedLevels(List<FileInfo> levelFiles, Memory.GameValues.GameMode mode)
    {
        levelFiles.Clear();

        string folder = "";
        if (mode == Memory.GameValues.GameMode.Level) folder = "CustomLevels";
        if (mode == Memory.GameValues.GameMode.Endless) folder = "CustomSections";

        string directoryPath = $"{Application.persistentDataPath}{(folder == null ? "" : $"/{folder}")}";

        //create folder if not existing
        if (!Directory.Exists(directoryPath))
        {
            Debug.LogWarning($"Directory: {directoryPath} does not exist, creating directory...");

            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (System.Exception)
            {
                Debug.LogError($"Failed to create directory: {directoryPath}");
                throw;
            }
        }

        var info = new DirectoryInfo(directoryPath);
        var fileInfo = info.GetFiles();

        foreach (var file in fileInfo)
        {
            if (file.Extension == ".json") levelFiles.Add(file);
        }
    }

    void DisplaySavedlevels()
    {
        levelPreviewTemplate.SetActive(false);

        //delete the previous gameobjects
        while (displayedLevels.Count > 0)
        {
            GameObject go = displayedLevels[0];
            displayedLevels.RemoveAt(0);
            Destroy(go);
        }

        //set scrollview height
        float panelHeight = levelPreviewTemplate.GetComponent<RectTransform>().rect.height + 5;
        viewportContent.sizeDelta = new Vector2(viewportContent.sizeDelta.x, levelFiles.Count * panelHeight - 5);

        for (int i = 0; i < levelFiles.Count; i++)
        {
            GameObject go = Instantiate(levelPreviewTemplate, levelPreviewTemplate.transform.parent);
            go.SetActive(true);
            displayedLevels.Add(go);

            string path = levelFiles[i].ToString();
            string json = "";
            try
            {
                json = File.ReadAllText(path);
            }
            catch (System.Exception)
            {
                Debug.LogError($"Failed to read JSON data at path: {path}");
                throw;
            }

            LevelData data = new LevelData();

            if (LevelEditor.editorMode == Memory.GameValues.GameMode.Level) data = JsonUtility.FromJson<LevelData>(json);
            if (LevelEditor.editorMode == Memory.GameValues.GameMode.Endless) data = JsonUtility.FromJson<EndlessSection>(json).levelData;

            go.transform.Find("Title").GetComponent<Text>().text = data.saveName;
            go.transform.Find("Author").GetComponent<Text>().text = "By " + data.author;
            SetPreviewImage(go.GetComponentInChildren<RawImage>(), data.previewImage, data.previewImageResolution.x, data.previewImageResolution.y);

            go.transform.name = "*" + data.saveName + "*" + data.author + "*";

            go.GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() => { GetLevel(path); }));
        }
    }
    #endregion

    #region Save & Load Methods
    public void GetLevel(string path)
    {
        string json = "";
        try
        {
            json = File.ReadAllText(path);
        }
        catch (System.Exception)
        {
            Debug.LogError($"Failed to read JSON data at path: {path}");
            throw;
        }

        LevelData data = new LevelData();

        if (LevelEditor.editorMode == Memory.GameValues.GameMode.Level) data = JsonUtility.FromJson<LevelData>(json);
        if (LevelEditor.editorMode == Memory.GameValues.GameMode.Endless)
        {
            sectionDataDisplayed = JsonUtility.FromJson<EndlessSection>(json);
            data = sectionDataDisplayed.levelData;
        }

        inputField_Title.text = data.saveName;
        inputField_Author.text = data.author;
        inputField_Description.text = data.description;

        SetPreviewImage(image_Preview, data.previewImage, data.previewImageResolution.x, data.previewImageResolution.y);

        levelDataDisplayed = data;
        levelPathDisplayed = path;
        if (currentMode == mode.Load) button_Delete.gameObject.SetActive(true);
    }

    public void Save()
    {
        if (inputField_Title.text == "") return;
        if (inputField_Author.text == "") return;

        AutoSave(false);
    }

    public void AutoSave(bool forceSave)
    {
        if (!forceSave)
        {
            if (inputField_Title.text == "") return;
            if (inputField_Author.text == "") return;
        }

        currentLevelTitle = inputField_Title.text == "" ? "(AutoSave)" : inputField_Title.text;
        currentLevelAuthor = inputField_Author.text == "" ? "(AutoSave)" : inputField_Author.text;
        currentLevelDescription = inputField_Description.text;

        if (LevelEditor.editorMode == Memory.GameValues.GameMode.Level)
        {
            LevelData level = LevelManager.instance.SaveLevel(currentLevelTitle, currentLevelAuthor, currentLevelDescription, screenshot, screenshotResolution);
            Memory.GameValues.levelToTest = level;
            SaveManager.SaveLevel(level, "CustomLevels", currentLevelTitle);
        }
        if (LevelEditor.editorMode == Memory.GameValues.GameMode.Endless)
        {
            EndlessSection level = LevelManager.instance.SaveEndlessSection(currentLevelTitle, currentLevelAuthor, currentLevelDescription, screenshot, screenshotResolution);
            Memory.GameValues.sectionToTest = level;
            SaveManager.SaveEndlessSection(level, "CustomSections", currentLevelTitle);
        }

        CloseUI();
    }

    public void Load()
    {
        if (inputField_Title.text == "") return;
        if (levelDataDisplayed == null) return;

        currentLevelTitle = inputField_Title.text == "(AutoSave)" ? "" : inputField_Title.text;
        currentLevelAuthor = inputField_Author.text == "(AutoSave)" ? "" : inputField_Author.text;
        currentLevelDescription = inputField_Description.text == "(AutoSave)" ? "" : inputField_Description.text;

        LevelManager.instance.LoadLevel(levelDataDisplayed);
        if (LevelEditor.editorMode == Memory.GameValues.GameMode.Endless) LevelManager.instance.LoadEndlessSectionHardness(sectionDataDisplayed.hardness);
        LevelEditor.instance.ClearActionlog();

        CloseUI();
    }

    public void AutoLoadTest()
    {
        if (!Memory.GameValues.levelEditorTesting) return;

        if (LevelEditor.editorMode == Memory.GameValues.GameMode.Level)
        {
            LevelManager.instance.LoadLevel(Memory.GameValues.levelToTest);
        }
        else if (LevelEditor.editorMode == Memory.GameValues.GameMode.Endless)
        {
            LevelManager.instance.LoadLevel(Memory.GameValues.sectionToTest.levelData);
            LevelManager.instance.LoadEndlessSectionHardness(Memory.GameValues.sectionToTest.hardness);
        }
    }

    public void DeleteFile()
    {
        if (levelPathDisplayed == "") return;
        if (levelDataDisplayed == null) return;

        File.Delete(levelPathDisplayed);

        levelPathDisplayed = "";
        levelDataDisplayed = null;
        sectionDataDisplayed = null;

        button_Delete.gameObject.SetActive(false);

        inputField_Title.text = "";
        inputField_Author.text = "";
        inputField_Description.text = "";

        GetSavedLevels(levelFiles, LevelEditor.editorMode);
        DisplaySavedlevels();
    }
    #endregion

    public static void SetPreviewImage(RawImage img, byte[] bytes, int width, int height)
    {
        //set preview Image
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.LoadImage(bytes, false);
        tex.Apply();
        img.texture = tex;
        img.color = new Color(1, 1, 1, bytes.Length > 0 ? 1 : 0);

        //set rect size
        Vector2 targetsize = img.transform.parent.GetComponent<RectTransform>().sizeDelta;
        float targetAspect = targetsize.x / targetsize.y;
        float imageAspect = width / (float)height;
        float ratio = targetAspect - imageAspect;

        if (ratio >= 0) //higher image
        {
            Vector2 size = new Vector2(targetsize.x, targetsize.x / imageAspect);
            img.GetComponent<RectTransform>().sizeDelta = size;
        }
        else //wider image
        {
            Vector2 size = new Vector2(targetsize.y * imageAspect, targetsize.y);
            img.GetComponent<RectTransform>().sizeDelta = size;
        }
    }
}
