using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameEventBalloonStab : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventBalloonStabListener> eventListeners =
        new List<GameEventBalloonStabListener>();

    public void Raise(BalloonStab value) {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(value);
    }

    public void RegisterListener(GameEventBalloonStabListener listener) {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventBalloonStabListener listener) {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}

[System.Serializable]
public class BalloonStabEvent : UnityEvent<BalloonStab> { }
