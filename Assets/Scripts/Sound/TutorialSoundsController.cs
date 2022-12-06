using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TutorialSoundsController : MonoBehaviour
{
    [SerializeField] AudioClip[] mallTutorials;
    [SerializeField] AudioClip[] streamerTutorials;

    AudioSource[] PAs;
    AudioSource streamerSource;
    
    [HideInInspector] public int tutorialProgress;
    [HideInInspector] public bool isPlaying;

    void Start()
    {
        streamerSource = GetComponent<AudioSource>();
        PAs = GetComponentsInChildren<AudioSource>();
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
        }
    }

    public void PlayMallTutorial(int tut)
    {
        if (tut == tutorialProgress && tut < PAs.Length)
        {
            foreach (AudioSource i in PAs)
            {
                i.clip = mallTutorials[tut];
                i.Play();
            }

            if (tut == 1)
            {
                FindObjectOfType<HintButtons>().HideAllButtons();   
            }
            
            tutorialProgress++;
        }
    }

    public void PlayStreamerTutorial(int tut)
    {
        streamerSource.clip = streamerTutorials[tut];
        streamerSource.Play();
    }
}