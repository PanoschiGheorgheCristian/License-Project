using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.IO;

public class MapController : MonoBehaviour
{
    SaveObject save = new();
    private bool isSceneLoading;
    public GameObject player;
    public GameObject currentStage;
    private Camera _mainCamera;
    [SerializeField] private List<GameObject> mapNodes;

    void Awake()
    {
        _mainCamera = Camera.main;
        isSceneLoading = false;
        SetEncounters();

        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        //Make animations
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        if (isSceneLoading == false)
            foreach (GameObject iteratorGameObject in currentStage.GetComponent<MapNode>().successors)
            {
                if (rayHit.collider.gameObject.name == iteratorGameObject.name)
                {
                    currentStage.GetComponent<MapNode>().hasPlayer = false;
                    currentStage = iteratorGameObject;
                    currentStage.GetComponent<MapNode>().hasPlayer = true;
                    player.transform.position = currentStage.transform.position + new Vector3(0, 0.5f, -1);
                    SaveGame(currentStage.name, save.currentGold, PlayerWeapons.GetWeaponsIndexes(), save.heroHealth);
                    isSceneLoading = true;
                    StartCoroutine(LoadStage());
                }
            }
    }

    private void SetEncounters()
    {
        //WeaponShop is a rare encounter
        //UpgradeShop is a mechanic of the camp
        //Getting new weapons is manly done through defeating hard enemies that are wielding the weapon

        mapNodes[0].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Start);
        mapNodes[^1].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Boss);
        mapNodes[^2].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);

        float rand = UnityEngine.Random.Range(0f, 2f);
        if (rand < 1f)
        {
            mapNodes[7].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[8].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);
            mapNodes[^3].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[^4].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Event);
        }
        else
        {
            mapNodes[7].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);
            mapNodes[8].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[^3].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Event);
            mapNodes[^4].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
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
                mapNodes[index].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Enemy);
            else
                mapNodes[index].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Event);
            // node.GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Enemy);
        }
    }

    public void SaveGame(string name, int gold, int[] weapons, int heroHealth)
    {
        save.currentStage = name;
        save.currentGold = gold;
        save.heroHealth = heroHealth;
        save.currentWeapons = weapons;

        save.SaveGame();
        Debug.Log("Saved");
    }

    public void LoadGame()
    {
        string json = SaveObject.getJsonSave();

        SaveObject tempSave = new();
        tempSave = JsonUtility.FromJson<SaveObject>(json);
        if (tempSave is not null)
        {
            save = tempSave;
            currentStage = GameObject.Find(save.currentStage);
            GameObject.Find("Player").transform.position = currentStage.transform.position + new Vector3(0, 0.5f, -1);
        }
        else
            StartGame();
    }

    private void StartGame()
    {
        currentStage = GameObject.Find("Start");
        // foreach (Weapon weapon in playerWeapons.GetWeapons())
        // {
        //     Debug.Log(weapon.GetType().Name);
        // }
        SaveGame(currentStage.name, 0, PlayerWeapons.GetWeaponsIndexes(), 100);
    }

    private IEnumerator LoadStage()
    {
        yield return new WaitForSeconds(1.5f);

        isSceneLoading = false;
        if (currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.Enemy ||
            currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.HardEnemy ||
                currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.Boss)
        {
            SceneManager.LoadScene("Combat", LoadSceneMode.Single);
        }
        else if (currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.Camp ||
            currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.Event)
        {
            SceneManager.LoadScene("Encounter", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Shop", LoadSceneMode.Single);
        }
    }
}
