using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; private set; }

    [SerializeField] float timeStep;
    float timer;
    public UnityEvent onDoTimeStep { get; private set; } = new UnityEvent();
    bool gameRunning = true;

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

    private void Update()
    {
        if (!gameRunning)
            return;

        timer += Time.deltaTime;
        if (timer >= timeStep)
        {
            timer -= timeStep;
            onDoTimeStep.Invoke();
        }
    }

    public float GetTimeStep()
    {
        return timeStep;
    }
}
