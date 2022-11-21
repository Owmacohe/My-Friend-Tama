using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectDetection : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.tag == "Food")
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
