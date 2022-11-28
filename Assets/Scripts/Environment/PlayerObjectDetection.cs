using System;
using UnityEngine;

public class PlayerObjectDetection : MonoBehaviour
{
    [SerializeField] GameObject AudioManager;
    [SerializeField] GameObject washroomSpawnerOBJ;
    [SerializeField] GameObject connectedLightSwitch;
    [SerializeField] GameObject washroomLights;
    public bool hasCoin;

    TamagotchiController tc;
    bool inWashroom;
    bool isPlayingWashroomGame;
    AudioManagerMenu audioManagerMenu;
    PlayerController playercontroller;

    void Start()
    {
        tc = FindObjectOfType<TamagotchiController>();
        audioManagerMenu = AudioManager.GetComponent<AudioManagerMenu>();

        // TODO: this first evolution to be removed and placed where we want it somewhere in the tutorial
        Invoke(nameof(TamaEvolve), 5);

        washroomSpawnerOBJ.SetActive(false);
    }

    void TamaEvolve()
    {
        tc.Evolve();
    }

    void FixedUpdate()
    {
        if (isPlayingWashroomGame)
        {
            tc.tama.Scold(0.005f);
            
            if (tc.tama.Discipline >= 0.8f && (int)tc.tama.Age == 3) // TODO: this condition may want to be tweaked
            {
                // TODO: end of game?
            }
        }
    }

    void CheckWashroomSpawner()
    {

        if (!connectedLightSwitch.GetComponent<LightSwitchBool>().lightOn
            && inWashroom)
        {
            washroomLights.SetActive(false);

            playercontroller = GetComponent<PlayerController>();

            if (playercontroller.flashlightOn)
            {
                washroomSpawnerOBJ.SetActive(false);

                isPlayingWashroomGame = false;
 
            } 
            else if (!playercontroller.flashlightOn)
            {
                washroomSpawnerOBJ.SetActive(true);

                isPlayingWashroomGame = true;
            }
        }

        else
        {
            washroomSpawnerOBJ.SetActive(false);
            washroomLights.SetActive(true);
            
            isPlayingWashroomGame = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log($"Collide with '{other.gameObject}'");
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.gameObject.CompareTag("Food"))
            {
                audioManagerMenu.eatSFX.PlayOneShot(audioManagerMenu.eatSFX.clip);

                Destroy(other.gameObject);
                
                tc.tama.Feed(0.2f);

                if (tc.tama.Food >= 0.8f && (int)tc.tama.Age == 1) // TODO: this condition may want to be tweaked
                {
                    tc.Evolve();
                }
            }
            else if (other.gameObject.CompareTag("Money"))
            {
                if (!hasCoin)
                {
                    audioManagerMenu.coinSFX.PlayOneShot(audioManagerMenu.coinSFX.clip);
                    Destroy(other.gameObject);
                    hasCoin = true;
                }
            }
            else if (other.gameObject.CompareTag("ArcadeCab"))
            {
                CoinCheck(other);
                
                tc.tama.Play(0.2f);
                
                if (tc.tama.Happiness >= 0.8f && (int)tc.tama.Age == 2) // TODO: this condition may want to be tweaked
                {
                    tc.Evolve();
                }
            }
            else if (other.gameObject == connectedLightSwitch)
            {
                var lightSwitch = connectedLightSwitch.GetComponent<LightSwitchBool>();

                lightSwitch.lightOn = !lightSwitch.lightOn;
                CheckWashroomSpawner();
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Starts spawning washroom ghosts
        if (other.gameObject.CompareTag("washroomStart"))
        {
            inWashroom = true;
            CheckWashroomSpawner();
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Stops spawning washroom ghosts
        if (other.gameObject.CompareTag("washroomStart"))
        {
            inWashroom = false;
            connectedLightSwitch.GetComponent<LightSwitchBool>().lightOn = true;
            CheckWashroomSpawner();
        }
    }

    public void CoinCheck(Collider other)
    {
        if (hasCoin)
        {
            audioManagerMenu.arcadeCabSFX.PlayOneShot(audioManagerMenu.arcadeCabSFX.clip);
            hasCoin = false;
        }
    }
}
