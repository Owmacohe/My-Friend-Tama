using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectDetection : MonoBehaviour
{
    public bool hasCoin;
    [SerializeField] GameObject washroomSpawnerOBJ;
    [SerializeField] GameObject connectedLightSwitch;

    TamagotchiController tc;
    bool InWashroom = false;

    void Start()
    {
        tc = FindObjectOfType<TamagotchiController>();

        washroomSpawnerOBJ.SetActive(false);

    }

    void CheckWashroomSpawner()
    {
        if(connectedLightSwitch.GetComponent<LightSwitchBool>().lightOn
            && InWashroom)
        {
            washroomSpawnerOBJ.SetActive(true);
        }
        else
        {
            washroomSpawnerOBJ.SetActive(false);
        }

    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log($"Collide with '{other.gameObject}'");
        if (Input.GetKey(KeyCode.E))
        {

            if (other.gameObject.CompareTag("Food"))
            {
                Destroy(other.gameObject);
                
                tc.tama.Feed();
            }
            else if (other.gameObject.CompareTag("Money"))
            {
                if (!hasCoin)
                {
                    Destroy(other.gameObject);
                    hasCoin = true;
                }
                else
                {
                    return;   
                }
            }
            else if (other.gameObject.CompareTag("ArcadeCab"))
            {
                CoinCheck(other);
                
                tc.tama.Play();
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
            InWashroom = true;
            CheckWashroomSpawner();
        }
    }

    void OnTriggerExit(Collider other)
    {

        //Stops spawning washroom ghosts
        if (other.gameObject.CompareTag("washroomStart"))
        {
            InWashroom = false;
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
