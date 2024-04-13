using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager instance { get; private set; }

    [SerializeField] GameObject[] machines;

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
    public void SelectMachine(int i)
    {
        selectedMachine = machines[i];
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            selectedMachine = null;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (selectedMachine == null)
        {
            sr.enabled = false;
            return;
        }

        if (selectedMachine.GetComponent<Machine>().canRotate && Input.GetKeyDown(KeyCode.R))
            transform.Rotate(0f, 0f, -90f);

        sr.enabled = true;
        sr.sprite = selectedMachine.GetComponent<Machine>().sprite;   
    }

    public void Place(Box box, GridPosition gridPosition, Vector3 position)
    {
        if (selectedMachine == null)
            return;

        GameObject temp = Instantiate(selectedMachine, position, transform.rotation, box.transform);
        box.AddMachine(gridPosition, selectedMachine.GetComponent<Machine>());
        temp.GetComponent<Machine>().SetupMachine(box, gridPosition);

        selectedMachine = null;
    }
}
