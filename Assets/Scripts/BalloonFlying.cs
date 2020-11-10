using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlying : MonoBehaviour
{
    [Header("References")]
    public Vector3Variable BalloonPartPosition;

    [Header("Heat information")]
    public FloatReference MaxHeat;
    public FloatReference AddHeatPerSecond;
    public FloatReference RemoveHeatPerSecond;
    public FloatReference StandardHeatLoss;
    
    [SerializeField] protected FloatReference Heat;

    [Header("Speed")]
    public FloatReference UpwardVelocity;
    public FloatReference MaxUpwardVelocity;
    public FloatReference HorizontalSpeed;

    protected Rigidbody2D myRigidbody;
    protected bool grounded;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Heat.Value > 0f) {
            Heat.Value -= StandardHeatLoss.Value * Time.deltaTime;
        }

        myRigidbody.AddForce(transform.up * Heat.Value * UpwardVelocity.Value);

        if (myRigidbody.velocity.magnitude > MaxUpwardVelocity.Value) {
            Vector2 vel = myRigidbody.velocity.normalized;
            myRigidbody.velocity = vel * MaxUpwardVelocity.Value;
        }

        grounded = Physics2D.Raycast(transform.position - transform.up * 0.01f, -transform.up, 0.15f).collider != null;
        if (!grounded)
            transform.position = transform.position + transform.right * Time.deltaTime * HorizontalSpeed.Value * GameManager.GetSpeed(BalloonPartPosition.Value);
    }

    protected void ApplyHeat() {
        if (Heat.Value < MaxHeat.Value) {
            Heat.Value += AddHeatPerSecond.Value * Time.deltaTime;
        }
    }

    protected void RemoveHeat() {
        if (Heat.Value > 0f) {
            Heat.Value -= RemoveHeatPerSecond.Value * Time.deltaTime;
        }
    }
}
