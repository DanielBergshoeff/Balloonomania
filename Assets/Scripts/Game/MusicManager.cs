using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource myAudioSource;

    private void Awake() {
        myAudioSource = GetComponent<AudioSource>();
    }
}
