using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : Machine
{
    [SerializeField] private GameObject itemPrefab;

    public override bool CanReceiveItem(string itemName)
    {
        if (holding == null && itemName.Equals("Bronze Ingot"))
            return true;
        return false;
    }

    public override void Process()
    {
        if (holding == null)
            return;

        if (holding.GetComponent<Item>().resource.name.Contains("Ingot"))
        {
            Destroy(holding);

            holding = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            holding.GetComponent<Item>().SetItem(ResourceManager.instance.GetAvailableResource("Bronze Plate"));

            return;
        }

        GameObject neighbour = GetNeighbour(outputDirection);

        if (neighbour != null)
        {
            if (neighbour.GetComponent<Machine>().CanReceiveItem(holding.GetComponent<Item>().resource.name))
            {
                neighbour.GetComponent<Machine>().ReceiveItem(holding);
                holding = null;
            }
        }
    }

    public override void ReceiveItem(GameObject item)
    {
        holding = item;
        holding.GetComponent<Item>().Show(false);
        holding.transform.position = transform.position;
    }
}
