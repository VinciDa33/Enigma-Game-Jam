using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance { get; private set; }
    [SerializeField] ResourceItem[] gameItems;

    Dictionary<string, int> resources = new Dictionary<string, int>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int GetResource(string resourceName)
    {
        return resources[resourceName];
    }

    public void AddResource(string resourceName, int resourceAmount)
    {
        if (!resources.ContainsKey(resourceName)) {
            resources.Add(resourceName, resourceAmount);
            return;
        }

        resources[resourceName] += resourceAmount;

    }

    public void ConsumeResource(string resourceName, int resourceAmount)
    {
        resources[resourceName] -= resourceAmount;
    }

    public List<string> GetResourceNames()
    {
        return new List<string>(resources.Keys);
    }

    public Dictionary<string, int> GetResources()
    {
        return resources;
    }

    public ResourceItem GetGameItem(string name)
    {
        foreach(ResourceItem ri in gameItems)
        {
            if (ri.name.Equals(name))
                return ri;
        }
        return new ResourceItem("error", null);
    }

}
