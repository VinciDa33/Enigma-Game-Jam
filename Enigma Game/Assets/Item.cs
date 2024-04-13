using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Resource resource { get; private set; }

    public void SetItem(Resource resource)
    {
        this.resource = resource;
        GetComponent<SpriteRenderer>().sprite = resource.sprite;
    }

    public void Show(bool b)
    {
        GetComponent<SpriteRenderer>().enabled = b;
    }
}