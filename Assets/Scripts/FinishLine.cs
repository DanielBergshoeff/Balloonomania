using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject WinCanvas;
    public GameObject LossCanvas;

    private bool gameOver = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Balloon") || gameOver)
            return;

        BalloonInfo bi = collision.GetComponentInParent<BalloonInfo>();
        if (bi.IsPlayer) {
            YouWon();
        }
        else {
            YouLost();
        }

        gameOver = true;
    }

    private void YouWon() {
        WinCanvas.SetActive(true);
    }

    private void YouLost() {
        LossCanvas.SetActive(true);
    }
}
