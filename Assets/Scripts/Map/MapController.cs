using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    private bool isSceneLoading;
    public GameObject player;
    public GameObject currentStage;
    private Camera _mainCamera;
    [SerializeField] private List<GameObject> mapNodes;
    void Awake() 
    {
        _mainCamera = Camera.main;
        isSceneLoading = false;
        SetEncounters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        if(isSceneLoading == false)
            foreach(GameObject iteratorGameObject in currentStage.GetComponent<MapNode>().successors)
            {
                if(rayHit.collider.gameObject.name == iteratorGameObject.name)
                {
                    currentStage.GetComponent<MapNode>().hasPlayer = false;
                    currentStage = iteratorGameObject;
                    currentStage.GetComponent<MapNode>().hasPlayer = true;
                    player.transform.position = currentStage.transform.position + new Vector3 (0, 0.5f, -1);
                    isSceneLoading = true;
                    StartCoroutine(LoadStage());
                }
            }
    }

    private void SetEncounters()
    {
        mapNodes[0].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Start);
        mapNodes[^1].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Boss);

        float rand = UnityEngine.Random.Range(0f,2f);
        if(rand < 1f)
        {
            mapNodes[7].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[8].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);
            mapNodes[^2].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[^3].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);
        }
        else
        {
            mapNodes[7].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);
            mapNodes[8].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
            mapNodes[^2].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Camp);
            mapNodes[^3].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.HardEnemy);
        }
        for(int index = 1; index <= mapNodes.Count - 2; index++)
        {
            if(index % 8 == 7)  
            {
                index += 1;
                continue;
            }
            rand = UnityEngine.Random.Range(0f,3f);
            if(rand < 1f)
                mapNodes[index].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Enemy);
            else    if(rand < 2f)
                        mapNodes[index].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Treasure);
                    else    mapNodes[index].GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Event);
            // node.GetComponent<MapNode>().SetEncounter(MapNode.Encounter.Enemy);
        }
    }

    private IEnumerator LoadStage()
    {
        yield return new WaitForSeconds(1.5f);

        isSceneLoading = false;
        if(this.currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.Enemy || 
            this.currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.HardEnemy ||
                this.currentStage.GetComponent<MapNode>().encounter == MapNode.Encounter.Boss)
        {
            SceneManager.LoadScene("Combat", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Encounter", LoadSceneMode.Single);
        }
    }
}
