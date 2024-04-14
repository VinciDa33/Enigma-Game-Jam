using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    [Header("Miner")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private LayerMask tileLayer;
    private int tick = 0;
    public override bool CanReceiveItem(string itemName)
    {
        return false;
    }

    public override void Process()
    {
        if (holding != null)
        {
            GameObject neighbour = GetNeighbour(outputDirection);

            if (neighbour != null)
            {
                if (neighbour.GetComponent<Machine>().CanReceiveItem(holding.GetComponent<Item>().resource.name))
                {
                    neighbour.GetComponent<Machine>().ReceiveItem(holding);
                    holding = null;
                    tick = 0;
                }
            }
            return;
        }

        tick++;
        if (tick < 3)
            return;

        GameObject tile = null;
        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (Vector2.Distance(transform.position, gameObject.transform.position) < 0.1f)
            {
                tile = gameObject;
                break;
            }
        }

        if (tile == null)
            return;

        string tileData = tile.GetComponent<Tile>().GetTileData();
        if (tileData.Length <= 0)
            return;

        holding = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        holding.GetComponent<Item>().SetItem(ResourceManager.instance.GetAvailableResource(tileData));
        holding.GetComponent<Item>().Show(true);
    }

    public override void ReceiveItem(GameObject item)
    {
        Debug.LogError("Miner cannot receive item!");
    }
}
