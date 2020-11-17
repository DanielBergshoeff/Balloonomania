using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoomEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public ZoomEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityZoomEvent Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Zoom value) {
        Response.Invoke(value);
    }
}

[System.Serializable]
public class UnityZoomEvent : UnityEvent<Zoom> { }
