using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabEffect : MonoBehaviour
{
    public GameObject ParticlePrefab;
    public GameEvent PlayerStabbed;

    private AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnStab(BalloonStab balloonStab) {
        myAudioSource.PlayOneShot(AudioManager.GetSound(Sound.BalloonPop));
        GameObject go = Instantiate(ParticlePrefab);
        go.transform.position = balloonStab.StabPosition;

        if (balloonStab.BalloonStabbed.IsPlayer) {
            PlayerStabbed.Raise();
        }
    }
}
