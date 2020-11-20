using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public FloatReference GlobalSpeed;
    private AudioSource myAudioSource;

    private void Awake() {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        myAudioSource.pitch = ((GlobalSpeed.Value - 1) / 2) + 1f;
    }
}
