using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventBalloonStabListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEventBalloonStab Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public BalloonStabEvent Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(BalloonStab value) {
        Response.Invoke(value);
    }
}
