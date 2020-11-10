using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonAIGettingStabbed : BalloonGettingStabbed
{
    public FloatReference WaitForFix;
    private float waiting = 0f;

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        if (waiting <= 0f && stabs.Count > 0) {
            waiting = WaitForFix.Value;
        }

        if (waiting > 0f) {
            waiting -= Time.deltaTime;
            if (waiting <= 0f) {
                StartCoroutine(FixHole(stabs[0]));
            }
        }
    }
}
