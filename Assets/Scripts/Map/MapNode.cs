using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public bool hasPlayer;
    public List<GameObject> successors;
    
    public void printHelloWorld()
    {
        Debug.Log("Hello World!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
