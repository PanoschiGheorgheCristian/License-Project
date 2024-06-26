using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{

    void Awake() 
    {
        LoadSave();    
    }

    SaveObject save = new();
    public void LoadAfterChoiceDescription()
    {
        Debug.Log("Loading after choice description...");
        GetComponent<EncDataLoader>().DisplayAfterChoice();
    }

    public void ContinueToMap()
    {
        Debug.Log("Loading map...");
        StartCoroutine(LoadMapDelay());
    }

    public void GainGold(int amount)
    {
        Debug.Log("Gained " + amount + " gold");
        SaveGame(save.currentStage, save.currentGold + amount, save.currentWeapons, save.heroHealth, save.curses, save.availableWeapons);
    }

    public void LoseGold(int amount)
    {
        Debug.Log("Lost " + amount + " gold");
        SaveGame(save.currentStage, Math.Max(save.currentGold - amount, 0), save.currentWeapons, save.heroHealth, save.curses, save.availableWeapons);
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Took " + amount + " damage");
        if(save.heroHealth <= amount)
            Debug.Log("Game over!");
        SaveGame(save.currentStage, save.currentGold, save.currentWeapons, save.heroHealth - amount, save.curses, save.availableWeapons);
    }

    public void Heal(int amount)
    {
        Debug.Log("Healed " + amount + " damage");
        SaveGame(save.currentStage, save.currentGold, save.currentWeapons, Math.Min(save.heroHealth + amount, 100), save.curses, save.availableWeapons);
    }

    public void GainCurse(string curse)
    {
        Debug.Log("Gained the " + curse + " curse!");
        bool exists = false;
        foreach(string tempString in save.curses)
        {
            if(string.Equals(tempString, curse))
                exists = true;
        }
        if(!exists)
            save.curses.Add(curse);
        SaveGame(save.currentStage, save.currentGold, save.currentWeapons, save.heroHealth, save.curses, save.availableWeapons);
    }


    public void PurifyCurse(string curse)
    {
        if(save.curses.Count > 0)
        {
            int curseNr = UnityEngine.Random.Range(0, save.curses.Count);
            while(curseNr == save.curses.Count)
                curseNr = UnityEngine.Random.Range(0, save.curses.Count);

            Debug.Log("Removed the " + save.curses[curseNr] + " curse!");
            if(string.Equals(save.curses[curseNr], "Wrathful"))
                MapController.isWrathful = false;
            save.curses.RemoveAt(curseNr);
            
            SaveGame(save.currentStage, save.currentGold, save.currentWeapons, save.heroHealth, save.curses, save.availableWeapons);
        }
    }

    public void Sleep()
    {
        Debug.Log("You go to sleep...");
        SaveGame(save.currentStage, save.currentGold, save.currentWeapons, 100, save.curses, save.availableWeapons);
    }

    public void UpgradeWeapon()
    {
        Debug.Log("Loading Blacksmith...");
        StartCoroutine(LoadSmithDelay());
    }

    public void SaveGame(string name, int gold, int[] weapons, int heroHealth, List<string> curses, List<int> availableWeapons)
    {
        save.currentStage = name;
        save.currentGold = gold;
        save.heroHealth = heroHealth;
        save.currentWeapons = weapons;
        save.curses = curses;
        save.availableWeapons = availableWeapons;

        save.SaveGame();
        Debug.Log("Saved");
    }

    public void LoadSave()
    {
        string json = SaveObject.getJsonSave();

        SaveObject tempSave = new();
        tempSave = JsonUtility.FromJson<SaveObject>(json);
        if (tempSave is not null)
        {
            save = tempSave;
        }
        else ContinueToMap();
    }

    private IEnumerator LoadMapDelay()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Map", LoadSceneMode.Single);
    }

    private IEnumerator LoadSmithDelay()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("BlackSmith", LoadSceneMode.Single);
    }
}
