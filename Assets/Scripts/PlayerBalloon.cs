using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalloon : Balloon
{
    public static PlayerBalloon Instance;
    public GameObject HolePrefab;

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

        if (stabCooldown <= 0f && Input.GetMouseButton(0)) {
            Stab();
        }

        if (Input.GetMouseButtonDown(1)) {
            TryFix();
        }

        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Sword.LookAt(new Vector3(mPos.x, mPos.y, 0f));
    }

    protected void TryFix() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit.collider != null) {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
