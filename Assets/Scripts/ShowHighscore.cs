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

    // Update is called once per frame
    void Update()
    {
        HighscoreText.text = Highscore.Value.ToString();
    }
}
