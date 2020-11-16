using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatMan : MonoBehaviour
{
    public Vector3Variable PlayerBalloonPosition;
    public FloatReference SpeedIncrease;
    public GameEvent EndGameEvent;
    public IntVariable Score; 

    private float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        speed = speed * SpeedIncrease.Value;
        transform.position = transform.position + transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.CompareTag("Balloon")) return;

        if (collision.collider.GetComponentInParent<BalloonInfo>().IsPlayer) {
            speed = 0f;
            EndGameEvent.Raise();
        }
        else {
            Score.Value += 1000;
            speed = 1f;
        }
    }
}
