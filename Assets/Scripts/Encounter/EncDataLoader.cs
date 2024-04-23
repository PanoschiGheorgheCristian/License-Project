using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EncDataLoader : MonoBehaviour
{
    [SerializeField]
    GameObject Description;
    [SerializeField]
    GameObject TextButton1;
    [SerializeField]
    GameObject TextButton2;
    [SerializeField]
    GameObject TextButton3;
    // Start is called before the first frame update
    void Awake()
    {
        string json = EncounterObject.getJsonEncounter();
        EncounterObject encounterObject = JsonUtility.FromJson<EncounterObject>(json);

        Description.GetComponent<TMP_Text>().text = encounterObject.description;
        TextButton1.GetComponent<TMP_Text>().text = encounterObject.choices[0];
        TextButton2.GetComponent<TMP_Text>().text = encounterObject.choices[1];
        TextButton3.GetComponent<TMP_Text>().text = encounterObject.choices[2];
    }
}
