using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBalloon : Balloon
{
    public float WaitForFix = 0f;

    private float waiting = 0f;

    protected new void Update() {
        base.Update();

        CheckDirection();

        if(waiting <= 0f && stabs.Count > 0 && fixingHole == null) {
            waiting = WaitForFix;
        }

        if(waiting > 0f) {
            waiting -= Time.deltaTime;
            if(waiting <= 0f) {
                StartCoroutine(FixHole(stabs[0]));
            }
        }

        Vector3 mPos = PlayerBalloon.Instance.BalloonPart.position;
        Sword.LookAt(new Vector3(mPos.x, mPos.y, 0f));
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
