using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Handles saving and loading files
/// </summary>
public static class SaveManager
{
    public static void SaveLevel(LevelData data, string folder, string fileName)
    {
        //paths
        string directoryPath = $"{Application.persistentDataPath}{(folder == null ? "" : $"/{folder}")}";
        string fullPath = $"{directoryPath}/{fileName}.level.json";

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

        //write json
        string json = JsonUtility.ToJson(data, false);
        File.WriteAllText(fullPath, json);

        Debug.Log("Saved level at " + fullPath);
    }

    public static LevelData LoadLevel(string folder, string fileName)
    {
        string fullPath = $"{Application.persistentDataPath}{(folder == null ? "" : $"/{folder}")}/{fileName}.level.json";
        
        string json = "";
        try
        {
            json = File.ReadAllText(fullPath);
        }
        catch (System.Exception)
        {
            Debug.LogError($"Failed to read JSON data at path: {fullPath}");
            throw;
        }

        return JsonUtility.FromJson<LevelData>(json);
    }
    public static LevelData LoadLevel(string fullPath)
    {
        string json = "";
        try
        {
            json = File.ReadAllText(fullPath);
        }
        catch (System.Exception)
        {
            Debug.LogError($"Failed to read JSON data at path: {fullPath}");
            throw;
        }

        return JsonUtility.FromJson<LevelData>(json);
    }
    public static LevelData LoadLevelJson(string json)
    {
        return JsonUtility.FromJson<LevelData>(json);
    }

    public static void SaveEndlessSection(EndlessSection data, string folder, string fileName)
    {
        //paths
        string directoryPath = $"{Application.persistentDataPath}{(folder == null ? "" : $"/{folder}")}";
        string fullPath = $"{directoryPath}/{fileName}.section.json";

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

        //write json
        string json = JsonUtility.ToJson(data, false);
        File.WriteAllText(fullPath, json);

        Debug.Log("Saved endless section at " + fullPath);
    }

    public static EndlessSection SaveEndlessSection(string folder, string fileName)
    {
        string fullPath = $"{Application.persistentDataPath}{(folder == null ? "" : $"/{folder}")}/{fileName}.section.json";

        string json = "";
        try
        {
            json = File.ReadAllText(fullPath);
        }
        catch (System.Exception)
        {
            Debug.LogError($"Failed to read JSON data at path: {fullPath}");
            throw;
        }

        return JsonUtility.FromJson<EndlessSection>(json);
    }
    public static EndlessSection SaveEndlessSection(string fullPath)
    {
        string json = "";
        try
        {
            json = File.ReadAllText(fullPath);
        }
        catch (System.Exception)
        {
            Debug.LogError($"Failed to read JSON data at path: {fullPath}");
            throw;
        }

        return JsonUtility.FromJson<EndlessSection>(json);
    }
}