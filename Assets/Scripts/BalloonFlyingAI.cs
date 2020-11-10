using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlyingAI : BalloonFlying
{
    public Transform BalloonTop;
    public FloatReference CollisionCheckDistance;
    public IntVariable CheckLanesPerSide;


    protected new void Update() {
        base.Update();

        CheckDirection();
    }

    private void CheckDirection() {
        int coll = CheckForCollision();
        if (coll == 1)
            ApplyHeat();
        else if (coll == -1)
            RemoveHeat();
        else {
            if (GameManager.GetDirection(transform.position, 1) < 0f) {
                ApplyHeat();
            }
            else {
                RemoveHeat();
            }
        }
    }

    private int CheckForCollision() {
        int dir = 0;

        //Check for bottom colliding
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, CollisionCheckDistance.Value);

        //Check for top colliding
        RaycastHit2D hittop = Physics2D.Raycast(BalloonTop.position, transform.right, CollisionCheckDistance.Value);

        if (hit.collider != null && hittop.collider == null)
            dir = 1;
        else if (hit.collider == null && hittop.collider != null)
            dir = -1;

        return dir;
    }
}
