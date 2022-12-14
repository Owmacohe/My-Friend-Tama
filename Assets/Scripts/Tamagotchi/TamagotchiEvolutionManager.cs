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

    [HideInInspector] public bool isEvolveReady, isFirstTime, isEvolving;
    float lastEvolutionTime;

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

        pc.isPaused = true;
        
        Invoke(nameof(Evolve), 2);
    }

    public void Evolve()
    {
        if (!isEvolving && Time.time - lastEvolutionTime > 8)
        {
            isEvolving = true;
            lastEvolutionTime = Time.time;
            
            if (cc != null) cc.evolveMessages = true;

            fakeTama.SetActive(false);
            realTama.SetActive(true);

            pc.isPaused = true;

            int tamaAge = tc.tama.Age;

            if (tamaAge < 3)
            {
                if (tamaAge == 0)
                {
                    tsc.PlayStreamerTutorial(2);
                }
                else if (tamaAge == 1)
                {
                    cps.hasPassedCheckpoint2 = true;
                    tsc.PlayStreamerTutorial(4);
                }
                else if (tamaAge == 2)
                {
                    cps.hasPassedCheckpoint3 = true;
                    tsc.PlayStreamerTutorial(6);
                }
                
                tc.SlideTama(true, false);
                tc.WaitEvolve(1);

                Invoke(nameof(ReturnControl), 10);
            }
            else
            {
                pc.Pause();
                GetComponent<MenuManager>().LoadScene("Win");
            }
        }
    }

    void ReturnControl()
    {
        if (isFirstTime) tsc.PlayMallTutorial(0);

        if (cc != null) cc.evolveMessages = false;

        pc.isPaused = false;
        tc.SlideTama(false, false);

        switch (tc.tama.Age)
        {
            case 2:
                gc.arcadeGateADown = false;
                gc.arcadeGateBDown = false;
                
                tsc.PlayMallTutorial(4);
                break;
            case 3:
                gc.bathroomGateDown = false;
                
                tsc.PlayMallTutorial(7);
                break;
        }

        isEvolveReady = false;
        isFirstTime = false;
        isEvolving = false;
    }
}