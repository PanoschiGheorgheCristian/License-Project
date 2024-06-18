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

        int moneyDebuff = 0;
        foreach (string iteratorString in saveObject.curses)
        {
            if (string.Equals("FeyTouched", iteratorString))
            {
                moneyDebuff = -40;
            }
        }

        if (EnemyToFight.isElite)
        {
            double goldGained = UnityEngine.Random.Range(100, 150);
            saveObject.currentGold += (int) (goldGained + goldGained * moneyDebuff / 100);
        }
        else if(EnemyToFight.isBoss)
        {
            double goldGained = UnityEngine.Random.Range(200, 300);
            saveObject.currentGold += (int)(goldGained + goldGained * moneyDebuff / 100);
        }
        else
        {
            double goldGained = UnityEngine.Random.Range(50, 100);
            saveObject.currentGold += (int)(goldGained + goldGained * moneyDebuff / 100);
        }

        bool isWeaponObtained = false;
        for(int iterator = 0; iterator < saveObject.availableWeapons.Count; iterator++)
            if(EnemyToFight.currentEnemy == saveObject.availableWeapons[iterator])
            {
                isWeaponObtained = true;
                saveObject.weaponExperience[saveObject.availableWeapons[iterator]] = saveObject.weaponExperience[saveObject.availableWeapons[iterator]] + 500;
                break;
            }
        if(isWeaponObtained == false)
        {
            if(!EnemyToFight.isBoss)
                saveObject.availableWeapons.Add(EnemyToFight.currentEnemy);
            else
            {
                EnemyToFight.layer++;
            }
        }
        
        saveObject.SaveGame();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }
}
