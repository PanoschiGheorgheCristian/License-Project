using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{
    private bool hasPlayer;
    private Vector3 coordinates;
    private int stageNr;
    private int lineNr;
    private List<int> successorsNr;

    public MapNode(Vector3 coordinates, int stageNr, int lineNr, List<int> successors)
    {
        hasPlayer = false;
        this.coordinates = coordinates;
        this.stageNr = stageNr;
        this.lineNr = lineNr;
        this.successorsNr = new List<int>(successors);
    }
    
    public void printHelloWorld()
    {
        Debug.Log("Hello World!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
