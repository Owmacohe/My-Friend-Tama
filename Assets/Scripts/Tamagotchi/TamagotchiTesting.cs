using System;
using TMPro;
using UnityEngine;

public class TamagotchiTesting : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    TMP_Text info;

    Tamagotchi tama;
    
    void Start()
    {
        tama = new Tamagotchi();
    }

    void FixedUpdate()
    {
        tama.UpdateStats(speed);
        info.text = tama.ToString();
    }

    public void Feed()
    {
        tama.Feed();
    }
}