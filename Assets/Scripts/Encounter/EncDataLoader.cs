using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EncDataLoader : MonoBehaviour
{
    [SerializeField] GameObject Description;
    [SerializeField] GameObject Button1;
    [SerializeField] GameObject Button2;
    [SerializeField] GameObject Button3;
    [SerializeField] GameObject Button4;
    [SerializeField] GameObject TextButton1;
    [SerializeField] GameObject TextButton2;
    [SerializeField] GameObject TextButton3;
    private string[] afterChoiceDescriptions;
    private string descriptionAfterSpecificChoice;
    private string[] choiceEffectsStrings;
    // Start is called before the first frame update
    void Awake()
    {
        Button4.SetActive(false);

        string json = EncounterObject.GetJsonEncounter(CampFlag.isCamp);
        EncounterObject encounterObject = JsonUtility.FromJson<EncounterObject>(json);

        Description.GetComponent<TMP_Text>().text = encounterObject.description;
        TextButton1.GetComponent<TMP_Text>().text = encounterObject.choices[0];
        TextButton2.GetComponent<TMP_Text>().text = encounterObject.choices[1];
        TextButton3.GetComponent<TMP_Text>().text = encounterObject.choices[2];

        choiceEffectsStrings = new string[3];
        choiceEffectsStrings[0] = encounterObject.choiceEffects[0];
        choiceEffectsStrings[1] = encounterObject.choiceEffects[1];
        choiceEffectsStrings[2] = encounterObject.choiceEffects[2];

        afterChoiceDescriptions = encounterObject.afterChoiceDescription;
    }

    public void DoButtonActions(int buttonNr)
    {
        descriptionAfterSpecificChoice = afterChoiceDescriptions[buttonNr];

        ButtonActions buttonActions = GetComponent<ButtonActions>();

        string[] actions = choiceEffectsStrings[buttonNr].Split(',');
        foreach (string action in actions)
        {
            if(!String.IsNullOrEmpty(action))
                switch (action[..1])
                {
                    case "g":
                        buttonActions.GainGold(Convert.ToInt32(action[1..^0]));
                        break;
                    case "l":
                        buttonActions.LoseGold(Convert.ToInt32(action[1..^0]));
                        break;
                    case "d":
                        buttonActions.TakeDamage(Convert.ToInt32(action[1..^0]));
                        break;
                    case "h":
                        buttonActions.Heal(Convert.ToInt32(action[1..^0]));
                        break;
                    case "c":
                        buttonActions.GainCurse(action[1..^0]);
                        break;
                    case "p":
                        buttonActions.PurifyCurse(action[1..^0]);
                        break;
                    case "s":
                        buttonActions.Sleep();
                        break;
                    case "b":
                        buttonActions.UpgradeWeapon();
                        break;
                    case "m":
                        buttonActions.LoadAfterChoiceDescription();
                        break;
                    default:
                        Debug.Log("Unknown choice effect in encounter JSON");
                        break;
                }
        }
    }

    public void DisplayAfterChoice()
    {
        Description.GetComponent<TMP_Text>().text = descriptionAfterSpecificChoice;
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);
        Button4.SetActive(true);
    }
}

public static class CampFlag
{
    public static bool isCamp;
}