using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [Header("References")]
    public Transform BalloonPart;
    public Transform FirePart;
    public Transform Sword;
    public Transform SwordPoint;

    [Header("Speed")]
    public float UpwardVelocity = 1f;
    public float MaxUpwardVelocity = 2f;
    public float HorizontalSpeed = 1f;

    [Header("Heat information")]
    public float MaxHeat = 3f;
    public float AddHeatPerSecond = 2f;
    public float RemoveHeatPerSecond = 2f;
    public float StandardHeatLoss = 1f;
    public float HoleHeatLoss = 3f;
    public float TimeForHoleFix = 1f;
    protected float Heat = 0f;

    [Header("Abilities")]
    public float StabCooldown = 1f;

    protected Rigidbody2D myRigidbody;
    protected bool grounded = false;
    protected List<GameObject> stabs;
    protected float stabbedCooldown = 0f;
    protected GameObject fixingHole;
    protected bool stabbing = false;


    protected float stabCooldown = 0f;

    // Start is called before the first frame update
    protected void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        stabs = new List<GameObject>();
    }

    protected void Update() {
        if (stabbedCooldown > 0f)
            stabbedCooldown -= Time.deltaTime;

        if (Heat > 0f) {
            Heat -= StandardHeatLoss * Time.deltaTime;
            foreach(GameObject go in stabs) {
                Heat -= HoleHeatLoss * Time.deltaTime;
            }

            if(fixingHole != null)
                Heat -= HoleHeatLoss * Time.deltaTime;
        }

        FirePart.transform.localScale = (Heat / MaxHeat) * 1.5f * Vector3.one;

        myRigidbody.AddForce(transform.up * Heat * UpwardVelocity);

        grounded = Physics2D.Raycast(transform.position - transform.up * 0.01f, -transform.up, 0.15f).collider != null;
        grounded = true;

        if (!grounded)
            transform.position = transform.position + transform.right * Time.deltaTime * HorizontalSpeed * GameManager.GetSpeed(BalloonPart.position);

        if (myRigidbody.velocity.magnitude > MaxUpwardVelocity) {
            Vector2 vel = myRigidbody.velocity.normalized;
            myRigidbody.velocity = vel * MaxUpwardVelocity;
        }

        if (stabCooldown > 0f) {
            stabCooldown -= Time.deltaTime;
        }

        if (stabbing) {
            RaycastHit2D hit = Physics2D.Raycast(SwordPoint.transform.position, SwordPoint.transform.up, SwordPoint.transform.localScale.y * SwordPoint.transform.parent.localScale.y);
            Debug.DrawRay(SwordPoint.transform.position, SwordPoint.transform.up * SwordPoint.transform.localScale.y * SwordPoint.transform.parent.localScale.y, Color.red);
            if(hit.collider != null && hit.collider.CompareTag("Balloon") && hit.collider.gameObject != BalloonPart.gameObject) {
                EndStab();
                hit.collider.GetComponentInParent<Balloon>().Stabbed(hit.point);
            }
        }
    }

    protected void ApplyHeat() {
        if(Heat < MaxHeat) {
            Heat += AddHeatPerSecond * Time.deltaTime;
        }
    }

    protected void RemoveHeat() {
        if(Heat > 0f) {
            Heat -= RemoveHeatPerSecond * Time.deltaTime;
        }
    }

    protected IEnumerator FixHole(GameObject hole) {
        stabs.Remove(hole);
        fixingHole = hole;

        yield return new WaitForSeconds(TimeForHoleFix);

        Destroy(fixingHole);
        fixingHole = null;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.CompareTag("Swordpoint") || stabbedCooldown > 0f)
            return;

        Stabbed(collision.GetContact(0).point);
    }

    protected void Stabbed(Vector3 pos) {
        stabbedCooldown = 0.5f;

        GameObject go = Instantiate(PlayerBalloon.Instance.HolePrefab);
        go.transform.position = pos;
        go.transform.parent = transform;
        stabs.Add(go);
    }

    protected void Stab() {
        stabCooldown = StabCooldown;
        Sequence sq = DOTween.Sequence();
        sq.Append(SwordPoint.DOScaleY(1, 0.3f));
        sq.Append(SwordPoint.DOScaleY(0, 0.3f));

        stabbing = true;

        Invoke("EndStab", 0.3f);
    }

    protected void EndStab() {
        if (!stabbing)
            return;

        stabbing = false;
    }
}
