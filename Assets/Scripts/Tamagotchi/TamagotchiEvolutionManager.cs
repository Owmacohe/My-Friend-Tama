using System;
using UnityEngine;

public class TamagotchiEvolutionManager : MonoBehaviour
{
    [SerializeField] GameObject realTama, fakeTama;

    TamagotchiController tc;
    PlayerController pc;
    ChatController cc;
    GateControlScript gc;
    TutorialSoundsController tsc;
    CheckpointScript cps;

    [HideInInspector] public bool isEvolveReady, isFirstTime;
    bool isEvolving;

    void Start()
    {
        tc = realTama.GetComponent<TamagotchiController>();
        tc.Start();

        pc = FindObjectOfType<PlayerController>();
        cc = FindObjectOfType<ChatController>();
        gc = FindObjectOfType<GateControlScript>();
        tsc = FindObjectOfType<TutorialSoundsController>();
        cps = FindObjectOfType<CheckpointScript>();

        isEvolveReady = true;
        isFirstTime = true;
    }

    public void Place()
    {
        fakeTama.SetActive(true);
        realTama.SetActive(false);

        pc.keyboardInteractionPaused = true;
        
        Invoke(nameof(Evolve), 4);
    }

    public void Evolve()
    {
        if (!isEvolving)
        {
            isEvolving = true;
            
            if (cc != null)
                cc.evolveMessages = true;

            fakeTama.SetActive(false);
            realTama.SetActive(true);

            pc.keyboardInteractionPaused = true;

            switch ((int)tc.tama.Age)
            {
                case 1:
                    cps.hasPassedCheckpoint2 = true;
                    break;
                case 2:
                    cps.hasPassedCheckpoint3 = true;
                    break;
                case 3:
                    FindObjectOfType<MenuManager>().LoadScene("Win");
                    break;
            }

            if ((int)tc.tama.Age >= 3)
            {
                GetComponent<MenuManager>().LoadScene("Win");
            }
            else
            {
                tc.SlideTama(true, false);
                tc.WaitEvolve(1);

                tsc.PlayNextStreamerTutorial();

                Invoke(nameof(ReturnControl), 14);
            }
        }
    }

    void ReturnControl()
    {
        if (isFirstTime)
        {
            tsc.PlayMallTutorial(0);
        }

        if (cc != null)
            cc.evolveMessages = false;

        pc.keyboardInteractionPaused = false;
        tc.SlideTama(false, false);

        switch ((int)tc.tama.Age)
        {
            case 2:
                gc.ArcadeGateADown = false;
                gc.ArcadeGateBDown = false;
                
                tsc.PlayMallTutorial(4);
                break;
            case 3:
                gc.BathroomGateDown = false;
                
                tsc.PlayMallTutorial(7);
                break;
        }

        isEvolveReady = false;
        isFirstTime = false;
        isEvolving = false;
    }
}