using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHighscore : MonoBehaviour
{
    public GameEventVector3 ScoreEvent;
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
        if (bs.BalloonStabbed.IsPlayer) {
            ScoreEvent.Raise(new Vector3(-100f, 0f, 0f));
        }

        if (bs.BalloonStabbing.IsPlayer) {
            ScoreEvent.Raise(new Vector3(100f, 0f, 0f));
        }
    }

    public void OnAddPoints(Vector3 amt) {
        Highscore.Value += (int)amt.x;
        if(amt.x > 0)
            AudioManager.PlaySound(Sound.AddScore);
        else
            AudioManager.PlaySound(Sound.SubtractScore);
    }
}
