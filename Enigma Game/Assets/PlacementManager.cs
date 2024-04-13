using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager instance { get; private set; }

    [SerializeField] GameObject[] machines;
    [SerializeField] LayerMask machineLayer;

    GameObject selectedMachine = null;
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
    public void SelectMachine(int index)
    {
        selectedMachine = machines[index];
        transform.rotation = Quaternion.identity;
        GetComponent<SpriteRenderer>().sprite = selectedMachine.GetComponent<Machine>().GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DeselectMachine()
    {
        selectedMachine = null;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);

        if (Input.GetKeyDown(KeyCode.Escape))
            DeselectMachine();

        if (selectedMachine == null)
            return;

        if (selectedMachine.GetComponent<Machine>().canRotate && Input.GetKeyDown(KeyCode.R))
            transform.Rotate(0f, 0f, -90f);
    }

    public void Place(Vector3 position, Transform parent)
    {
        if (selectedMachine == null)
            return;
        if (Physics2D.OverlapBox(position, new Vector2(0.5f, 0.5f), 0f, machineLayer) != null)
            return;

        Instantiate(selectedMachine, position, transform.rotation, parent);

        if (selectedMachine.GetComponent<Machine>().deselectOnPlacement)
            DeselectMachine();
    }
}
