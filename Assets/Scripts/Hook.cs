using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hook : MonoBehaviour
{
    public BalloonEvent HookHitEvent;
    public BalloonInfo ThrowingPlayer;

    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Balloon") || collision.gameObject.transform.parent.GetComponent<BalloonInfo>() == ThrowingPlayer)
            return;

        myRigidbody.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = collision.transform;

        HookHitEvent.Invoke(collision.transform.parent.GetComponent<BalloonInfo>());
        Destroy(gameObject);
    }
}

[Serializable]
public class BalloonEvent : UnityEvent<BalloonInfo> { }
