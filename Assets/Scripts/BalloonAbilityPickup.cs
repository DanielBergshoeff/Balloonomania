using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonAbilityPickup : MonoBehaviour
{
    public GameObjectVariable HookPrefab;
    public FloatReference HookMovePercentage;
    public FloatReference HookMoveDuration;

    private Ability currentAbility;
    private GameObject hook;
    private bool throwing = false;
    private bool postthrow = false;
    private BalloonInfo hookedBalloon;

    private Vector3 targetPosition;
    private Vector3 otherTargetPosition;

    private Vector3 startPosition;
    private Vector3 otherStartPosition;

    private float startTime;

    private Vector3 startPos;
    private Vector3 previousPos;
    private Vector3 totalPos;
    private int amtPos;

    void Update()
    {
        if (hook != null && !postthrow) {
            HookInteraction();
        }
        
        if(hookedBalloon != null) {
            PullBalloon();
        }
    }

    private void HookInteraction() {
        if (!throwing && Input.GetMouseButtonDown(0))
            ThrowStart();

        if (!throwing) return;

        Vector3 startPosWorld = Camera.main.ScreenToWorldPoint(startPos);
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*hook.transform.LookAt(new Vector3(mPos.x, mPos.y, 0f));*/

        Vector3 dir = mPos - startPosWorld;
        if (dir != Vector3.zero) {
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
            hook.transform.rotation = rot;
        }

        totalPos += previousPos - Input.mousePosition;
        amtPos++;

        previousPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0)) {
            ThrowEnd();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Ability"))
            return;

        AbilityPickup ap = collision.gameObject.GetComponent<AbilityPickup>();
        if (ap.MyAbility.name == "Hook") {
            OnHookPickup();
        }

        currentAbility = ap.MyAbility;

        Destroy(collision.gameObject);
    }

    private void OnHookPickup() {
        hook = Instantiate(HookPrefab.Value);
        hook.transform.parent = transform;
        hook.transform.localPosition = Vector3.zero;

        Hook h = hook.GetComponent<Hook>();
        h.HookHitEvent = new BalloonEvent();
        h.HookHitEvent.AddListener(ThrowHit);
        postthrow = false;
    }

    private void ThrowStart() {
        throwing = true;
        startPos = Input.mousePosition;
        previousPos = startPos;
        amtPos = 0;
        totalPos = Vector3.zero;
    }

    private void ThrowEnd() {
        throwing = false;
        hook.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        float throwStrength = (totalPos / amtPos).magnitude;
        hook.GetComponent<Rigidbody2D>().AddForce(hook.transform.forward * 10f * throwStrength);
        hook.transform.parent = null;
        postthrow = true;
    }

    private void ThrowHit(BalloonInfo bi) {
        hookedBalloon = bi;
        Vector3 dir = bi.transform.position - transform.position;
        float dist = dir.magnitude;
        Vector3 heading = dir / dist;
        float newDist = dist * HookMovePercentage.Value;

        targetPosition = transform.position + heading * newDist;
        otherTargetPosition = bi.transform.position - heading * newDist;

        startPosition = transform.position;
        otherStartPosition = bi.transform.position;

        startTime = Time.time;

        Destroy(hook);

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        bi.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        if(bi.GetComponent<BalloonAIStabbing>() != null) {
            bi.GetComponent<BalloonAIStabbing>().enabled = false;
        }
    }

    private void PullBalloon() {
        float t = (Time.time - startTime) / HookMoveDuration.Value;

        if (t >= 1f) {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            hookedBalloon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            if (hookedBalloon.GetComponent<BalloonAIStabbing>() != null) {
                hookedBalloon.GetComponent<BalloonAIStabbing>().enabled = true;
            }

            hookedBalloon = null;
            hook = null;
            return;
        }

        transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        hookedBalloon.transform.position = Vector3.Lerp(otherStartPosition, otherTargetPosition, t);
    }
}
