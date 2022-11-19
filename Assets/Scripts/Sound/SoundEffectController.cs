using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectController : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips;
    [SerializeField]
    float pitchVariation;
    
    // TODO: add option for 3D sound

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        SetClipAndPitch();

        if (source.playOnAwake)
        {
            Play();
        }
    }

    void SetClipAndPitch()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
    }

    public void Play()
    {
        SetClipAndPitch();
        source.Play();
    }
}