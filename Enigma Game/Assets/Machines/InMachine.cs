using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMachine : Machine
{
    GameObject holding;
    bool justReceivedItem = false;

    public override bool CanReceiveItem(string itemName)
    {
        if (!itemName.Equals("InItem"))
            return false;

        if (holding == null)
            return true;
        return false;
    }

    public override void Process()
    {
        if (!justReceivedItem && holding != null)
        {
            GameObject neighbour = GetNeighbour(transform.right);

            if (neighbour != null)
            {
                if (neighbour.GetComponent<Machine>().CanReceiveItem(holding.GetComponent<Item>().resource.name))
                {
                    neighbour.GetComponent<Machine>().ReceiveItem(holding);
                    holding = null;
                }
            }
        }

        justReceivedItem = false;
    }

    public override void ReceiveItem(GameObject item)
    {
        holding = item;
        holding.GetComponent<Item>().Show(true);
        holding.transform.position = transform.position;
        justReceivedItem = true;
    }
}
