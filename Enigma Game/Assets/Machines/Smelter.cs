using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : Machine
{
    [SerializeField] private GameObject itemPrefab;

    public override bool CanReceiveItem(string itemName)
    {
        if (holding == null && itemName.Contains("Ore"))
            return true;
        return false;
    }

    public override void Process()
    {
        if (holding == null)
            return;

        if (holding.GetComponent<Item>().resource.name.Contains("Ore"))
        {
            string type = holding.GetComponent<Item>().resource.name.Split(" ")[0];
            type += " Ingot";

            Destroy(holding);

            holding = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            holding.GetComponent<Item>().SetItem(ResourceManager.instance.GetAvailableResource(type));

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
