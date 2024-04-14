using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Machine
{
    public override bool CanReceiveItem(string itemName)
    {
        return false;
    }

    public override void Process()
    {
        Debug.Log("Rock and Stone");
    }

    public override void ReceiveItem(GameObject item)
    {
        Destroy(item);
    }
}
