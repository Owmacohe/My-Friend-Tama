using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateSwitchScript : MonoBehaviour
{
    public GameObject stateDefault;
    public GameObject stateInRange;

    // Start is called before the first frame update
    void Start()
    {
        stateDefault.SetActive(true);
        stateInRange.SetActive(false);

    }

   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stateDefault.SetActive(false);
            stateInRange.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stateDefault.SetActive(true);
            stateInRange.SetActive(false);
        }
    }
}
