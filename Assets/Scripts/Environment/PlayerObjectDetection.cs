using System;
using UnityEngine;

public class PlayerObjectDetection : MonoBehaviour
{
    [SerializeField] GameObject AudioManager;
    [SerializeField] GameObject washroomSpawnerOBJ;
    [SerializeField] GameObject connectedLightSwitch;
    [SerializeField] GameObject washroomLights;
    [SerializeField] TamagotchiController tc;
    [SerializeField] TamagotchiEvolutionManager tem;
    public bool hasCoin;

    bool inWashroom;
    bool isPlayingWashroomGame;
    bool hasRealTama;
    AudioManagerMenu audioManagerMenu;
    PlayerController playercontroller;
    GateControlScript gateControlScript;
    TutorialSoundsController tutorial;

    void Start()
    {
        audioManagerMenu = AudioManager.GetComponent<AudioManagerMenu>();
        gateControlScript = FindObjectOfType<GateControlScript>();
        tutorial = FindObjectOfType<TutorialSoundsController>();

        washroomSpawnerOBJ.SetActive(false);
    }

    void FixedUpdate()
    {
        CheckWashroomSpawner();

        if (isPlayingWashroomGame)
        {
            tc.tama.Scold(0.005f);
            
            if (tc.tama.Discipline >= 0.8f && (int)tc.tama.Age == 3) // TODO: this condition may want to be tweaked
            {
                // TODO: raise gates
                // TODO: end of game?
                tc.SetUpdatingStats(false);
            }
        }
        
        if (hasRealTama && !tutorial.isPlaying && gateControlScript.TutorialGateDown && tutorial.tutorialProgress == 2)
        {
            gateControlScript.TutorialGateDown = false;
        }
    }

    void CheckWashroomSpawner()
    {
        if (!connectedLightSwitch.GetComponent<LightSwitchBool>().lightOn
            && inWashroom)
        {
            washroomLights.SetActive(false);
            washroomSpawnerOBJ.SetActive(true);

            isPlayingWashroomGame = true;

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
        if (Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.CompareTag("Food"))
            {
                audioManagerMenu.eatSFX.PlayOneShot(audioManagerMenu.eatSFX.clip);

                Destroy(other.gameObject);
                
                tc.tama.Feed(0.2f);

                if (tc.tama.Food >= 0.8f && (int)tc.tama.Age == 1) // TODO: this condition may want to be tweaked
                {
                    // TODO: raise gates
                    tem.isEvolveReady = true;
                    tc.SetUpdatingStats(false);
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
                    // TODO: raise gates
                    tem.isEvolveReady = true;
                    tc.SetUpdatingStats(false);
                }
            }
            else if (other.gameObject == connectedLightSwitch)
            {
                var lightSwitch = connectedLightSwitch.GetComponent<LightSwitchBool>();

                lightSwitch.lightOn = !lightSwitch.lightOn;
            }
            else if (other.gameObject.CompareTag("fakeTama"))
            {
                if (tem.isEvolveReady)
                {
                    if (tem.isFirstTime)
                    {
                        tem.Evolve();
                
                        tutorial.PlayMallTutorial(0);
                
                        FindObjectOfType<HintButtons>().ShowTamaButton(); 
                    }
                    else
                    {
                        tem.Place();
                    }

                    hasRealTama = true;
                }
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
        else if (other.gameObject.CompareTag("tutorialVolume"))
        {  
            tutorial.PlayMallTutorial(1);

            if (hasRealTama)
            {
                gateControlScript.FoodCourtGateDown = false;
                gateControlScript.FrontDoorGateDown = true;
                
                gateControlScript.TutorialGateDown = true;
            }
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
