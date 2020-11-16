using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject Screen;

    public void EndGame() {
        Screen.SetActive(true);
        Time.timeScale = 0f;
        AudioManager.PlaySound(Sound.GameFinished);
    }

    public void StartOver() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
