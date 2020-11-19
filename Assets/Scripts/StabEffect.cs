using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StabEffect : MonoBehaviour
{
    public GameObject ParticlePrefab;
    public GameEvent PlayerStabbed;

    private AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioMixer mixer = Resources.Load("Main") as AudioMixer;
        AudioMixerGroup amx = mixer.FindMatchingGroups("Rest")[0];

        myAudioSource = gameObject.AddComponent<AudioSource>();
        myAudioSource.outputAudioMixerGroup = amx;
    }

    public void OnStab(BalloonStab balloonStab) {
        AudioManager.PlaySound(Sound.BalloonPop);
        GameObject go = Instantiate(ParticlePrefab);
        go.transform.position = balloonStab.StabPosition;

        if (balloonStab.BalloonStabbed.IsPlayer) {
            PlayerStabbed.Raise();
        }
    }
}
