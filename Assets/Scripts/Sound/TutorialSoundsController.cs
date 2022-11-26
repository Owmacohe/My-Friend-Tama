using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TutorialSoundsController : MonoBehaviour
{
    [SerializeField] AudioSource[] mallTutorials;
    [SerializeField] AudioClip[] streamerTutorials;
    
    public enum MallTutorials { TEST, RETURN_TO_ENTRANCE, FOOD_COURT, ARCADE, RESTROOM }
    public enum StreamerTutorials { TAMAGOTCHI }

    AudioSource streamerSource;

    void Start()
    {
        streamerSource = GetComponent<AudioSource>();
        
        PlayMallTutorial(MallTutorials.TEST);
    }

    public void PlayMallTutorial(MallTutorials tut)
    {
        mallTutorials[(int)tut].Play();
    }

    public void PlayStreamerTutorial(StreamerTutorials tut)
    {
        streamerSource.clip = streamerTutorials[(int) tut];
        streamerSource.Play();
    }
}