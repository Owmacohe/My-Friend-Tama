using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{
    [HideInInspector] public AudioSource coinSFX;
    [HideInInspector] public AudioSource arcadeCabSFX;
    [HideInInspector] public AudioSource eatSFX;
    [HideInInspector] public AudioSource ghostHitSFX;

    // Start is called before the first frame update
    void Start()
    {

        coinSFX = gameObject.transform.Find("coinSFX").gameObject.GetComponent<AudioSource>();
        arcadeCabSFX = gameObject.transform.Find("arcadeCabSFX").gameObject.GetComponent<AudioSource>();
        eatSFX = gameObject.transform.Find("eatSFX").gameObject.GetComponent<AudioSource>();
        ghostHitSFX = gameObject.transform.Find("ghostHitSFX").gameObject.GetComponent<AudioSource>();
    }

}
