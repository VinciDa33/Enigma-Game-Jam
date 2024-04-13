using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    [Header("Miner")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private LayerMask tileLayer;

    GameObject holding = null;

    public override bool CanReceiveItem(string itemName)
    {
        return false;
    }

    public override void Process()
    {
        if (holding != null)
        {
            GameObject[] neighbours = GetNeighbours();
            foreach(GameObject neighbour in neighbours)
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
            return;
        }


        Collider2D tileCollider = Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.5f), 0f, tileLayer);
        if (tileCollider == null)
            return;
        string tileData = tileCollider.GetComponent<Tile>().GetTileData();
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
