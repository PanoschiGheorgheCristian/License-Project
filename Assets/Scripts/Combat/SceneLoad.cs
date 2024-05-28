using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private bool isLoading;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemyManager;

    private void Awake()
    {
        isLoading = false;
    }

    void Update()
    {
        if (enemyManager.GetComponent<EnemyManage>().GetCurrentEnemy().activeSelf == false && isLoading == false)
        {
            StartCoroutine(LoadMap("You Won!"));
        }
        else if (player.GetComponent<HeroStatus>().alive <= 0 && isLoading == false)
        {
            StartCoroutine(LoadMap("You Died"));
        }
    }

    private IEnumerator LoadMap(String message)
    {
        Debug.Log(message);
        isLoading = true;
        
        string json = SaveObject.getJsonSave();
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);
        saveObject.heroHealth = player.GetComponent<HeroStatus>().health;
        if(EnemyToFight.isElite)
            saveObject.currentGold += (int)UnityEngine.Random.Range(40,60);
        else if(EnemyToFight.isBoss)
            saveObject.currentGold += (int)UnityEngine.Random.Range(100, 150);
        else
            saveObject.currentGold += (int)UnityEngine.Random.Range(20, 30);
        bool isWeaponObtained = false;
        for(int iterator = 0; iterator < saveObject.availableWeapons.Count; iterator++)
            if(EnemyToFight.currentEnemy == saveObject.availableWeapons[iterator])
            {
                isWeaponObtained = true;
                break;
            }
        if(isWeaponObtained == false)
        {
            saveObject.availableWeapons.Add(EnemyToFight.currentEnemy);
        }
        else
        {
            //add weapon exp to the weapon given
        }
        saveObject.SaveGame();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }
}
