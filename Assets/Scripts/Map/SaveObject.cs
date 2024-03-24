using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject
{
    public string currentStage;
    public int currentGold;
    public Weapon[] currentWeapons;

    public SaveObject() {
        currentStage = "Start";
        currentGold = 0;
        currentWeapons = new Weapon[5] {new Bow(), new Staff(), new Shield(), new Sword(), new Mace()};
    }
}
