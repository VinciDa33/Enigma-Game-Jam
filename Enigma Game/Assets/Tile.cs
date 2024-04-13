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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseOver = mousePos.x > transform.position.x - 0.45f && mousePos.x < transform.position.x + 0.45f && mousePos.y > transform.position.y - 0.45f && mousePos.y < transform.position.y + 0.45f;


        transform.GetChild(0).gameObject.SetActive(mouseOver);
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
