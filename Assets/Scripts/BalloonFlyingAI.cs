using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlyingAI : BalloonFlying
{
    public Transform BalloonTop;
    public FloatReference CollisionCheckDistance;
    public IntVariable CheckLanesPerSide;
    public FloatReference MaxDistancePlayer;
    public Vector3Variable PlayerBalloonPosition;

    private bool playerTooFar;
    protected new void Update() {
        base.Update();

        CheckPlayerDistance();
        CheckDirection();
    }

    private void CheckDirection() {
        int coll = CheckForCollision();
        if (coll == 1)
            ApplyHeat();
        else if (coll == -1) {
            StopHeat();
            RemoveHeat();
        }
        else {
            if (GameManager.GetDirection(transform.position, CheckLanesPerSide.Value, playerTooFar) < 0f) {
                ApplyHeat();
            }
            else {
                StopHeat();
                RemoveHeat();
            }
        }
    }

    private void CheckPlayerDistance() {
        playerTooFar = transform.position.x > PlayerBalloonPosition.Value.x + MaxDistancePlayer.Value;
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
