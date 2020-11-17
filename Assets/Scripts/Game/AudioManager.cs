using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public List<SoundToClip> AllClips;

    private AudioSource myAudioSource;
    private AudioSource ambientAudioSource;

    void Awake()
    {
        Instance = this;
        myAudioSource = GetComponent<AudioSource>();
        ambientAudioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start() {
        ambientAudioSource.clip = GetSound(Sound.Ambient);
        ambientAudioSource.loop = true;
        ambientAudioSource.Play();
    }

    public static AudioClip GetSound(Sound soundToPlay) {
        if (Instance == null || Instance.AllClips == null)
            return null;

        foreach (SoundToClip stc in Instance.AllClips) {
            if(stc.MySound == soundToPlay) {
                return stc.MyClip;
            }
        }

        return null;
    }

    public static void PlaySound(Sound soundToPlay) {
        if (Instance == null || Instance.AllClips == null)
            return;

        foreach (SoundToClip stc in Instance.AllClips) {
            if (stc.MySound == soundToPlay) {
                Instance.myAudioSource.PlayOneShot(stc.MyClip);
            }
        }
    }
}

public enum Sound
{
    Ascend,
    SwordStab,
    Deflate,
    Repair,
    Curse,
    Pickup,
    ThrowHook,
    PullHook,
    AddScore,
    SubtractScore,
    FatMan,
    Squashed,
    Ambient,
    GameFinished,
    WindSlower,
    WindFaster
}

[System.Serializable]
public class SoundToClip
{
    public Sound MySound;
    public AudioClip MyClip;
}
