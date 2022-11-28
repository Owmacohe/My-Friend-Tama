using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{
    private AudioSource OfficeAmbientLoopMusic;
    private AudioSource OfficeNoiseLoopMusic;

    // Start is called before the first frame update
    void Start()
    {
       
        OfficeAmbientLoopMusic = gameObject.transform.Find("OfficeAmbientLoopMusic").gameObject.GetComponent<AudioSource>();
        OfficeNoiseLoopMusic = gameObject.transform.Find("OfficeNoiseLoopMusic").gameObject.GetComponent<AudioSource>();

        if (!OfficeAmbientLoopMusic.isPlaying)
        {
            OfficeAmbientLoopMusic.Play();
        }

        if (!OfficeNoiseLoopMusic.isPlaying)
        {
            OfficeNoiseLoopMusic.Play();
        }
    }

}
