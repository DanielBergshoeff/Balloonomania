using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPlayerGettingStabbed : BalloonGettingStabbed
{
    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(1)) {
            TryFix();
        }
    }

    protected void TryFix() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Hole")) {
            if (stabs.Contains(hit.collider.gameObject)) {
                StartCoroutine(FixHole(hit.collider.gameObject));
            }
        }
    }
}
