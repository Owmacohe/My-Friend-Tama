using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectDetection : MonoBehaviour
{

    public bool hasCoin = false;

    void OnTriggerStay(Collider other)
    {
        // TODO: eventually we'll also need to check if the tamagotchi needs food currently
        
        if (Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.CompareTag("Food"))
            {
                Destroy(other.gameObject);
            }
            
            else  if (other.gameObject.CompareTag("Money"))
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
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

    public void CoinCheck(Collider other)
    {
        if (hasCoin)
        {
            ///Call increase to entertainment here
            hasCoin = false;
        }
    }
}
