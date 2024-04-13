using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    public Sprite sprite;
    protected Box box;
    protected GridPosition gridPosition;
    public bool canRotate;

    private void Start()
    {
        TimeManager.instance.onDoTimeStep.AddListener(process);
    }

    private void OnDestroy()
    {
        TimeManager.instance.onDoTimeStep.RemoveListener(process);
    }

    public abstract bool IsInventoryFull();
    public abstract bool CanTakeItem(string itemName);
    public abstract void TransferItem(Item item);

    public abstract void GainItem(Item item);

    public void SetBox(Box box)
    {
        this.box = box;
    }

    public abstract void process();

    public void SetupMachine(Box box, GridPosition gridPosition)
    {
        this.box = box;
        this.gridPosition = gridPosition;
    }
}
