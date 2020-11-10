using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventBalloonInfo : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventBalloonInfoListener> eventListeners =
        new List<GameEventBalloonInfoListener>();

    public void Raise(BalloonInfo bi) {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(bi);
    }

    public void RegisterListener(GameEventBalloonInfoListener listener) {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventBalloonInfoListener listener) {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
