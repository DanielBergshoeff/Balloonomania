using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBalloon : Balloon
{
    [Header("AI")]
    public Transform BalloonTop;
    public float WaitForFix = 0f;
    public float StabDistance = 5f;
    public float CollisionCheckDistance = 1f;
    public LayerMask CollisionMask;

    private float waiting = 0f;

    protected new void Update() {
        base.Update();

        CheckDirection();

        if(waiting <= 0f && stabs.Count > 0) {
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

        if(stabCooldown <= 0f)
            TryStab();
    } 

    private void TryStab() {
        if((transform.position - PlayerBalloon.Instance.BalloonPart.position).sqrMagnitude < StabDistance * StabDistance) {
            Stab();
        }
    }

    private void CheckDirection() {
        int coll = CheckForCollision();
        if (coll == 1)
            ApplyHeat();
        else if (coll == -1)
            RemoveHeat();
        else {
            if (GameManager.GetDirection(transform.position, 1, false) < 0f) {
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, CollisionCheckDistance);

        //Check for top colliding
        RaycastHit2D hittop = Physics2D.Raycast(BalloonTop.position, transform.right, CollisionCheckDistance);

        if (hit.collider != null && hittop.collider == null)
            dir = 1;
        else if (hit.collider == null && hittop.collider != null)
            dir = -1;

        return dir;
    }
}
