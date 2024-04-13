using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutMachine : Machine
{
    [SerializeField] private Box box;
    [SerializeField] private GameObject spatialBox;

    public override bool CanReceiveItem(string itemName)
    {
        if (holding == null)
            return true;
        return false;
    }

    public override void Process()
    {
        if (holding == null)
            return;
        if (!box.IsUnlocked())
        {
            ResourceManager.instance.AddResource(holding.GetComponent<Item>().resource.name, 1);
            Destroy(holding);
            holding = null;
            return;
        }

        GameObject neighbour = spatialBox.GetComponent<Machine>().GetNeighbour(transform.right);

        if (neighbour != null)
        {
            if (neighbour.GetComponent<Machine>().CanReceiveItem(holding.GetComponent<Item>().resource.name))
            {
                neighbour.GetComponent<Machine>().ReceiveItem(holding);
                holding.transform.position = spatialBox.transform.position;
                holding = null;
            }
        }
    }

    public override void ReceiveItem(GameObject item)
    {
        holding = item;
        holding.GetComponent<Item>().Show(true);
        holding.transform.position = transform.position;
    }
}
