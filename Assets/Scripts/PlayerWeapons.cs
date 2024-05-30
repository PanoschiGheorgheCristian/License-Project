using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PlayerWeapons
{
    private static string SAVE_FOLDER;
    static readonly List<Weapon> weapons = new() {new Bow(), new IronBow(), new ThrowingKnives(), 
                                                new Staff(), new Sceptre(), new Circlet(), 
                                                new Shield(), new SpikedShield(), new MonkeyEarrings(), 
                                                new Sword(), new Waraxe(), new Spear(),
                                                new Mace(), new Daggers(), new Flail()};

    public static List<Weapon> GetWeapons() 
    {
        SAVE_FOLDER = Application.dataPath + "/Saves";
        string json = File.ReadAllText(SAVE_FOLDER);
        List<Weapon> returnWeapons = new();

        SaveObject save = JsonUtility.FromJson<SaveObject>(json);
        if (save is not null)
        {
            for(int i = 0; i < 5; i++)
            {
                UpdateWeaponToCurrentLevel(save.currentWeapons[i]);
                returnWeapons.Add(weapons[save.currentWeapons[i]]);
            }
            HeroShield.shieldEquipped = save.currentWeapons[2] - 5;
        }
        else
        {
            returnWeapons.Add(weapons[0]);
            returnWeapons.Add(weapons[3]);
            returnWeapons.Add(weapons[6]);
            returnWeapons.Add(weapons[9]);
            returnWeapons.Add(weapons[12]);
            HeroShield.shieldEquipped = 1;
        }
        return returnWeapons;
    }

    public static int[] GetWeaponsIndexes()
    {
        SAVE_FOLDER = Application.dataPath + "/Saves";
        string json = File.ReadAllText(SAVE_FOLDER);

        SaveObject save = JsonUtility.FromJson<SaveObject>(json);
        if (save is not null)
        {
            for (int i = 0; i < 5; i++)
            {
                UpdateWeaponToCurrentLevel(save.currentWeapons[i]);
            }
            return save.currentWeapons;
        }
        else
        {
            int[] weapons = new int[5];
            for (int i = 0; i < 5; i++)
            {
                weapons[i] = i * 3;
            }
            return weapons;
        }
    }

    public static void UpdateWeaponToCurrentLevel(int index)
    {
        SAVE_FOLDER = Application.dataPath + "/Saves";
        string json = File.ReadAllText(SAVE_FOLDER);

        SaveObject save = JsonUtility.FromJson<SaveObject>(json);
        if (save is not null)
        {
            int weaponExp = save.weaponExperience[index];
            switch (GetWeaponLevel(weaponExp))
            {
                case 1:
                    break;
                case 2:
                    weapons[index].UpdateToLevel2();
                    break;
                case 3:
                    weapons[index].UpdateToLevel3();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Attempted to update the level of a weapon but the save is null. There is no exp to read.");
        }
    }

    private static int GetWeaponLevel(int weaponExp)
    {
        if (weaponExp < 500)
            return 1;
        else if (weaponExp < 1500)
            return 2;
        else
            return 3;
    }

    public static int[] GetAllWeaponsLevels()
    {
        SAVE_FOLDER = Application.dataPath + "/Saves";
        string json = File.ReadAllText(SAVE_FOLDER);

        SaveObject save = JsonUtility.FromJson<SaveObject>(json);
        int[] levels = new int[15];
        if (save is not null)
        {
            for(int index = 0; index < 15; index++)
            {
                levels[index] = GetWeaponLevel(save.weaponExperience[index]);
            }
        }
        else
        {
            for (int index = 0; index < 15; index++)
            {
                levels[index] = 1;
            }
        }
        return levels;
    }
}
