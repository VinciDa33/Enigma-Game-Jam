using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector] public string itemName;

    public void SetItem(string itemName, Sprite sprite)
    {
        this.itemName = itemName;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Show(bool b)
    {
        GetComponent<SpriteRenderer>().enabled = b;
    }

}