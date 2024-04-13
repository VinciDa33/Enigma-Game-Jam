using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    int tick = 0;


    public override bool CanTakeItem(string itemName)
    {
        return false;
    }

    public override void GainItem(Item item)
    {
        Debug.Log("What are you doing?");
    }

    public override bool IsInventoryFull()
    {
        return true;
    }

    public override void process()
    {
        tick++;
        if (tick >= 3)
        {
            
        }
    }

    public override void TransferItem(Item item)
    {
        Machine toTransfer = box.GetMachine(new GridPosition(gridPosition.x + (int)outputDirection.x, gridPosition.y + (int)outputDirection.y));
        if (toTransfer == null)
            return;
        if (!toTransfer.CanTakeItem(item.itemName))
            return;

        toTransfer.GainItem(item);
        item = null;
    }
}
