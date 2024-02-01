using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //view height: 10 --- 4(2 up, 2 down) clear on the edges
    //view width: 18
    private int currentStage;
    private List<MapNode> nodes;
    // Start is called before the first frame update
    void Start()
    {
        nodes = new List<MapNode>();
        currentStage = 0;

        List<int> successors = new List<int>(){1,2,3};
        Vector3 coordinates = new Vector3(-7.5f, 0, 0);
        MapNode startNode = new MapNode(coordinates, 0, 0, successors);
        nodes.Add(startNode);
        nodes[0].printHelloWorld();
    }

    private void populateMap()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
