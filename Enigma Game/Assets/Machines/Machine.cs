using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Machine : MonoBehaviour, IPointerEnterHandler
{
    public string machineName;
    public bool canRotate;
    public bool deselectOnPlacement;
    public bool sellable;
    public Vector2 outputDirection = new Vector2(1, 0);
    protected GameObject holding;

    [SerializeField] LayerMask machineLayer;
    [SerializeField] Resource[] machineCost;

    private void Start()
    {
        TimeManager.instance.onDoTimeStep.AddListener(Process);
    }

    private void OnDestroy()
    {
        if (holding != null)
            Destroy(holding);
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
        MouseOver();

        if (!sellable)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Resource resource in machineCost)
            {
                ResourceManager.instance.AddResource(resource.name, resource.amount);
            }
            Destroy(gameObject);
        }
    }

    public virtual void MouseOver() 
    {
    }

    public Resource[] GetResourceCost()
    {
        return machineCost;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ENTERED: " + machineName);
    }
}
