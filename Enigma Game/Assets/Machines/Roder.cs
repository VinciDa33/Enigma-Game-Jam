using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roder : Machine
{
    [SerializeField] private GameObject itemPrefab;
    GameObject secondHolding;

    List<string> canHold = new List<string>
    {
        "Bronze Plate",
        "Ferro Tin"
    };

    public override bool CanReceiveItem(string itemName)
    {
        if (holding == null && canHold.Contains(itemName))
            return true;
        if (holding.GetComponent<Item>().resource.name.Equals("Bronze Plate") && itemName.Equals("Ferro Tin"))
            return true;
        if (holding.GetComponent<Item>().resource.name.Equals("Ferro Tin") && itemName.Equals("Bronze Plate"))
            return true;
        return false;
    }

    public override void Process()
    {
        if (holding == null)
            return;

        if (holding.GetComponent<Item>().resource.name.Contains("Spun"))
        {
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

        if (secondHolding == null)
            return;

        Destroy(holding);
        Destroy(secondHolding);

        holding = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        holding.GetComponent<Item>().SetItem(ResourceManager.instance.GetAvailableResource("Bronze Spun Ferro Tin Rod"));
    }

    public override void ReceiveItem(GameObject item)
    {
        if (holding == null)
        {
            holding = item;
            holding.GetComponent<Item>().Show(false);
            holding.transform.position = transform.position;
            return;
        }
        if (secondHolding == null)
        {
            secondHolding = item;
            secondHolding.GetComponent<Item>().Show(false);
            secondHolding.transform.position = transform.position;
        }
    }
}
