using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    [Header("Miner")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int ticksPerResource;
    [SerializeField] private LayerMask tileLayer;

    GameObject holding = null;
    int tickCount = 0;


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

        tickCount++;
        if (tickCount >= ticksPerResource)
        {
            Collider2D tileCollider = Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.5f), 0f, tileLayer);
            if (tileCollider == null)
                return;
            string tileData = tileCollider.GetComponent<Tile>().GetTileData();
            if (tileData.Length <= 0)
                return;

            holding = Instantiate(itemPrefab, transform.position, Quaternion.identity, transform.parent);
            holding.GetComponent<Item>().SetItem(ResourceManager.instance.GetAvailableResource(tileData));
            holding.GetComponent<Item>().Show(true);
            tickCount = 0;
        }
    }

    public override void ReceiveItem(GameObject item)
    {
        Debug.LogError("Miner cannot receive item!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawCube(transform.position + new Vector3(1, 0, 0), new Vector3(0.5f, 0.5f, 0.5f));
        Gizmos.DrawCube(transform.position + new Vector3(-1, 0, 0), new Vector3(0.5f, 0.5f, 0.5f));
        Gizmos.DrawCube(transform.position + new Vector3(0, 1, 0), new Vector3(0.5f, 0.5f, 0.5f));
        Gizmos.DrawCube(transform.position + new Vector3(0, -1, 0), new Vector3(0.5f, 0.5f, 0.5f));

    }
}
