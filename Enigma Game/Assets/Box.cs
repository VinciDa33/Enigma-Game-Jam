using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    [Header("Connection")]
    [SerializeField] Box parentBox;
    
    [Header("Unlock")]
    [SerializeField] bool unlocked;
    [SerializeField] Resource[] unlockCost;
    [SerializeField] Resource[] unlockReward;

    [Header("UI")]
    [SerializeField] GameObject unlockButton;
    [SerializeField] GameObject goUpButton;
    [SerializeField] GameObject boxCostUI;
    [SerializeField] GameObject resourceDisplay;
    [SerializeField] float verticalOffset;

    private void Start()
    {
        for (int i = 0; i < unlockCost.Length; i++)
        {
            GameObject temp = Instantiate(resourceDisplay, boxCostUI.transform.position + new Vector3(0, -verticalOffset * i - 80), Quaternion.identity, boxCostUI.transform);
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -verticalOffset * i - 80);

            temp.transform.GetChild(0).GetComponent<TMP_Text>().text = unlockCost[i].name;
            temp.transform.GetChild(1).GetComponent<TMP_Text>().text = "" + unlockCost[i].amount;
        }
    }

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

        unlockButton.SetActive(false);
        goUpButton.SetActive(true);

        Destroy(boxCostUI);
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
