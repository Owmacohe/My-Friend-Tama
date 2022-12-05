using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound1Script : MonoBehaviour
{
    [SerializeField] GameObject FoodCourtSpawner;

    GateControlScript GateControl;

    // Start is called before the first frame update
    void Start()
    {
        GateControl = GameObject.FindGameObjectWithTag("gateControl").gameObject.GetComponent<GateControlScript>();
        FoodCourtSpawner.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                GateControl.FoodCourtGateDown = true;
                FoodCourtSpawner.SetActive(true);
                Destroy(gameObject);

            }
        }
    }
}
