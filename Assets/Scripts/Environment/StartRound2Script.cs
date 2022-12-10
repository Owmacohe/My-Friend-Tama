using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound2Script : MonoBehaviour
{
    [SerializeField] GameObject arcadeTokenSpawner;
    
    PlayerObjectDetection playerObjectDetection;
    GateControlScript gateControl;

    void Start()
    {
        gateControl = FindObjectOfType<GateControlScript>();
        playerObjectDetection = FindObjectOfType<PlayerObjectDetection>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                gateControl.arcadeGateADown = true;
                gateControl.arcadeGateBDown = true;

                playerObjectDetection.hasCoin = true;
                arcadeTokenSpawner.SetActive(true);
                gameObject.SetActive(false);

                FindObjectOfType<TamagotchiController>().StartRound(2);
            }
        }
    }
}
