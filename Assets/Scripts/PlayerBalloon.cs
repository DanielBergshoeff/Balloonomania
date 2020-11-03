using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalloon : Balloon
{

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) {
            ApplyHeat();
        }
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.S)) {
            RemoveHeat();
        }
    }
}
