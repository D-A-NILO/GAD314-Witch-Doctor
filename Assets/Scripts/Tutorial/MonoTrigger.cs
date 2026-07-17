using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoTrigger : MonoBehaviour
{
    public UnityEvent OnTriggered;
    public List<Action> listeners;

    public void Trigger()
    {
        if(listeners == null)
        {
            Debug.Log("listeners uninitialised");
            return;
        }

        for(int i = 0; i < listeners.Count; i++)
        {
            listeners[i]?.Invoke();
        }
        Debug.Log(name +" triggered");
        OnTriggered?.Invoke();
    }

    public void AddListener(Action listener)
    {
        if(listeners == null) listeners = new();

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
