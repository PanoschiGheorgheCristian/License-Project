using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveObject
{
    private static string SAVE_FOLDER;
    public string currentStage;
    public List<MapNode.Encounter> stages;
    public int[] enemyNumbers;
    public int currentGold;
    public int heroHealth;
    public int[] currentWeapons;
    public List<string> curses;
    public List<int> availableWeapons;
    public int[] weaponExperience;
    public int layer;

    public SaveObject() { }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(SAVE_FOLDER, json);
    }

    public static string getJsonSave()
    {
        return File.ReadAllText(SAVE_FOLDER);
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void GetSaveFolder()
    {
        SAVE_FOLDER = Application.streamingAssetsPath + "/Saves";
    }
}
