using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialBox : Machine
{
    [SerializeField] GameObject inMachine;
    [SerializeField] Box box;

    public override bool CanReceiveItem(string itemName)
    {
        if (holding == null)
            return true;
        return false;
    }

    public override void Process()
    {
        if (holding == null)
            return;

        if (inMachine.GetComponent<Machine>().CanReceiveItem("InItem"))
        {
            inMachine.GetComponent<Machine>().ReceiveItem(holding);
            holding = null;
        }


    }

    public override void ReceiveItem(GameObject item)
    {
        holding = item;
        holding.GetComponent<Item>().Show(true);
    }

    public override void MouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BoxManager.instance.SetCurrentBox(box);
        }
    }
}
