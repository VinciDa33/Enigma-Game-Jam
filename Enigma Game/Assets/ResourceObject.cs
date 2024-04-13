using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ResourceItem
{
    public string name;
    public Sprite sprite;

    public ResourceItem(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }
}
