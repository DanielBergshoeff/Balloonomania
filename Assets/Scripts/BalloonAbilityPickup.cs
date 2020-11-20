using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonAbilityPickup : MonoBehaviour
{
    public GameEventVector3 ScoreEvent;
    public GameObjectVariable HookPrefab;
    public FloatReference HookMovePercentage;
    public FloatReference HookMoveDuration;
    public GameEvent PickupEvent;

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

        float z_plane = 3.5f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mPos = ray.origin + ray.direction * (z_plane - ray.origin.z) / ray.direction.z;

        Ray ray2 = Camera.main.ScreenPointToRay(startPos);
        Vector3 startPosWorld = ray2.origin + ray2.direction * (z_plane - ray2.origin.z) / ray2.direction.z;

        Vector3 dir = mPos - startPosWorld;
        dir = new Vector3(dir.x, dir.y, hook.transform.position.z);
        if (dir != Vector3.zero) {
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
            hook.transform.rotation = rot;
        }

        if (Input.GetMouseButtonUp(0)) {
            dir = new Vector3(dir.x, dir.y, 0f);
            ThrowEnd(dir);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Ability"))
            return;

        AbilityPickup ap = collision.gameObject.GetComponent<AbilityPickup>();
        if (ap.MyAbility.name == "Hook" && hook == null) {
            OnHookPickup();
        }
        else if(ap.MyAbility.name == "Hook") {
            ScoreEvent.Raise(new Vector3(100f, 0f, 0f));
        }

        currentAbility = ap.MyAbility;

        Destroy(collision.gameObject);

        AudioManager.PlayRandomSound(Sound.Pickup);
        AudioManager.PlayRandomSound(Sound.ItemGetShout);
    }

    private void OnHookPickup() {
        hook = Instantiate(HookPrefab.Value);
        hook.transform.parent = transform;
        hook.transform.localPosition = Vector3.zero;

        Hook h = hook.GetComponent<Hook>();
        h.HookHitEvent = new BalloonEvent();
        h.HookHitEvent.AddListener(ThrowHit);
        h.ThrowingPlayer = GetComponent<BalloonInfo>();
        postthrow = false;

        PickupEvent.Raise();
    }

    private void ThrowStart() {
        throwing = true;
        startPos = Input.mousePosition;
    }

    private void ThrowEnd(Vector3 dir) {
        throwing = false;
        hook.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        float throwStrength = dir.magnitude;
        hook.GetComponent<Rigidbody2D>().AddForce(hook.transform.forward * 50f * throwStrength);
        hook.transform.parent = null;
        postthrow = true;
        hook = null;

        AudioManager.PlaySound(Sound.ThrowHook);
    }

    private void ThrowHit(BalloonInfo bi) {
        Debug.Log("Throw hit");
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

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        bi.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        if(bi.GetComponent<BalloonAIStabbing>() != null) {
            bi.GetComponent<BalloonAIStabbing>().enabled = false;
        }

        AudioManager.PlaySound(Sound.PullHook);
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
            return;
        }

        transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        hookedBalloon.transform.position = Vector3.Lerp(otherStartPosition, otherTargetPosition, t);
    }
}
