using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonStabbing : MonoBehaviour
{
    public GameEventBalloonStab StabEvent;

    [Header("References")]
    public Transform SwordPoint;
    public Transform Sword;

    [Header("Abilities")]
    public FloatReference StabCooldown;

    protected float stabCooldown = 0f;
    protected bool stabbing = false;

    private float pushFromPoint = 0f;
    private Vector3 pushPoint;


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
                    BalloonStab bs = new BalloonStab();
                    bs.BalloonStabbed = bgs.GetComponent<BalloonInfo>();
                    bs.BalloonStabbing = GetComponent<BalloonInfo>();
                    bs.StabPosition = hit.point;
                    bs.StabNormal = hit.normal;
                    StabEvent.Raise(bs);
                    PushAwayFromPoint(hit.point);
                }
            }
        }

        if(pushFromPoint > 0f) {
            pushFromPoint -= Time.deltaTime;
            Vector3 dir = transform.position - pushPoint;
            dir = new Vector3(dir.x, dir.y, 0f);
            transform.position = transform.position + dir.normalized * pushFromPoint * Time.deltaTime * 10f;
        }
    }

    private void PushAwayFromPoint(Vector3 point) {
        pushPoint = point;
        pushFromPoint = 1f;
    }

    public void Stab() {
        stabCooldown = StabCooldown.Value;
        Sequence sq = DOTween.Sequence();
        sq.Append(SwordPoint.DOScaleY(1, 0.3f));
        sq.Append(SwordPoint.DOScaleY(0, 0.3f));

        stabbing = true;

        Invoke("EndStab", 0.3f);

        AudioManager.PlaySound(Sound.SwordStab);
    }

    protected void EndStab() {
        if (!stabbing)
            return;

        stabbing = false;
    }
}
