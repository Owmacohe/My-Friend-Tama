using System;
using UnityEngine;

public class TamagotchiEvolutionManager : MonoBehaviour
{
    [SerializeField] GameObject realTama, fakeTama;

    TamagotchiController tc;
    PlayerController pc;

    [HideInInspector] public bool isEvolveReady, isFirstTime;

    void Start()
    {
        tc = realTama.GetComponent<TamagotchiController>();
        tc.Start();

        pc = FindObjectOfType<PlayerController>();

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
        fakeTama.SetActive(false);
        realTama.SetActive(true);

        pc.keyboardInteractionPaused = true;

        tc.SlideTama(false);
        tc.WaitEvolve(1);

        isEvolveReady = false;
        isFirstTime = false;
        
        Invoke(nameof(ReturnControl), 12);
    }

    void ReturnControl()
    {
        pc.keyboardInteractionPaused = false;
        tc.SlideTama();
    }
}