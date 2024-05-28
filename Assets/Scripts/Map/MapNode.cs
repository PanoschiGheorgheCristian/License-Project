using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public enum Encounter { Start, Enemy, HardEnemy, Camp, Event, Boss };
    public Encounter encounter;
    public bool hasPlayer;
    public List<GameObject> successors;
    public int enemyNumber;

    public void SetEncounter(Encounter encounter)
    {
        this.encounter = encounter;
    }
    public void SetRandomEnemy()
    {
        this.enemyNumber = UnityEngine.Random.Range(0, 15);
    }
}
