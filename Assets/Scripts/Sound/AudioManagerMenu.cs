using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{
    [HideInInspector] public AudioSource coinSFX;
    [HideInInspector] public AudioSource arcadeCabSFX;
    [HideInInspector] public AudioSource eatSFX;
    [HideInInspector] public AudioSource ghostHitSFX;
    [HideInInspector] public AudioSource gateShutSFX;
    [HideInInspector] public AudioSource mall_intro;
    [HideInInspector] public AudioSource mall_gameStart;

    // Start is called before the first frame update
    void Start()
    {

        coinSFX = gameObject.transform.Find("coinSFX").gameObject.GetComponent<AudioSource>();
        arcadeCabSFX = gameObject.transform.Find("arcadeCabSFX").gameObject.GetComponent<AudioSource>();
        eatSFX = gameObject.transform.Find("eatSFX").gameObject.GetComponent<AudioSource>();
        ghostHitSFX = gameObject.transform.Find("ghostHitSFX").gameObject.GetComponent<AudioSource>();
        gateShutSFX = gameObject.transform.Find("gateShutSFX").gameObject.GetComponent<AudioSource>();
        mall_intro = gameObject.transform.Find("Tutorial").gameObject.transform.Find("mall_intro").gameObject.GetComponent<AudioSource>();
        mall_gameStart = gameObject.transform.Find("Tutorial").gameObject.transform.Find("mall_gameStart").gameObject.GetComponent<AudioSource>();

    }
}
