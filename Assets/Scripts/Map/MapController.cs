using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject player;
    public GameObject currentStage;
    private Camera _mainCamera;
    void Awake() 
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position = currentStage.transform.position + new Vector3 (0, 0.5f, -1);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if(!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        foreach(GameObject iteratorGameObject in currentStage.GetComponent<MapNode>().successors)
        {
            if(rayHit.collider.gameObject.name == iteratorGameObject.name)
            {
                currentStage = iteratorGameObject;
            }
        }
    }
}
