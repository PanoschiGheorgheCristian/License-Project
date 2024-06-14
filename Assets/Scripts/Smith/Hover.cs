using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color defaultColor;

    private void OnMouseOver()
    {
        this.transform.GetChild(2).GetComponent<SpriteRenderer>().color = hoverColor;;
    }

    private void OnMouseExit()
    {
        this.transform.GetChild(2).GetComponent<SpriteRenderer>().color = defaultColor;
    }
}
