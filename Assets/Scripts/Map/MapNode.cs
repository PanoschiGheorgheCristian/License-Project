using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public enum Encounter { Start, Enemy, HardEnemy, Camp, Event, UpgradeShop, WeaponShop, Boss };
    public Encounter encounter;
    public bool hasPlayer;
    public List<GameObject> successors;

    public void SetEncounter(Encounter encounter)
    {
        this.encounter = encounter;
    }
}
