using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public static BoxManager instance { get; private set; }

    Box currentBox;

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

    public void ExitBox()
    {
        if (currentBox.IsUnlocked())
            currentBox = currentBox.GetParentBox();
    }

    public void TryUnlockBox()
    {
        currentBox.Unlock();
    }
}
