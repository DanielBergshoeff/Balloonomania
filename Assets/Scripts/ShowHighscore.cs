using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHighscore : MonoBehaviour
{
    public IntVariable Highscore;
    public TextMeshProUGUI HighscoreText;

    private void Awake() {
        Highscore.Value = 0;
    }

    private void FixedUpdate() {
        Highscore.Value += 1;
        HighscoreText.text = Highscore.Value.ToString();
    }

    public void OnStab(BalloonStab bs) {
        if (bs.BalloonStabbed.IsPlayer)
            Highscore.Value -= 100;

        if (bs.BalloonStabbing.IsPlayer)
            Highscore.Value += 100;
    }
}
