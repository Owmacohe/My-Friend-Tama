using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TutorialSoundsController : MonoBehaviour
{
    [SerializeField] GameObject tama;
    [SerializeField] AudioSource[] mallTutorials;
    [SerializeField] AudioClip[] streamerTutorials;
    
    public enum MallTutorials { INTRO, GAME_START }
    public enum StreamerTutorials { TAMAGOTCHI }

    AudioSource streamerSource;
    int tutorialProgress;

    void Start()
    {
        streamerSource = GetComponent<AudioSource>();
    }

    public void PlayNextMallTutorial()
    {
        if (tutorialProgress > 0)
        {
            PlayMallTutorial((MallTutorials)tutorialProgress);   
        }
    }

    public void PlayMallTutorial(MallTutorials tut)
    {
        if ((int)tut == tutorialProgress && (int)tut < mallTutorials.Length)
        {
            foreach (AudioSource i in mallTutorials)
            {
                i.Stop();
            }
            
            mallTutorials[(int)tut].Play();
            tutorialProgress++;

            switch (tut)
            {
                case MallTutorials.INTRO:
                    tama.SetActive(true);
                    break;
                case MallTutorials.GAME_START:
                    tama.GetComponent<TamagotchiController>().Evolve();
                    break;
            }
        }
    }

    public void PlayStreamerTutorial(StreamerTutorials tut)
    {
        streamerSource.clip = streamerTutorials[(int) tut];
        streamerSource.Play();
    }
}