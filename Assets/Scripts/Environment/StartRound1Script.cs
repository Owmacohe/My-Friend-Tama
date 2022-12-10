using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound1Script : MonoBehaviour
{
    [SerializeField] GameObject foodCourtSpawner;

    GateControlScript gateControl;
    //PlayerObjectDetection playerObjectDetection;
    
    void Start()
    {
        gateControl = FindObjectOfType<GateControlScript>();
        foodCourtSpawner.SetActive(false);

        //playerObjectDetection = FindObjectOfType<PlayerObjectDetection>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                gateControl.foodCourtGateDown = true;
                
                foodCourtSpawner.SetActive(true);
                gameObject.SetActive(false);
                
                FindObjectOfType<TamagotchiController>().StartRound(1);
            }
        }
    }
}
