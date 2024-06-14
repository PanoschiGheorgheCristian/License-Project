using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class SmithController : MonoBehaviour
{
    private Camera _mainCamera;
    SaveObject save = new();
    [SerializeField] List<GameObject> weaponIcons;
    [SerializeField] List<GameObject> LevelBoxes;
    [SerializeField] List<GameObject> ExperienceBoxes;
    [SerializeField] List<HealthBar> healthBars;
    [SerializeField] GameObject goldAmountText;
    [SerializeField] GameObject returnButton;
    int[] weaponLevels;

    void Awake()
    {
        _mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 15; i++)
        {
            weaponIcons[i].SetActive(false);
        }
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        string json = SaveObject.getJsonSave();

        SaveObject tempSave = new();
        tempSave = JsonUtility.FromJson<SaveObject>(json);

        if (tempSave is not null)
        {
            save = tempSave;
            weaponLevels = PlayerWeapons.GetAllWeaponsLevels();
            for (int iterator = 0; iterator < weaponLevels.Length; iterator++)
            {
                LevelBoxes[iterator].GetComponent<TMP_Text>().text = weaponLevels[iterator].ToString();
                switch (weaponLevels[iterator])
                {
                    case 1:
                        ExperienceBoxes[iterator].GetComponent<TMP_Text>().text = save.weaponExperience[iterator].ToString() + " / 500";
                        healthBars[iterator].GetComponent<HealthBar>().SetMaxHealth(500);
                        healthBars[iterator].GetComponent<HealthBar>().SetHealth(save.weaponExperience[iterator]);
                        break;
                    case 2:
                        ExperienceBoxes[iterator].GetComponent<TMP_Text>().text = save.weaponExperience[iterator].ToString() + " / 1500";
                        healthBars[iterator].GetComponent<HealthBar>().SetMaxHealth(1500);
                        healthBars[iterator].GetComponent<HealthBar>().SetHealth(save.weaponExperience[iterator]);
                        break;
                    case 3:
                        ExperienceBoxes[iterator].GetComponent<TMP_Text>().text = "Max Level";
                        healthBars[iterator].GetComponent<HealthBar>().SetHealth(1500);
                        break;
                    default:
                        ExperienceBoxes[iterator].GetComponent<TMP_Text>().text = "Error occured";
                        break;
                }
            }

            for(int i = 0; i < save.availableWeapons.Count; i++)
            {
                weaponIcons[save.availableWeapons[i]].SetActive(true);
            }

            goldAmountText.GetComponent<TMP_Text>().text = "Gold: " + save.currentGold.ToString();
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.started)    return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        for(int iterator = 0; iterator < 15; iterator++)
        {
            if(weaponIcons[iterator].name == rayHit.collider.gameObject.name)
            {
                var expTarget = weaponLevels[iterator] switch
                {
                    1 => 500,
                    2 => 1500,
                    3 => 999999,
                    _ => -1,
                };
                if (save.currentGold >= expTarget - save.weaponExperience[iterator])
                {
                    save.currentGold = save.currentGold - expTarget + save.weaponExperience[iterator];
                    save.weaponExperience[iterator] = expTarget;
                    save.SaveGame();
                    LoadGame();
                }
                else
                {
                    //show that you don't have enough gold
                }
            }
        }
    }

    public void ReturnToMap()
    {
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }

    public void SaveGame(string name, int gold, int[] weapons, int heroHealth, List<string> curses, List<int> availableWeapons, int[] weaponExperience)
    {
        save.currentStage = name;
        save.currentGold = gold;
        save.heroHealth = heroHealth;
        save.currentWeapons = weapons;
        save.curses = curses;
        save.availableWeapons = availableWeapons;
        save.weaponExperience = weaponExperience;

        save.SaveGame();
        Debug.Log("Saved");
    }
}
