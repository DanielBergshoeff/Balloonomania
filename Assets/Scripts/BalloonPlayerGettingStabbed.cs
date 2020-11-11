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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (hit.collider != null && hit.collider.CompareTag("Hole")) {
            if (stabs.Contains(hit.collider.transform.parent.gameObject)) {
                StartCoroutine(FixHole(hit.collider.transform.parent.gameObject));
            }
        }
    }
}
