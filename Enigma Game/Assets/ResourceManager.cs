using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance { get; private set; }
    [SerializeField] Resource[] availableResources;

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

    private void Start()
    {
        AddResource("Iron Ingot", 10);
        AddResource("Tin Ingot", 5);

        AddResource("Iron Ingot", 1000);
        AddResource("Tin Ingot", 1000);
        AddResource("Copper Ingot", 1000);
        AddResource("Steel Ingot", 1000);
        AddResource("Bronze Ingot", 1000);
        AddResource("Bronze Plate", 1000);
        AddResource("Ferro Tin", 1000);
        AddResource("Bronze Spun Ferro Tin Rod", 1000);
    }

    public int GetResource(string resourceName)
    {
        if (resources.ContainsKey(resourceName))
            return resources[resourceName];
        return 0;
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

    public Resource GetAvailableResource(string name)
    {
        foreach(Resource resource in availableResources)
        {
            if (resource.name.Equals(name))
                return resource;
        }
        return new Resource("error", null);
    }

}
