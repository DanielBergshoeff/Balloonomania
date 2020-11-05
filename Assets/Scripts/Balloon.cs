using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public Transform BalloonPart;
    public Transform FirePart;
    public float UpwardVelocity = 1f;
    public float MaxUpwardVelocity = 2f;
    public float HorizontalSpeed = 1f;

    public float MaxHeat = 3f;
    public float AddHeatPerSecond = 2f;
    public float RemoveHeatPerSecond = 2f;
    public float StandardHeatLoss = 1f;
    public float HoleHeatLoss = 3f;

    public float Heat = 0f;

    protected Rigidbody2D myRigidbody;
    protected bool grounded = false;
    protected List<GameObject> stabs;
    protected float stabbedCooldown = 0f;

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
        }

        FirePart.transform.localScale = (Heat / MaxHeat) * 1.5f * Vector3.one;

        myRigidbody.AddForce(transform.up * Heat * UpwardVelocity);

        grounded = Physics2D.Raycast(transform.position - transform.up * 0.01f, -transform.up, 0.15f).collider != null;

        if (!grounded)
            transform.position = transform.position + transform.right * Time.deltaTime * HorizontalSpeed * GameManager.GetSpeed(BalloonPart.position);

        if (myRigidbody.velocity.magnitude > MaxUpwardVelocity) {
            Vector2 vel = myRigidbody.velocity.normalized;
            myRigidbody.velocity = vel * MaxUpwardVelocity;
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.CompareTag("Swordpoint") || stabbedCooldown > 0f)
            return;

        stabbedCooldown = 0.5f;

        GameObject go = Instantiate(PlayerBalloon.Instance.HolePrefab);
        go.transform.position = collision.GetContact(0).point;
        go.transform.parent = transform;
        stabs.Add(go);
    }
}
