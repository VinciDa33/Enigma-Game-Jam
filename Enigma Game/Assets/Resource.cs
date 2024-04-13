using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Resource
{
    public string name;
    public int amount;
    public Sprite sprite;

    public Resource(string name, Sprite sprite)
    {
        this.name = name;
        amount = 1;
        this.sprite = sprite;
    }

    public Resource(string name, int amount, Sprite sprite)
    {
        this.name = name;
        this.amount = amount;
        this.sprite = sprite;
    }
}
