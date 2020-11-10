using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlyingPlayer : BalloonFlying
{
    protected new void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
            ApplyHeat();
        }
        if (Input.GetKey(KeyCode.S)) {
            RemoveHeat();
        }
    }
}
