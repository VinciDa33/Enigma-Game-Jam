using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    int tick = 0;
    [SerializeField] private GameObject itemPrefab;
    GameObject holding = null;

    public override bool CanTakeItem(string itemName)
    {
        return false;
    }

    public override void GainItem(GameObject item)
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
                holding = itemObject;
            }
            TransferItem();
        }
    }

    public override void TransferItem()
    {
        GridPosition transferPosition = new GridPosition(gridPosition.x + (int)transform.right.x, gridPosition.y + (int)transform.right.y);
        Machine toTransfer = box.GetMachine(transferPosition);
        if (toTransfer == null)
            return;
        if (!toTransfer.CanTakeItem(holding.GetComponent<Item>().itemName))
            return;

        Debug.Log("TRANSFER ITEM");
        toTransfer.GainItem(holding);
        holding = null;
        tick = 0;
    }
}
