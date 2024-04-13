using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public static BoxManager instance { get; private set; }

    [SerializeField] Box currentBox;

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

    public void SetCurrentBox(Box box)
    {
        currentBox = box;
    }

    public Box GetCurrentBox()
    {
        return currentBox;
    }

    public void ExitBox()
    {
        if (currentBox.IsUnlocked())
            currentBox = currentBox.GetParentBox();
    }

    public void TryUnlockBox()
    {
        currentBox.Unlock();
    }

    private void Update()
    {
        Vector3 camPosition = new Vector3(currentBox.transform.position.x, currentBox.transform.position.y, -10f);
        Camera.main.transform.position = camPosition;
    }
}
