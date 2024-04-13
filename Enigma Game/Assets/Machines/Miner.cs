using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    int tick = 0;
    [SerializeField] private GameObject itemPrefab;
    Item holding = null;

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
            if (holding == null)
            {
                Debug.Log("SUMMON ITEM");
                GameObject itemObject = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                Item item = itemObject.GetComponent<Item>();
                item.SetItem("IronOre", ResourceManager.instance.GetGameItem("IronOre").sprite);
                holding = item;
            }
            TransferItem(holding);
        }
    }

    public override void TransferItem(Item item)
    {
        GridPosition transferPosition = new GridPosition(gridPosition.x + (int)transform.right.x, gridPosition.y + (int)transform.right.y);
        Machine toTransfer = box.GetMachine(transferPosition);
        if (toTransfer == null)
            return;
        if (!toTransfer.CanTakeItem(item.itemName))
            return;

        Debug.Log("TRANSFER ITEM");
        toTransfer.GainItem(item);
        holding = null;
        Debug.Log(toTransfer.IsInventoryFull());
    }
}
