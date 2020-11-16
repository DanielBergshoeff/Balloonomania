using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonAIStabbing : BalloonStabbing
{
    public Vector3Variable PlayerBalloonPosition;
    public FloatReference StabDistance;

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        Vector3 mPos = PlayerBalloonPosition.Value;
        Sword.LookAt(new Vector3(mPos.x, mPos.y, 0f));

        if (stabCooldown <= 0f)
            TryStab();
    }

    private void TryStab() {
        if ((transform.position - PlayerBalloonPosition.Value).sqrMagnitude < StabDistance.Value * StabDistance.Value) {
            Stab();
        }
    }
}
