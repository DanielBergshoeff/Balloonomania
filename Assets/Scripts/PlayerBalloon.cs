using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalloon : Balloon
{
    public static PlayerBalloon Instance;
    public Transform Sword;
    public Transform SwordPoint;
    public GameObject HolePrefab;
    public float StabCooldown = 1f;
    public float StabDistance = 3f;

    private float stabCooldown = 0f;

    private void Awake() {
        Instance = this;
    }

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

        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Sword.LookAt(new Vector3(mPos.x, mPos.y, 0f));
    }

    private void Stab() {
        stabCooldown = StabCooldown;

        Sequence sq = DOTween.Sequence();
        sq.Append(SwordPoint.DOScaleY(1, 0.3f));
        sq.Append(SwordPoint.DOScaleY(0.1f, 0.3f));
    }
}
