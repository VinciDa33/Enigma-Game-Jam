using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    GridPosition gridPosition;

    bool mouseOver = false;

    public void setBoxPosition(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(mouseOver);
        
        if (mouseOver)
        {

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
