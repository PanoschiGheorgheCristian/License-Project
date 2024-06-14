using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class MapController : MonoBehaviour
{
    SaveObject save = new();
    private bool isSceneLoading;
    public GameObject player;
    public GameObject currentStage;
    private Camera _mainCamera;
    [SerializeField] private List<GameObject> mapNodes;
    private bool isInventoryOpen = false;
    [SerializeField] private GameObject inventory;
    [SerializeField] private List<GameObject> weaponsInInventory;
    [SerializeField] private List<GameObject> equippedWeaponSlots;
    [SerializeField] private List<GameObject> LevelsTextObjects;
    [SerializeField] private GameObject GoldAmountTextObject;
    [SerializeField] private GameObject HealthAmountTextObject;
    [SerializeField] private GameObject CursesTextObject;

    void Awake()
    {
        _mainCamera = Camera.main;
        isSceneLoading = false;
        if(save.stages == null)
            SetEncounters();

        LoadGame();
        InitializeInventory();
        if (save.heroHealth <= 0)
            EndGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(isInventoryOpen == false)
            {
                isInventoryOpen = true;
                inventory.SetActive(true);
            }
            else
            {
                isInventoryOpen = false;
                inventory.SetActive(false);
            }
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        if (isSceneLoading == false)
            if(isInventoryOpen == false)
            {
                foreach (GameObject iteratorGameObject in currentStage.GetComponent<MapNode>().successors)
                {
                    if (rayHit.collider.gameObject.name == iteratorGameObject.name)
                    {
                        currentStage.GetComponent<MapNode>().hasPlayer = false;
                        currentStage = iteratorGameObject;
                        currentStage.GetComponent<MapNode>().hasPlayer = true;
                        player.transform.position = currentStage.transform.position + new Vector3(0, 0.5f, -1);
                        SaveGame(currentStage.name, save.currentGold, PlayerWeapons.GetWeaponsIndexes(), save.heroHealth, save.curses, save.availableWeapons, save.weaponExperience, save.stages, save.layer);
                        isSceneLoading = true;
                        StartCoroutine(LoadStage());
                    }
                }
            }
            else
            {
                for(int i = 0; i < save.availableWeapons.Count; i++)
                {
                    int iterator = save.availableWeapons[i];
                    if (rayHit.collider.gameObject.name == weaponsInInventory[iterator].name)
                    {
                        equippedWeaponSlots[iterator/3].GetComponent<SpriteRenderer>().sprite = weaponsInInventory[iterator].GetComponent<SpriteRenderer>().sprite;

                        int[] tempArray = PlayerWeapons.GetWeaponsIndexes();

                        // Change color of the previously equipped weapon to clear white to show that it can be reequipped
                        weaponsInInventory[tempArray[iterator/3]].GetComponent<SpriteRenderer>().color = Color.white;

                        tempArray[iterator / 3] = iterator;
                        Color tempColor = Color.white;
                        tempColor.a = 0.4f;
                        weaponsInInventory[iterator].GetComponent<SpriteRenderer>().color = tempColor;
                        SaveGame(currentStage.name, save.currentGold, tempArray, save.heroHealth, save.curses, save.availableWeapons, save.weaponExperience, save.stages, save.layer);

                    }
                }
            }
    }

    private void InitializeInventory()
    {
        int[] tempArray = PlayerWeapons.GetWeaponsIndexes();
        for(int iterator = 0; iterator < 5; iterator ++)
        {
            equippedWeaponSlots[iterator].GetComponent<SpriteRenderer>().sprite = weaponsInInventory[tempArray[iterator]].GetComponent<SpriteRenderer>().sprite;

            Color tempColor = Color.white;
            tempColor.a = 0.4f;
            weaponsInInventory[tempArray[iterator]].GetComponent<SpriteRenderer>().color = tempColor;
        }
        for(int iterator = 0; iterator < 15; iterator++)
        {
            weaponsInInventory[iterator].SetActive(false);
        }
        for(int iterator = 0; iterator < save.availableWeapons.Count; iterator++)
        {
            weaponsInInventory[save.availableWeapons[iterator]].SetActive(true);
        }
        GameObject.Find("Inventory").SetActive(false);
    }

    private void SetEncounters()
    {
        //UpgradeShop is a mechanic of the camp
        //Getting new weapons is manly done through defeating enemies that are wielding the weapon

        mapNodes[0].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Start);
        mapNodes[^1].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Boss);
        mapNodes[^1].GetComponent<MapNode>().enemyNumber = 15;
        mapNodes[^2].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);

        float rand = UnityEngine.Random.Range(0f, 2f);
        if (rand < 1f)
        {
            mapNodes[7].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[7].GetComponent<MapNode>().SetRandomEnemy();
            mapNodes[8].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);
            mapNodes[^3].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[^3].GetComponent<MapNode>().SetRandomEnemy();
            mapNodes[^4].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Event);
        }
        else
        {
            mapNodes[7].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);
            mapNodes[8].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[8].GetComponent<MapNode>().SetRandomEnemy();
            mapNodes[^3].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Event);
            mapNodes[^4].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[^4].GetComponent<MapNode>().SetRandomEnemy();
        }
        for (int index = 1; index <= mapNodes.Count - 3; index++)
        {
            if (index % 8 == 7)
            {
                index += 1;
                continue;
            }
            rand = UnityEngine.Random.Range(0f, 3f);
            if (rand < 2f)
            {
                mapNodes[index].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Enemy);
                mapNodes[index].GetComponent<MapNode>().SetRandomEnemy();
            }
            else
                mapNodes[index].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Event);
        }
    }

    private void LoadEncounters()
    {
        for(int i=0; i<18; i++)
        {
            mapNodes[i].GetComponent<MapNode>().SetEncounter(save.stages[i]);
        }
    }

    public void SaveGame(string name, int gold, int[] weapons, int heroHealth, List<string> curses, List<int> availableWeapons, int[] weaponExperience, List<MapNode.Encounter> stages, int layer)
    {
        save.currentStage = name;
        save.stages = stages;
        save.currentGold = gold;
        save.heroHealth = heroHealth;
        save.currentWeapons = weapons;
        save.curses = curses;
        save.availableWeapons = availableWeapons;
        save.weaponExperience = weaponExperience;
        save.layer = layer;

        save.SaveGame();
        Debug.Log("Saved");
    }

    public void LoadGame()
    {
        string json = SaveObject.getJsonSave();

        SaveObject tempSave = new();
        tempSave = JsonUtility.FromJson<SaveObject>(json);

        int[] weaponLevels = PlayerWeapons.GetAllWeaponsLevels();
        for (int iterator = 0; iterator < weaponLevels.Length; iterator++)
        {
            LevelsTextObjects[iterator].GetComponent<TMP_Text>().text = weaponLevels[iterator].ToString();
        }

        if (tempSave is not null)
        {
            save = tempSave;
            if (save.layer != EnemyToFight.layer)
            {
                SetEncounters();
                save.layer = EnemyToFight.layer;
                SaveGame("Start", save.currentGold, save.currentWeapons, save.heroHealth, save.curses, save.availableWeapons, save.weaponExperience, save.stages, save.layer);
            }
            LoadEncounters();
            currentStage = GameObject.Find(save.currentStage);
            GameObject.Find("Player").transform.position = currentStage.transform.position + new Vector3(0, 0.5f, -1);
        }
        else
        {
            StartGame();
        }
        GoldAmountTextObject.GetComponent<TMP_Text>().text = "Gold:" + save.currentGold.ToString();
        HealthAmountTextObject.GetComponent<TMP_Text>().text = "Health:" + save.heroHealth.ToString();
        string tempString = "Curses:";
        foreach(string s in save.curses)
        {
            tempString = tempString + s + "<br>";
        }
        CursesTextObject.GetComponent<TMP_Text>().text = tempString;
    }

    private void StartGame()
    {
        currentStage = GameObject.Find("Start");
        List<string> curses = new();
        List<MapNode.Encounter> stages = new();
        for(int i=0; i < 19; i++)
        {
            stages.Add(mapNodes[i].GetComponent<MapNode>().encounter);
        }
        SaveGame(currentStage.name, 0, PlayerWeapons.GetWeaponsIndexes(), 100, curses, new List<int> {0,3,6,9,12}, new int[15], stages, 1);
    }

    private IEnumerator LoadStage()
    {
        yield return new WaitForSeconds(1.5f);

        isSceneLoading = false;
        EnemyToFight.layer = save.layer;
        if (currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.Enemy)
        {
            EnemyToFight.isElite = false;
            EnemyToFight.isBoss = false;
            EnemyToFight.currentEnemy = currentStage.GetComponent<MapNode>().enemyNumber;
            SceneManager.LoadScene("Combat", LoadSceneMode.Single);
        }
        else if (currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.HardEnemy)
        {
            EnemyToFight.isElite = true;
            EnemyToFight.isBoss = false;
            EnemyToFight.currentEnemy = currentStage.GetComponent<MapNode>().enemyNumber;
            SceneManager.LoadScene("Combat", LoadSceneMode.Single);
        }
        else if (currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.Boss)
        {
            EnemyToFight.isElite = false;
            EnemyToFight.isBoss = true;
            EnemyToFight.bossNr = save.layer;
            EnemyToFight.currentEnemy = currentStage.GetComponent<MapNode>().enemyNumber;
            SceneManager.LoadScene("Combat", LoadSceneMode.Single);
        }
        else if (currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.Camp)
        {
            CampFlag.isCamp = true;
            SceneManager.LoadScene("Encounter", LoadSceneMode.Single);
        }
        else
        {
            CampFlag.isCamp = false;
            SceneManager.LoadScene("Encounter", LoadSceneMode.Single);
        }
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER!");
        System.IO.File.WriteAllText(Application.dataPath + "/Saves", string.Empty);
        StartGame();
        currentStage = GameObject.Find("Start");
        GameObject.Find("Player").transform.position = currentStage.transform.position + new Vector3(0, 0.5f, -1);
    }
}
