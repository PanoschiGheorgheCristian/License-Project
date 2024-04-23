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
        saveObject.currentGold += (int)UnityEngine.Random.Range(20,30);
        saveObject.SaveGame();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }
}
