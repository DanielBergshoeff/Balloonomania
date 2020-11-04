using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalloon : Balloon
{
    public GameObject HolePrefab;
    public float StabCooldown = 1f;
    public float StabDistance = 3f;

    private float stabCooldown = 0f;

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
            ApplyHeat();
        }
        if (Input.GetKey(KeyCode.S)) {
            RemoveHeat();
        }

        if (stabCooldown > 0f) {
            stabCooldown -= Time.deltaTime;
        }

        if (stabCooldown <= 0f && Input.GetMouseButton(0)) {
            Stab();
        }
    }

    private void Stab() {
        stabCooldown = StabCooldown;

        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Debug.DrawRay(transform.position, dir.normalized * StabDistance);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, StabDistance);
        if (hit.collider.CompareTag("Balloon")) {
            Balloon b = hit.collider.GetComponentInParent<Balloon>();

            if (b != null) {
                GameObject go = Instantiate(HolePrefab);
                go.transform.position = hit.point;
                b.GetStabbed(go);
            }
        }
    }
}
