using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    static SoundManager Singleton;
    private AudioSource audioSource;

    private void Awake() {
        if (Singleton) Destroy(Singleton);
        Singleton = this;

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip clip) {
        Singleton.audioSource.PlayOneShot(clip);
    }
}
