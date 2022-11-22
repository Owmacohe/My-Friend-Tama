using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectController : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] float pitchVariation;

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

    /// <summary>
    /// Sets a new random clip and pitch
    /// </summary>
    void SetClipAndPitch()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
    }
    
    /// <summary>
    /// Updates the clip and pitch, then plays the sound
    /// </summary>
    public void Play()
    {
        SetClipAndPitch();
        source.Play();
    }
}