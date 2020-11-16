using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlyingPlayer : BalloonFlying
{
    public GameEvent PlayerFlyingEvent;
    private bool firstFly = false;

    protected new void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.W)) {
            ApplyHeat();
        }
        else {
            StopHeat();
        }
        if (Input.GetKey(KeyCode.S)) {
            RemoveHeat();
        }

        if (!firstFly && !grounded && Input.GetKey(KeyCode.W)) {
            PlayerFlyingEvent.Raise();
            firstFly = true;
        }
    }
}
