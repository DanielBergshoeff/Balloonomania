using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public Transform BalloonPart;
    public float UpwardVelocity = 1f;
    public float MaxUpwardVelocity = 2f;
    public float HorizontalSpeed = 1f;

    public float MaxHeat = 3f;
    public float AddHeatPerSecond = 2f;
    public float RemoveHeatPerSecond = 2f;
    public float StandardHeatLoss = 1f;

    public float Heat = 0f;

    private Rigidbody2D myRigidbody;
    private bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Heat > 0f)
            Heat -= StandardHeatLoss * Time.deltaTime;

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
            ApplyHeat();
        }
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.S)) {
            RemoveHeat();
        }

        myRigidbody.AddForce(transform.up * Heat * UpwardVelocity);

        if(myRigidbody.velocity.magnitude > MaxUpwardVelocity) {
            Vector2 vel = myRigidbody.velocity.normalized;
            myRigidbody.velocity = vel * MaxUpwardVelocity;
        }

        grounded = Physics2D.Raycast(transform.position - transform.up * 0.01f, -transform.up, 0.15f).collider != null;

        if(!grounded)
            transform.position = transform.position + transform.right * Time.deltaTime * HorizontalSpeed * GameManager.GetSpeed(BalloonPart.position);
    }

    private void ApplyHeat() {
        if(Heat < MaxHeat) {
            Heat += AddHeatPerSecond * Time.deltaTime;
        }
    }

    private void RemoveHeat() {
        if(Heat > 0f) {
            Heat -= RemoveHeatPerSecond * Time.deltaTime;
        }
    }
}
