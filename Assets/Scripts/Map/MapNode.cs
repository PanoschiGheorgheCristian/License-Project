using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public enum Encounter {Start, Enemy, HardEnemy, Camp, Treasure, Event, Boss};
    public Encounter encounter;
    public bool hasPlayer;
    public List<GameObject> successors;
    // Update is called once per frame
    void Update()
    {
        //move event picture around
    }

    public void SetEncounter(Encounter encounter)
    {
        this.encounter = encounter;
    }
}
