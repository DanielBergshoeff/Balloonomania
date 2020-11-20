using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventTypeListener<T> : MonoBehaviour
{
    [System.Serializable]
    public class TypeEvent : UnityEvent<T> { }

    public abstract GameEventType<T> Event { get; }

    public TypeEvent Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(T value) {
        Response.Invoke(value);
    }
}
