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

    SubtitleController sc;

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

        sc = FindObjectOfType<SubtitleController>();
        
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
            
            if (sc.isPopulated && sc.isTutorial)
            {
                sc.Clear();
            }
        }

        if (!streamerSource.isPlaying && sc.isPopulated && !sc.isTutorial)
        {
            sc.Clear();
        }
    }

    public void PlayMallTutorial(int tut)
    {
        if (tut == tutorialProgress && tut < PAs.Count)
        {
            if (tutorialSubtitles != null && tut < tutorialSubtitles.Length)
                sc.Populate(tutorialSubtitles[tut], true);
            
            foreach (AudioSource i in PAs)
            {
                i.clip = mallTutorials[tut];
                i.Play();
            }
            
            tutorialProgress++;
        }
    }

    public void PlayNextStreamerTutorial()
    {
        PlayStreamerTutorial(streamerProgress);
    }

    public void PlayStreamerTutorial(GameObject volume)
    {
        if (volume.Equals(seeTamaVolume))
        {
            PlayStreamerTutorial(1);
        }
        else if (volume.Equals(seeFoodCourtVolume))
        {
            PlayStreamerTutorial(3);
        }
        else if (volume.Equals(seeArcadeVolume))
        {
            PlayStreamerTutorial(5);
        }
        else if (volume.Equals(seeBathroomVolume))
        {
            PlayStreamerTutorial(7);
        }
    }

    void PlayStreamerTutorial(int tut)
    {
        if (tut == streamerProgress)
        {
            if (streamerSubtitles != null && tut < streamerSubtitles.Length)
                sc.Populate(streamerSubtitles[tut], false);
            
            streamerSource.clip = streamerTutorials[tut];
            streamerSource.Play();

            streamerProgress++;
        }
    }
}