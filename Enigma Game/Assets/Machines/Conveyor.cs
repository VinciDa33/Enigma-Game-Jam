using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Machine
{
    [Header("Conveyor")]
    [SerializeField] int ticksPerMovement;
    [SerializeField] float visualMoveSpeed;

    int tickCount = 0;
    GameObject holding;


    public override bool CanReceiveItem(string itemName)
    {
        if (holding == null)
            return true;
        return false;
    }

    public override void Process()
    {
        if (holding == null)
        {
            tickCount = 0;
            return;
        }

        tickCount++;
        if (tickCount >= ticksPerMovement)
        {
            GameObject neighbour = GetNeighbour(transform.right);

            if (neighbour == null)
                return;

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
        holding.GetComponent<Item>().Show(true);
    }

    private void Update()
    {
        if (holding == null)
            return;
        holding.transform.position = Vector2.MoveTowards(holding.transform.position, transform.position + (transform.right / 2), visualMoveSpeed * Time.deltaTime);
    }
}
