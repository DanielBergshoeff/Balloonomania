using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBalloon : Balloon
{
    protected new void Update() {
        base.Update();

        CheckDirection();
    }

    private void CheckDirection() {
        if(GameManager.GetDirection(transform.position, 1) < 0f) {
            ApplyHeat();
        }
        else {
            RemoveHeat();
        }
    }
}
