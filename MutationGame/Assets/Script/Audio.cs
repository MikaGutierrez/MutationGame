using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSr;
    public AudioClip[] audioClips;
    //ChangeAnimationState(Name);
    public void PlaySounds(AudioClip clip, float volume = 1f, float p1 = 1f, float p2 = 1f)
    {
        audioSr.pitch = Random.Range(p1, p2);
        audioSr.PlayOneShot(clip, volume);
    }
}
