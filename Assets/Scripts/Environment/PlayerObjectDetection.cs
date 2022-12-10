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
    [HideInInspector] public bool hasRealTama;
    AudioManagerMenu audioManagerMenu;
    PlayerController playerController;
    GateControlScript gateControlScript;
    TutorialSoundsController tutorial;
    Transform respawnPoint;
    CheckpointScript cps;
    LightSwitchBool lsb;
  
    void Start()
    {
        audioManagerMenu = AudioManager.GetComponent<AudioManagerMenu>();
        playerController = GetComponent<PlayerController>();
        gateControlScript = FindObjectOfType<GateControlScript>();
        tutorial = FindObjectOfType<TutorialSoundsController>();
        cps = FindObjectOfType<CheckpointScript>();
        lsb = connectedLightSwitch.GetComponent<LightSwitchBool>();

        washroomSpawnerOBJ.SetActive(false);
    }

    void FixedUpdate()
    {
        CheckWashroomSpawner();

        if (isPlayingWashroomGame)
        {
            tc.tama.Scold(0.005f);
        }
        
        if (hasRealTama && !tutorial.isPlaying && gateControlScript.tutorialGateDown && tutorial.tutorialProgress == 2)
        {
            gateControlScript.tutorialGateDown = false;
        }

        if (tc.IsGateTimeDone(1))
        {
            gateControlScript.foodCourtGateDown = false;
        }
        else if (tc.IsGateTimeDone(2))
        {
            gateControlScript.arcadeGateADown = false;
            gateControlScript.arcadeGateBDown = false;
        }
        else if (tc.IsGateTimeDone(3))
        {
            gateControlScript.bathroomGateDown = false;
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
        if (!lsb.lightOn && inWashroom)
        {
            washroomLights.SetActive(false);
            washroomSpawnerOBJ.SetActive(true);

            isPlayingWashroomGame = true;

            if (playerController.flashlightOn)
            {
                washroomSpawnerOBJ.SetActive(false);
                isPlayingWashroomGame = false;
            }
            else
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
            if (hasRealTama)
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
                        
                        if (!tc.hasRound2Started && !tc.IsRoundDone(2))
                            tc.StartRound(2);
                    }
                }
                else if (other.gameObject.CompareTag("ArcadeCab"))
                {
                    CoinCheck(other);
                
                    tc.tama.Play(0.2f);
                }   
            }

            if (other.gameObject.CompareTag("fakeTama"))
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.gameObject == connectedLightSwitch && hasRealTama)
            {
                var lightSwitch = connectedLightSwitch.GetComponent<LightSwitchBool>();

                lightSwitch.lightOn = !lightSwitch.lightOn;

                if (!tc.hasRound3Started)
                    tc.StartRound(3);
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
                gateControlScript.foodCourtGateDown = false;
                gateControlScript.frontDoorGateDown = true;
                
                gateControlScript.tutorialGateDown = true;
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
