using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EncounterObject
{
    private static string ENCOUNTER_FOLDER;
    public string description;
    public List<string> choices;
    public Dictionary<string, List<string>> choiceEffects;

    public EncounterObject() { }


    public static string getJsonEncounter()
    {
        return File.ReadAllText(ENCOUNTER_FOLDER);
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void GetEncounterFolder()
    {
        // int encIndex = Random.Range(1, 11);
        int encIndex = 1;
        ENCOUNTER_FOLDER = Application.dataPath + "/Encounters/Encounter" + encIndex;
    }
}
