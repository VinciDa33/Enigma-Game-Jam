using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Connection")]
    [SerializeField] GameObject parentBox;
    
    [SerializeField] new Vector2 inBoxPosition;
    [SerializeField] new Vector2 outBoxPosition;

    
    [Header("Unlock")]
    [SerializeField] bool isUnlocked;
    [SerializeField] ResourceChunk[] unlockCost;
    [SerializeField] ResourceChunk[] unlockReward;

    [Header("Box Size")]
    readonly Vector2 areaSize = new Vector2(16f, 9f);
    readonly Vector2 areaStart = new Vector2(-7.5f, -4f);
    [SerializeField] int sizeX;
    [SerializeField] int sizeY;

    [Header("Graphics")]
    [SerializeField] GameObject floorTile;
    [SerializeField] GameObject wallTile;

    Machine[,] machines;


    private void Start()
    {
        machines = new Machine[sizeX, sizeY];

        for (int x = 0; x < (int) areaSize.x; x++)
        {
            for (int y = 0; y < (int) areaSize.y; y++)
            {
                if (x >= (int) areaSize.x - 1 - sizeX && x < (int)areaSize.x - 1 && y >= (int)areaSize.y - 1 - sizeY && y < (int)areaSize.y - 1)
                    Instantiate(floorTile, new Vector3(x + areaStart.x, y + areaStart.y, 0f), Quaternion.identity, transform);
                else
                    Instantiate(wallTile, new Vector3(x + areaStart.x, y + areaStart.y, 0f), Quaternion.identity, transform);
            }
        }
    }

    public void Unlock()
    {
        ResourceManager rm = ResourceManager.instance;

        //Check if cost can be paid
        foreach(ResourceChunk rc in unlockCost)
        {
            if (rm.GetResource(rc.name) < rc.amount)
                return;
        }

        //Consume cost
        foreach (ResourceChunk rc in unlockCost)
        {
            rm.ConsumeResource(rc.name, rc.amount);
        }

        //Add reward
        foreach(ResourceChunk rc in unlockReward)
        {
            rm.AddResource(rc.name, rc.amount);
        }

        isUnlocked = true;
    }

    public Machine GetMachine(GridPosition gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < sizeX && gridPosition.y >= 0 && gridPosition.y < sizeY)
            return machines[gridPosition.x, gridPosition.y];
        return null;
    }
}
