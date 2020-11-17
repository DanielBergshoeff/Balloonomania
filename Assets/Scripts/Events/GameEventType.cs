using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GameEventType<T> : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventTypeListener<T>> eventListeners =
        new List<GameEventTypeListener<T>>();

    public void Raise(T value) {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(value);
    }

    public void RegisterListener(GameEventTypeListener<T> listener) {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventTypeListener<T> listener) {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
