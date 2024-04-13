using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    public string machineName;
    public bool canRotate;
    public bool deselectOnPlacement;
    public bool sellable;

    [SerializeField] LayerMask machineLayer;
    [SerializeField] Resource[] machineCost;

    private void Start()
    {
        TimeManager.instance.onDoTimeStep.AddListener(Process);
    }

    private void OnDestroy()
    {
        TimeManager.instance.onDoTimeStep.RemoveListener(Process);
    }

    public abstract bool CanReceiveItem(string itemName);
    public abstract void ReceiveItem(GameObject item);
    public abstract void Process();

    public GameObject GetNeighbour(Vector2 direction)
    {
        Collider2D result = Physics2D.OverlapBox(transform.position + new Vector3(direction.x, direction.y, 0f), new Vector2(0.5f, 0.5f), 0f, machineLayer);
        if (result != null)
            return result.gameObject;
        return null;
    }

    public GameObject[] GetNeighbours()
    {
        GameObject[] neighbours = new GameObject[4];
        
        neighbours[0] = GetNeighbour(new Vector2(1, 0));
        neighbours[1] = GetNeighbour(new Vector2(0, -1));
        neighbours[2] = GetNeighbour(new Vector2(0, 1));
        neighbours[3] = GetNeighbour(new Vector2(-1, 0));

        return neighbours;
    }

    private void OnMouseOver()
    {
        if (!sellable)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Resource resource in machineCost)
            {
                ResourceManager.instance.AddResource(resource.name, resource.amount);
                Destroy(gameObject);
            }
        }
    }
}
