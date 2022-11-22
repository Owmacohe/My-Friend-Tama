using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectDetection : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        // TODO: eventually we'll also need to check if the tamagotchi needs food currently
        
        if (Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.CompareTag("Food"))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
