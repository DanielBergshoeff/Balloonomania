using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class ZoomEvent : ScriptableObject {
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<ZoomEventListener> eventListeners =
        new List<ZoomEventListener>();

    public void Raise(Zoom value) {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(value);
    }

    public void RegisterListener(ZoomEventListener listener) {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(ZoomEventListener listener) {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}

[System.Serializable]
public class Zoom
{
    public Transform Target;
    public float ZoomDistance;
    public float ZoomTime;

    public Zoom(Transform target, float zDistance, float zTime) {
        Target = target;
        ZoomDistance = zDistance;
        ZoomTime = zTime;
    }

    public Zoom() {}
}


