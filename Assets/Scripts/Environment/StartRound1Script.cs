using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound1Script : MonoBehaviour
{
    [SerializeField] GameObject FoodCourtSpawner;

    GateControlScript GateControl;
    
    void Start()
    {
        GateControl = GameObject.FindGameObjectWithTag("gateControl").gameObject.GetComponent<GateControlScript>();
        FoodCourtSpawner.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                GateControl.FoodCourtGateDown = true;
                
                FoodCourtSpawner.SetActive(true);
                gameObject.SetActive(false);
                
                FindObjectOfType<TamagotchiController>().StartRound(1);
            }
        }
    }
}
