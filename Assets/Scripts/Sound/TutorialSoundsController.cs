using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TutorialSoundsController : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] AudioClip[] mallTutorials;
    [TextArea(1, 100)]
    [SerializeField] string[] tutorialSubtitles;
    
    [Header("Streamer")]
    [SerializeField] AudioClip[] streamerTutorials;
    [TextArea(1, 100)]
    [SerializeField] string[] streamerSubtitles;
    [SerializeField] GameObject seeTamaVolume, seeFoodCourtVolume, seeArcadeVolume, seeBathroomVolume;

    List<AudioSource> PAs = new List<AudioSource>();
    AudioSource streamerSource;
    
    [HideInInspector] public int tutorialProgress;
    int streamerProgress;
    [HideInInspector] public bool isPlaying;

    SubtitleController subtitles;

    void Start()
    {
        streamerSource = GetComponent<AudioSource>();
        
        AudioSource[] temp = GetComponentsInChildren<AudioSource>();

        for (int i = 0; i < temp.Length; i++)
        {
            if (i > 0)
            {
                PAs.Add(temp[i]);
            }
        }

        subtitles = FindObjectOfType<SubtitleController>();
        
        PlayStreamerTutorial(0);
    }

    void Update()
    {
        if (PAs[0].isPlaying)
        {
            isPlaying = true;
        }
        else
        {
            isPlaying = false;
            
            if (subtitles.isPopulated && subtitles.isTutorial)
            {
                subtitles.Clear();
            }
        }

        if (!streamerSource.isPlaying && subtitles.isPopulated && !subtitles.isTutorial)
        {
            subtitles.Clear();
        }
    }

    public bool PlayMallTutorial(int tut)
    {
        if (tut == tutorialProgress && tut < PAs.Count)
        {
            if (tutorialSubtitles != null && tut < tutorialSubtitles.Length)
                subtitles.Populate(tutorialSubtitles[tut], true);
            
            foreach (AudioSource i in PAs)
            {
                i.clip = mallTutorials[tut];
                i.Play();
            }
            
            tutorialProgress++;

            return true;
        }

        return false;
    }

    public bool PlayNextStreamerTutorial()
    {
        return PlayStreamerTutorial(streamerProgress);
    }

    public bool PlayStreamerTutorial(GameObject volume)
    {
        if (volume.Equals(seeTamaVolume))
        {
            Destroy(volume);
            return PlayStreamerTutorial(1);
        }
        else if (volume.Equals(seeFoodCourtVolume))
        {
            Destroy(volume);
            return PlayStreamerTutorial(3);
        }
        else if (volume.Equals(seeArcadeVolume))
        {
            Destroy(volume);
            return PlayStreamerTutorial(5);
        }
        else if (volume.Equals(seeBathroomVolume))
        {
            Destroy(volume);
            return PlayStreamerTutorial(7);
        }

        return false;
    }

    bool PlayStreamerTutorial(int tut)
    {
        if (tut == streamerProgress)
        {
            if (streamerSubtitles != null && tut < streamerSubtitles.Length)
                subtitles.Populate(streamerSubtitles[tut], false);
            
            streamerSource.clip = streamerTutorials[tut];
            streamerSource.Play();

            streamerProgress++;

            return true;
        }

        return false;
    }
}