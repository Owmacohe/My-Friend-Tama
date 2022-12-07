using System;
using UnityEngine;

public class PlayerObjectDetection : MonoBehaviour
{
    [SerializeField] GameObject AudioManager;
    [SerializeField] GameObject washroomSpawnerOBJ;
    [SerializeField] GameObject connectedLightSwitch;
    [SerializeField] GameObject washroomLights;
    [SerializeField] GameObject deathScreen;
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
    Transform respawnPoint;
    CheckpointScript cps;
  
    void Start()
    {
        audioManagerMenu = AudioManager.GetComponent<AudioManagerMenu>();
        gateControlScript = FindObjectOfType<GateControlScript>();
        tutorial = FindObjectOfType<TutorialSoundsController>();
        cps = FindObjectOfType<CheckpointScript>();

        washroomSpawnerOBJ.SetActive(false);
    }

    void FixedUpdate()
    {
        CheckWashroomSpawner();

        if (isPlayingWashroomGame)
        {
            tc.tama.Scold(0.005f);
        }
        
        if (hasRealTama && !tutorial.isPlaying && gateControlScript.TutorialGateDown && tutorial.tutorialProgress == 2)
        {
            gateControlScript.TutorialGateDown = false;
        }

        if (tc.IsGateTimeDone(1))
        {
            gateControlScript.FoodCourtGateDown = false;
        }
        else if (tc.IsGateTimeDone(2))
        {
            gateControlScript.ArcadeGateADown = false;
            gateControlScript.ArcadeGateBDown = false;
        }
        else if (tc.IsGateTimeDone(3))
        {
            gateControlScript.BathroomGateDown = false;
        }

        if (tc.IsRoundDone(1))
        {
            tem.isEvolveReady = true;
            tutorial.PlayMallTutorial(3);
        }
        else if (tc.IsRoundDone(2))
        {
            tem.isEvolveReady = true;
            tutorial.PlayMallTutorial(6);
        }
        else if (tc.IsRoundDone(3))
        {
            tem.isEvolveReady = true;
            tutorial.PlayMallTutorial(8);
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
                tutorial.PlayMallTutorial(2);
                
                cps.hasPassedCheckpoint1 = true;
                
                audioManagerMenu.arcadeCabSFX.PlayOneShot(audioManagerMenu.eatSFX.clip);

                Destroy(other.gameObject);
                
                tc.tama.Feed(0.2f);
            }
            else if (other.gameObject.CompareTag("Money"))
            {
                tutorial.PlayMallTutorial(5);
                
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
            }
            else if (other.gameObject == connectedLightSwitch)
            {
                var lightSwitch = connectedLightSwitch.GetComponent<LightSwitchBool>();

                lightSwitch.lightOn = !lightSwitch.lightOn;
                
                if (tc.hasRound3Started)
                    tc.StartRound(3);
            }
            else if (other.gameObject.CompareTag("fakeTama"))
            {
                if (tem.isEvolveReady)
                {
                    if (tem.isFirstTime)
                    {
                        tem.Evolve();
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
        else if (other.gameObject.CompareTag("streamerVolume"))
        {
            tutorial.PlayStreamerTutorial(other.gameObject);
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
