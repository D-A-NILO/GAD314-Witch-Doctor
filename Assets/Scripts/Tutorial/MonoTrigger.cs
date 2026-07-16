using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class MonoTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggered;
    private List<Action> listeners;

    public void Trigger()
    {
        foreach(Action action in listeners)
        {
            action?.Invoke();
        }
        OnTriggered?.Invoke();
    }

    public void AddListener(Action listener)
    {
        if(listeners.Contains(listener))
        {
            Debug.Log($"Listener is already Subscribed, ignoring");
            return;
        }

        listeners.Add(listener);
    }
    public void RemoveListener(Action listener)
    {
        if(listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
