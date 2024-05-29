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
}
