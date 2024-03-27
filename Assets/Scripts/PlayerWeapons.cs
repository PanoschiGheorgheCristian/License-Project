using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PlayerWeapons
{
    private static string SAVE_FOLDER;
    static readonly List<Weapon> weapons = new() {new Bow(), new Staff(), new Shield(), new Sword(), new Mace(), new CrossBow(), new SpellBook(), new Buckler(), new Rapier(), 
                                new GreatSword(), new Daggers(), new Net()};

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
        }
        else
        {
            returnWeapons.Add(weapons[0]);
            returnWeapons.Add(weapons[1]);
            returnWeapons.Add(weapons[2]);
            returnWeapons.Add(weapons[3]);
            returnWeapons.Add(weapons[4]);
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
                weapons[i] = i;
            }
            return weapons;
        }
    }
}
