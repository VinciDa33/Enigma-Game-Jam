using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Machine
{

    Item heldItem;
    int tick = 0;

    public override bool CanTakeItem(string itemName)
    {
        if (heldItem == null)
            return true;
        return false;
    }

    public override void GainItem(Item transferItem)
    {
        heldItem = transferItem;
        Debug.Log("OBTAINED ITEM:" + heldItem);
        heldItem.Show(true);
    }

    public override bool IsInventoryFull()
    {
        if (heldItem == null)
            return false;
        return true;
    }

    public override void process()
    {
        return;
        tick++;
        if (heldItem == null)
            tick = 0;

        if (tick == 2 && heldItem != null)
        {
            TransferItem(heldItem);
            tick = 0;
        }
    }

    public override void TransferItem(Item item)
    {
        return;
        Machine toTransfer = box.GetMachine(new GridPosition(gridPosition.x + (int)transform.right.x, gridPosition.y + (int)transform.right.y));
        if (toTransfer == null)
            return;
        if (!toTransfer.CanTakeItem(item.itemName))
            return;

        toTransfer.GainItem(item);
        heldItem = null;
        tick = 0;
    }

    private void Update()
    {
        Debug.Log(heldItem);
        if (heldItem == null)
            return;
        heldItem.transform.position = Vector2.MoveTowards(heldItem.transform.position, transform.position, 100f * Time.deltaTime);
    }
}
