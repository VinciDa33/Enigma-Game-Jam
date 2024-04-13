using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    bool mouseOver = false;
    [SerializeField] string tileData;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(mouseOver);
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    public string GetTileData()
    {
        return tileData;
    }

    private void LateUpdate()
    {
        if (mouseOver) {
            PlacementManager.instance.transform.position = transform.position;
            if (Input.GetMouseButtonDown(0))
                PlacementManager.instance.Place(transform.position, transform.parent.parent);
        }
    }
}
