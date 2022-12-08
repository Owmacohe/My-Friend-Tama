using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound2Script : MonoBehaviour
{
    [SerializeField] GameObject ArcadeTokenSpawner;

    PlayerObjectDetection playerObjectDetection;
    GateControlScript GateControl;

    void Start()
    {
        GateControl = GameObject.FindGameObjectWithTag("gateControl").gameObject.GetComponent<GateControlScript>();
        playerObjectDetection = FindObjectOfType<PlayerObjectDetection>();
    
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                GateControl.ArcadeGateADown = true;
                GateControl.ArcadeGateBDown = true;

                playerObjectDetection.hasCoin = true;
                ArcadeTokenSpawner.SetActive(true);
                gameObject.SetActive(false);

                FindObjectOfType<TamagotchiController>().StartRound(2);
            }
        }
    }
}
