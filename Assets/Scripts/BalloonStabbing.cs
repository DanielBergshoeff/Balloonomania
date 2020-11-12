using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStabbing : MonoBehaviour
{
    [Header("References")]
    public Transform SwordPoint;
    public Transform Sword;

    [Header("Abilities")]
    public FloatReference StabCooldown;

    protected float stabCooldown = 0f;
    protected bool stabbing = false;


    // Update is called once per frame
    protected void Update()
    {
        if (stabCooldown > 0f) {
            stabCooldown -= Time.deltaTime;
        }

        if (stabbing) {
            RaycastHit2D hit = Physics2D.Raycast(SwordPoint.transform.position, SwordPoint.transform.up, SwordPoint.transform.localScale.y * SwordPoint.transform.parent.localScale.y);
            if (hit.collider != null && hit.collider.CompareTag("Balloon") && hit.collider.transform.parent != transform) {
                BalloonGettingStabbed bgs = hit.collider.GetComponentInParent<BalloonGettingStabbed>();
                if (bgs != null) {
                    EndStab();
                    bgs.Stabbed(hit.point, hit.normal);
                    PushAwayFromPoint();
                }
            }
        }
    }

    private void PushAwayFromPoint() {
        throw new System.NotImplementedException();
    }

    public void Stab() {
        stabCooldown = StabCooldown.Value;
        Sequence sq = DOTween.Sequence();
        sq.Append(SwordPoint.DOScaleY(1, 0.3f));
        sq.Append(SwordPoint.DOScaleY(0, 0.3f));

        stabbing = true;

        Invoke("EndStab", 0.3f);
    }

    protected void EndStab() {
        if (!stabbing)
            return;

        stabbing = false;
    }
}
