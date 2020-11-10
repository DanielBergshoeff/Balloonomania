using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPlayerStabbing : BalloonStabbing
{ 
    protected new void Update()
    {
        base.Update();

        if (stabCooldown <= 0f && Input.GetMouseButton(0)) {
            Stab();
        }

        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Sword.LookAt(new Vector3(mPos.x, mPos.y, 0f));
    }
}
