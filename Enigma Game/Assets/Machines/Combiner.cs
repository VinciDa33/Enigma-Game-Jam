using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : Machine
{
    [SerializeField] private GameObject itemPrefab;
    GameObject secondHolding;

    List<string> canHold = new List<string>
    {
        "Iron Ingot",
        "Coal",
        "Tin Ingot",
        "Copper Ingot"
    };

    public override bool CanReceiveItem(string itemName)
    {
        if (holding == null && canHold.Contains(itemName))
            return true;
        if (holding.GetComponent<Item>().resource.name.Equals("Iron Ingot") && (itemName.Equals("Tin Ingot") || itemName.Equals("Coal")))
            return true;
        if (holding.GetComponent<Item>().resource.name.Equals("Tin Ingot") && (itemName.Equals("Iron Ingot") || itemName.Equals("Copper Ingot")))
            return true;
        if (holding.GetComponent<Item>().resource.name.Equals("Coal") && itemName.Equals("Iron Ingot"))
            return true;
        if (holding.GetComponent<Item>().resource.name.Equals("Copper") && itemName.Equals("Tin Ingot"))
            return true;
        return false;
    }

    public override void Process()
    {
        if (holding == null)
            return;

        string holdingName = holding.GetComponent<Item>().resource.name;
        if (holdingName.Contains("Bronze") || holdingName.Contains("Steel") || holdingName.Contains("Ferro"))
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


        string combine = holding.GetComponent<Item>().resource.name + secondHolding.GetComponent<Item>().resource.name;
        if (combine.Contains("Iron") && combine.Contains("Tin"))
            CreateItem("Ferro Tin");
        if (combine.Contains("Iron") && combine.Contains("Coal"))
            CreateItem("Steel Ingot");
        if (combine.Contains("Copper") && combine.Contains("Tin"))
            CreateItem("Bronze Ingot");
    }

    private void CreateItem(string itemName)
    {
        Destroy(holding);
        Destroy(secondHolding);

        holding = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        holding.GetComponent<Item>().SetItem(ResourceManager.instance.GetAvailableResource(itemName));
    }

    public override void ReceiveItem(GameObject item)
    {
        if (holding == null) {
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
