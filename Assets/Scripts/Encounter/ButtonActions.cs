using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public void LoadMap()
    {
        Debug.Log("Loading map...");
        StartCoroutine(LoadMapDelay());
    }

    private IEnumerator LoadMapDelay()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Map", LoadSceneMode.Single); 
    }
}
