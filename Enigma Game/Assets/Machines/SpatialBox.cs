using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialBox : Machine
{
    GameObject holding;

    public override bool CanReceiveItem(string itemName)
    {
        if (holding == null)
            return true;
        return false;
    }

    public override void Process()
    {
        throw new System.NotImplementedException();
    }

    public override void ReceiveItem(GameObject item)
    {
        throw new System.NotImplementedException();
    }
}
