using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Connection")]
    [SerializeField] Box parentBox;
    
    [Header("Unlock")]
    [SerializeField] bool unlocked;
    [SerializeField] Resource[] unlockCost;
    [SerializeField] Resource[] unlockReward;

    public void Unlock()
    {
        ResourceManager rm = ResourceManager.instance;

        //Check if cost can be paid
        foreach(Resource resource in unlockCost)
        {
            if (rm.GetResource(resource.name) < resource.amount)
                return;
        }

        //Consume cost
        foreach (Resource resource in unlockCost)
        {
            rm.ConsumeResource(resource.name, resource.amount);
        }

        //Add reward
        foreach(Resource resource in unlockReward)
        {
            rm.AddResource(resource.name, resource.amount);
        }

        unlocked = true;
    }

    public Box GetParentBox()
    {
        return parentBox;
    }

    public bool IsUnlocked()
    {
        return unlocked;
    }
}
