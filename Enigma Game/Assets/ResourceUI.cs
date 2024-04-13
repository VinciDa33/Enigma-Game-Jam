using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    ResourceManager rm;
    Dictionary<string, GameObject> resourceDisplays = new Dictionary<string, GameObject>();
    [SerializeField] GameObject resourceDisplay;
    [SerializeField] float verticalOffset;

    void Start()
    {
        rm = ResourceManager.instance;
    }

    void Update()
    {
        List<string> names = rm.GetResourceNames();
        for (int i = 0; i < names.Count; i++)
        {
            if (!resourceDisplays.ContainsKey(names[i]))
            {
                resourceDisplays.Add(names[i], Instantiate(resourceDisplay, transform.position + new Vector3(0, -verticalOffset * i - 50), Quaternion.identity, transform));
                resourceDisplays[names[i]].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -verticalOffset * i - 50);
            }

            resourceDisplays[names[i]].transform.GetChild(0).GetComponent<TMP_Text>().text = names[i];
            resourceDisplays[names[i]].transform.GetChild(1).GetComponent<TMP_Text>().text = "" + rm.GetResource(names[i]);
        }
    }
}