using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EncounterObject
{
    private static string ENCOUNTER_FOLDER;
    public string description;
    public string[] choices;
    public string[] choiceEffects;


    public EncounterObject() { }


    public static string GetJsonEncounter(bool isCamp)
    {
        if(isCamp)
            ENCOUNTER_FOLDER = Application.dataPath + "/Encounters/Camp";
        return File.ReadAllText(ENCOUNTER_FOLDER);
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void GetEncounterFolder()
    {
        int encIndex = Random.Range(1, 11);
        while(encIndex == 11)
            encIndex = Random.Range(1, 11);
        ENCOUNTER_FOLDER = Application.dataPath + "/Encounters/Encounter" + encIndex;
    }
}
