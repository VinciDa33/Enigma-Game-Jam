using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : Machine
{
    [SerializeField] private GameObject itemPrefab;

    public override bool CanReceiveItem(string itemName)
    {
        if (itemName.Contains("Ore"))
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

        GameObject[] neighbours = GetNeighbours();
        foreach (GameObject neighbour in neighbours)
        {
            if (neighbour == null)
                continue;

            if (!neighbour.GetComponent<Machine>().machineName.Equals("Conveyor"))
                continue;

            if (neighbour.GetComponent<Machine>().CanReceiveItem(holding.GetComponent<Item>().resource.name))
            {
                neighbour.GetComponent<Machine>().ReceiveItem(holding);
                holding = null;
                break;
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
