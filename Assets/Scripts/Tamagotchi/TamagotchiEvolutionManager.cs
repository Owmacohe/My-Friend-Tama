using System;
using UnityEngine;

public class TamagotchiEvolutionManager : MonoBehaviour
{
    [SerializeField] GameObject realTama, fakeTama;

    TamagotchiController tc;
    PlayerController pc;
    ChatController cc;
    GateControlScript gc;

    [HideInInspector] public bool isEvolveReady, isFirstTime;
    bool isEvolving;

    void Start()
    {
        tc = realTama.GetComponent<TamagotchiController>();
        tc.Start();

        pc = FindObjectOfType<PlayerController>();
        cc = FindObjectOfType<ChatController>();
        gc = FindObjectOfType<GateControlScript>();

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

            tc.SlideTama(true, false);
            tc.WaitEvolve(1);

            isEvolveReady = false;
            isFirstTime = false;

            Invoke(nameof(ReturnControl), 12);   
        }
    }

    void ReturnControl()
    {
        if (cc != null)
            cc.evolveMessages = false;

        pc.keyboardInteractionPaused = false;
        tc.SlideTama(false, false);

        switch ((int)tc.tama.Age)
        {
            case 2:
                gc.ArcadeGateADown = false;
                gc.ArcadeGateBDown = false;
                break;
            case 3:
                gc.BathroomGateDown = false;
                break;
        }

        isEvolving = false;
    }
}