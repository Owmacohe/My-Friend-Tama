using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectDetection : MonoBehaviour
{
    public bool hasCoin;
    [SerializeField] GameObject washroomSpawnerOBJ;
    [SerializeField] GameObject connectedLightSwitch;
    [SerializeField] GameObject washroomLights;

    TamagotchiController tc;
    bool inWashroom;
    bool isPlayingWashroomGame;

    void Start()
    {
        tc = FindObjectOfType<TamagotchiController>();
        tc.Evolve(); // TODO: this first evolution to be removed and placed where we want it somewhere in the tutorial

        washroomSpawnerOBJ.SetActive(false);
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
            washroomSpawnerOBJ.SetActive(true);
            washroomLights.SetActive(false);
            
            isPlayingWashroomGame = true;
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
                Destroy(other.gameObject);
                
                tc.tama.Feed(0.15f);

                if (tc.tama.Food >= 0.8f && (int)tc.tama.Age == 1) // TODO: this condition may want to be tweaked
                {
                    tc.Evolve();
                }
            }
            else if (other.gameObject.CompareTag("Money"))
            {
                if (!hasCoin)
                {
                    Destroy(other.gameObject);
                    hasCoin = true;
                }
            }
            else if (other.gameObject.CompareTag("ArcadeCab"))
            {
                CoinCheck(other);
                
                tc.tama.Play(0.15f);
                
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
            hasCoin = false;
        }
    }
}
