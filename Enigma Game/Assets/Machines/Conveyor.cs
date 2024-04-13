using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Machine
{
    
    Item item = null;
    int tick = 0;


    public override bool CanTakeItem(string itemName)
    {
        if (item == null)
            return true;
        return false;
    }

    public override void GainItem(Item item)
    {
        this.item = item;
        item.Show(true);
    }

    public override bool IsInventoryFull()
    {
        if (item == null)
            return false;
        return true;
    }

    public override void process()
    {
        tick++;
        if (item == null)
            tick = 0;

        if (tick == 2 && item != null)
        {
            TransferItem(item);
            tick = 0;
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
        tick = 0;
    }

    private void Update()
    {
        if (item == null)
            return;

        item.transform.position = Vector2.MoveTowards(item.transform.position, transform.position, 100f * Time.deltaTime);
    }
}
