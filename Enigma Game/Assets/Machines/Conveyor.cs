using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Machine
{

    GameObject itemObject;
    int tick = 0;

    public override bool CanTakeItem(string itemName)
    {
        if (itemObject == null)
            return true;
        return false;
    }

    public override void GainItem(GameObject transferItem)
    {
        itemObject = transferItem;
        Debug.Log("OBTAINED ITEM:" + itemObject);
        itemObject.GetComponent<Item>().Show(true);
    }

    public override bool IsInventoryFull()
    {
        if (itemObject == null)
            return false;
        return true;
    }

    public override void process()
    {
        return;
        tick++;
        if (itemObject == null)
            tick = 0;

        if (tick == 2 && itemObject != null)
        {
            TransferItem();
            tick = 0;
        }
    }

    public override void TransferItem()
    {
        return;
        Machine toTransfer = box.GetMachine(new GridPosition(gridPosition.x + (int)transform.right.x, gridPosition.y + (int)transform.right.y));
        if (toTransfer == null)
            return;
        if (!toTransfer.CanTakeItem(itemObject.GetComponent<Item>().itemName))
            return;

        toTransfer.GainItem(itemObject);
        itemObject = null;
        tick = 0;
    }

    private void Update()
    {
        if (itemObject == null)
            return;
        Debug.Log(itemObject);
        itemObject.transform.position = Vector2.MoveTowards(itemObject.transform.position, transform.position, 100f * Time.deltaTime);
    }
}
