using System;
using UnityEngine;


public class Crutch : MonoBehaviour
{
    public static event Action onRestart;
    [SerializeField] private EventCondition onStart;

    private void Start()
    {
        onRestart?.Invoke();
        onStart.Perform();
    }
}


