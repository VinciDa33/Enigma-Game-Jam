using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Machine
{
    [Header("Conveyor")]
    [SerializeField] float visualMoveSpeed;

    bool justReceivedItem = false;

    public override bool CanReceiveItem(string itemName)
    {
        if (holding == null)
            return true;
        return false;
    }

    public override void Process()
    {
        if (!justReceivedItem && holding != null)
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

        justReceivedItem = false;
    }

    public override void ReceiveItem(GameObject item)
    {
        holding = item;
        holding.GetComponent<Item>().Show(true);
        justReceivedItem = true;
    }

    private void Update()
    {
        if (holding == null)
            return;
        holding.transform.position = Vector2.MoveTowards(holding.transform.position, transform.position + (transform.right / 2), visualMoveSpeed * Time.deltaTime);
    }
}
