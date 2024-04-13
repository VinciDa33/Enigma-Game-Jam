using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    Box box;
    GridPosition gridPosition;

    bool mouseOver = false;

    public void setupFloorPosition(Box box, GridPosition gridPosition)
    {
        this.box = box;
        this.gridPosition = gridPosition;
    }

    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(mouseOver);

        if (mouseOver)
        {
            PlacementManager.instance.transform.position = transform.position;
            if (Input.GetMouseButtonDown(0) && box.GetMachine(gridPosition) == null) {
                PlacementManager.instance.Place(box, gridPosition, transform.position);
            }
        }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
