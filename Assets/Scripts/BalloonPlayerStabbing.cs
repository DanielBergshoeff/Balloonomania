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

        float z_plane = 3.5f;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mPos = ray.origin + ray.direction * (z_plane - ray.origin.z) / ray.direction.z;
        Sword.transform.position = mPos;
        //Sword.LookAt(new Vector3(mPos.x, mPos.y, transform.position.z));
    }
}
